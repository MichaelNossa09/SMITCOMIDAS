using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMITCOMIDAS.Shared.Models;

namespace SMITCOMIDAS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Obtiene todos los roles disponibles en el sistema
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<string>>> GetRoles()
        {
            try
            {
                var roles = _roleManager.Roles.Select(r => r.Name).ToList();
                return Ok(roles);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error al obtener roles: {ex.Message}");
            }
        }

        /// <summary>
        /// Verifica si un rol específico existe
        /// </summary>
        [HttpGet("exists/{roleName}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> RoleExists(string roleName)
        {
            try
            {
                var exists = await _roleManager.RoleExistsAsync(roleName);
                return Ok(exists);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error al verificar rol: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene los usuarios en un rol específico
        /// </summary>
        [HttpGet("users/{roleName}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<string>>> GetUsersInRole(string roleName)
        {
            try
            {
                // Verificar que el rol exista
                if (!await _roleManager.RoleExistsAsync(roleName))
                    return NotFound($"No existe el rol '{roleName}'");

                // Obtener usuarios en el rol
                var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
                var userIds = usersInRole.Select(u => u.Id).ToList();

                return Ok(userIds);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error al obtener usuarios del rol: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea un nuevo rol
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<string>> CreateRole([FromBody] string roleName)
        {
            try
            {
                // Verificar que el rol no exista
                if (await _roleManager.RoleExistsAsync(roleName))
                    return BadRequest($"El rol '{roleName}' ya existe");

                // Crear rol
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                    return CreatedAtAction(nameof(RoleExists), new { roleName }, roleName);

                return BadRequest(result.Errors.First().Description);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error al crear rol: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un rol existente
        /// </summary>
        [HttpDelete("{roleName}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteRole(string roleName)
        {
            try
            {
                // Verificar que el rol exista
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null)
                    return NotFound($"No existe el rol '{roleName}'");

                // Verificar que no sea un rol del sistema
                if (roleName == "Admin" || roleName == "Administrativo" || roleName == "Operativo" || roleName == "Proveedor")
                    return BadRequest($"No se puede eliminar el rol del sistema '{roleName}'");

                // Eliminar rol
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return NoContent();

                return BadRequest(result.Errors.First().Description);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error al eliminar rol: {ex.Message}");
            }
        }
    }
}
