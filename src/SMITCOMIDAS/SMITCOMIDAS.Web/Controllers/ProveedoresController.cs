using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Shared.Models.RegistroRequest;
using SMITCOMIDAS.Web.Data;
using System.Security.Claims;

namespace SMITCOMIDAS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProveedoresController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
        {
            return await _context.Proveedores
                .OrderBy(p => p.RazonSocial)
                .ToListAsync();
        }

        // GET: api/proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
                return NotFound();

            return proveedor;
        }

        [HttpGet("mi-perfil")]
        public async Task<ActionResult<Proveedor>> GetMiPerfil()
        {
            var userId = User.Claims
               .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
               .Skip(1)
               .FirstOrDefault()?.Value;
            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (proveedor == null)
                return NotFound();

            return proveedor;
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<Proveedor>> GetProveedorByUsuarioId(string usuarioId)
        {
            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(p => p.UserId == usuarioId);

            if (proveedor == null)
                return NotFound();

            return proveedor;
        }

        // POST: api/proveedores
        [HttpPost("registro-completo")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Proveedor>> RegistroCompleto(RegistroProveedorRequest request)
        {
            Console.WriteLine("=== REGISTRO COMPLETO PROVEEDOR ===");

            // Validar que las contraseñas coincidan
            if (request.Password != request.ConfirmPassword)
                return BadRequest("Las contraseñas no coinciden");

            // Validar que no exista el email
            var existeEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existeEmail != null)
                return BadRequest("Ya existe un usuario con este email");

            // Validar que no exista el NIT
            var existeNIT = await _context.Proveedores
                .AnyAsync(p => p.NIT == request.NIT);

            if (existeNIT)
                return BadRequest("Ya existe un proveedor con este NIT");

            // Iniciar transacción
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Crear usuario
                var user = new ApplicationUser
                {
                    UserName = request.Email,
                    Email = request.Email,
                    FullName = request.RazonSocial,
                    EmailConfirmed = true,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest($"Error al crear usuario: {errors}");
                }

                // Asignar rol Proveedor
                await _userManager.AddToRoleAsync(user, "Proveedor");

                // Crear proveedor
                var proveedor = new Proveedor
                {
                    UserId = user.Id,
                    NIT = request.NIT,
                    RazonSocial = request.RazonSocial,
                    NombreComercial = request.NombreComercial,
                    Telefono = request.Telefono,
                    TelefonoAdicional = request.TelefonoAdicional,
                    Email = request.Email,
                    EmailAdicional = request.EmailAdicional,
                    Direccion = request.Direccion,
                    Ciudad = request.Ciudad,
                    Departamento = request.Departamento,
                    Pais = request.Pais,
                    Contacto = request.Contacto,
                    TelefonoContacto = request.TelefonoContacto,
                    FechaRegistro = DateTime.Now,
                    Activo = true
                };

                _context.Proveedores.Add(proveedor);
                await _context.SaveChangesAsync();

                // Confirmar transacción
                await transaction.CommitAsync();

                return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.Id }, proveedor);
            }
            catch (Exception ex)
            {
                // Revertir todo si algo falla
                await transaction.RollbackAsync();
                return StatusCode(500, "Error al crear usuario y proveedor");
            }
        }
        // PUT: api/proveedores/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Proveedor")]
        public async Task<IActionResult> UpdateProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.Id)
                return BadRequest();

            // Validar que no exista otro proveedor con el mismo NIT
            var existeNIT = await _context.Proveedores
                .AnyAsync(p => p.NIT == proveedor.NIT && p.Id != id);

            if (existeNIT)
                return BadRequest("Ya existe otro proveedor con este NIT");

            _context.Entry(proveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProveedorExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/proveedores/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var proveedor = await _context.Proveedores
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (proveedor == null)
                    return NotFound();

                var usuario = await _userManager.FindByEmailAsync(proveedor.Email);
                if (usuario != null)
                {
                    // Eliminar el usuario
                    var result = await _userManager.DeleteAsync(usuario);
                    if (!result.Succeeded)
                    {
                        throw new Exception("No se pudo eliminar el usuario asociado");
                    }
                }
                _context.Proveedores.Remove(proveedor);
                await _context.SaveChangesAsync();

                // Confirmar transacción
                await transaction.CommitAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // GET: api/proveedores/activos
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedoresActivos()
        {
            return await _context.Proveedores
                .Where(p => p.Activo)
                .OrderBy(p => p.RazonSocial)
                .ToListAsync();
        }

        private async Task<bool> ProveedorExists(int id)
        {
            return await _context.Proveedores.AnyAsync(e => e.Id == id);
        }

        [HttpPost("cambiar-password")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return NotFound("Usuario no encontrado");

            // Remover contraseña actual
            var removeResult = await _userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
                return BadRequest("Error al remover contraseña actual");

            // Agregar nueva contraseña
            var addResult = await _userManager.AddPasswordAsync(user, request.NuevaPassword);
            if (!addResult.Succeeded)
            {
                var errors = string.Join(", ", addResult.Errors.Select(e => e.Description));
                return BadRequest($"Error al cambiar contraseña: {errors}");
            }

            return Ok("Contraseña cambiada exitosamente");
        }
    }
}
