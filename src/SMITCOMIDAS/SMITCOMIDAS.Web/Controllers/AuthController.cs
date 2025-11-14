using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SMITCOMIDAS.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static SMITCOMIDAS.Web.Services.AuthService;

namespace SMITCOMIDAS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return Unauthorized();

            // Obtener los roles del usuario usando el UserManager
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Usuario"; // Tomar el primer rol o usar "Usuario" por defecto

            var token = GenerateJwtToken(user, role);

            return Ok(new LoginResponse
            {
                Token = token,
                UserName = user.FullName,
                Role = role,
            });
        }

        [HttpGet("userrole/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserRole(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return NotFound("Usuario no encontrado");

                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "Usuario";

                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPost("updaterole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateRoleRequest model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.Role))
                    return BadRequest("El ID de usuario y el rol son requeridos");

                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                    return NotFound("Usuario no encontrado");

                // Verificar si el rol existe
                bool roleExists = await _roleManager.RoleExistsAsync(model.Role);
                if (!roleExists)
                    return BadRequest($"El rol '{model.Role}' no existe");

                // Obtener roles actuales del usuario
                var currentRoles = await _userManager.GetRolesAsync(user);

                // Si ya tiene el mismo rol, no hacer nada
                if (currentRoles.Contains(model.Role) && currentRoles.Count == 1)
                    return Ok("El usuario ya tiene este rol asignado");

                // Eliminar roles actuales
                if (currentRoles.Any())
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!removeResult.Succeeded)
                        return BadRequest("Error al eliminar roles existentes: " + string.Join(", ", removeResult.Errors.Select(e => e.Description)));
                }

                // Asignar nuevo rol
                var addResult = await _userManager.AddToRoleAsync(user, model.Role);
                if (!addResult.Succeeded)
                    return BadRequest("Error al asignar nuevo rol: " + string.Join(", ", addResult.Errors.Select(e => e.Description)));

                return Ok("Rol actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        private string GenerateJwtToken(ApplicationUser user, string role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class UpdateRoleRequest
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
