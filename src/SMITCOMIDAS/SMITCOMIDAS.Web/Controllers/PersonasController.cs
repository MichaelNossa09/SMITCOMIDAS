using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Web.Data;
using System.Security.Claims;

namespace SMITCOMIDAS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PersonasController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("me")]
        public async Task<ActionResult<Persona>> GetMiPerfil()
        {
            var userId = User.Claims
                .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                .Skip(1)
                .FirstOrDefault()?.Value;

            var persona = await _context.Personas
                .Include(p => p.CentroCosto)
                    .ThenInclude(cc => cc.Compania)
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (persona == null)
                return NotFound();

            return persona;
        }

        // GET: api/personas
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
        {
            return await _context.Personas
                .Include(p => p.CentroCosto)
                .Include(p => p.User)
                .ToListAsync();
        }

        // GET: api/personas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _context.Personas
                .Include(p => p.CentroCosto)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (persona == null)
                return NotFound();

            return persona;
        }

        // POST: api/personas
        [HttpPost]
        public async Task<ActionResult<Persona>> CreatePersona(Persona persona)
        {
            var userId = User.Claims
                .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                .Skip(1)
                .FirstOrDefault()?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest("Usuario no autenticado");

            persona.UserId = userId;

            var existeIdentificacion = await _context.Personas
                .AnyAsync(p => p.Identificacion == persona.Identificacion);

            if (existeIdentificacion)
                return BadRequest("Ya existe una persona con esta identificación");

            // Inicializar valores de cuota mensual
            persona.PedidosRestantesMes = persona.MaxPedidosMes;
            persona.UltimaActualizacionPedidos = DateTime.Now;

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);
        }

        [HttpPost("registro-completo")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Persona>> RegistroCompleto(RegistroPersonaRequest request)
        {
            Console.WriteLine("=== REGISTRO COMPLETO ===");

            if (request.Password != request.ConfirmPassword)
                return BadRequest("Las contraseñas no coinciden");

            var existeEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existeEmail != null)
                return BadRequest("Ya existe un usuario con este email");

            var existeIdentificacion = await _context.Personas
                .AnyAsync(p => p.Identificacion == request.Identificacion);

            if (existeIdentificacion)
                return BadRequest("Ya existe una persona con esta identificación");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = new ApplicationUser
                {
                    UserName = request.Email,
                    Email = request.Email,
                    FullName = $"{request.Nombres} {request.Apellidos}",
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest($"Error al crear usuario: {errors}");
                }

                await _userManager.AddToRoleAsync(user, request.Rol);

                var persona = new Persona
                {
                    UserId = user.Id,
                    Identificacion = request.Identificacion,
                    TipoIdentificacion = request.TipoIdentificacion,
                    Nombres = request.Nombres,
                    Apellidos = request.Apellidos,
                    Cargo = request.Cargo,
                    CentroCostoId = request.CentroCostoId,
                    Telefono = request.Telefono,
                    TelefonoAdicional = request.TelefonoAdicional,
                    Direccion = request.Direccion,
                    FechaIngreso = DateTime.Now,
                    Activo = true,
                    MaxPedidosMes = request.MaxPedidosMes,
                    PedidosRestantesMes = request.MaxPedidosMes, // NUEVO
                    UltimaActualizacionPedidos = DateTime.Now // NUEVO
                };

                _context.Personas.Add(persona);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, "Error al crear usuario y persona");
            }
        }

        // PUT: api/personas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersona(int id, Persona persona)
        {
            if (id != persona.Id)
                return BadRequest("El ID de la persona no coincide con el ID proporcionado en la ruta");

            var userId = User.Claims
                 .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                 .Skip(1)
                 .FirstOrDefault()?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("No se pudo identificar al usuario");
            }

            var personaActual = await _context.Personas.FindAsync(id);

            if (personaActual == null)
                return NotFound("La persona no existe");

            bool isAdmin = User.IsInRole("Admin");
            bool isOwner = personaActual.UserId == userId;

            if (!isAdmin && !isOwner)
                return Forbid("Solo puedes editar tu propio perfil");

            // Preservar campos que no deben cambiar
            if (!isAdmin)
            {
                persona.UserId = personaActual.UserId;
                persona.FechaIngreso = personaActual.FechaIngreso;
                persona.MaxPedidosMes = personaActual.MaxPedidosMes; // Usuario normal no puede cambiar esto
                persona.PedidosRestantesMes = personaActual.PedidosRestantesMes;
                persona.UltimaActualizacionPedidos = personaActual.UltimaActualizacionPedidos;
            }
            else
            {
                // Si el admin cambia MaxPedidosMes, recalcular los restantes
                if (persona.MaxPedidosMes != personaActual.MaxPedidosMes)
                {
                    var mesActual = DateTime.Now.Month;
                    var añoActual = DateTime.Now.Year;

                    // Contar pedidos RECIBIDOS en el mes actual
                    var pedidosRecibidosEsteMes = await _context.Pedidos
                        .Where(p => p.UsuarioId == persona.UserId &&
                                   p.Estado == EstadoPedido.Recibido &&
                                   p.FechaRecepcion.HasValue &&
                                   p.FechaRecepcion.Value.Month == mesActual &&
                                   p.FechaRecepcion.Value.Year == añoActual)
                        .CountAsync();

                    // Calcular restantes = Máximo - Consumidos
                    persona.PedidosRestantesMes = persona.MaxPedidosMes - pedidosRecibidosEsteMes;

                    // Si el nuevo máximo es menor que los consumidos, los restantes serán 0
                    if (persona.PedidosRestantesMes < 0)
                        persona.PedidosRestantesMes = 0;

                    persona.UltimaActualizacionPedidos = DateTime.Now;
                }
                else
                {
                    // Si no cambia MaxPedidosMes, preservar los valores actuales
                    persona.PedidosRestantesMes = personaActual.PedidosRestantesMes;
                    persona.UltimaActualizacionPedidos = personaActual.UltimaActualizacionPedidos;
                }
            }

            _context.Entry(personaActual).CurrentValues.SetValues(persona);

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
                    return NotFound();
                throw;
            }
        }

        // DELETE: api/personas/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
                return NotFound();

            persona.Activo = false; // Soft delete
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.Id == id);
        }

        // POST: api/personas/cambiar-password
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

        [HttpPost("actualizar-cuota/{userId}")]
        public async Task<IActionResult> ActualizarCuotaMensual(string userId)
        {
            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.UserId == userId);

            if (persona == null)
                return NotFound("Persona no encontrada");

            var mesActual = DateTime.Now.Month;
            var añoActual = DateTime.Now.Year;

            // Solo actualizar si estamos en un mes diferente
            if (persona.UltimaActualizacionPedidos.Month != mesActual ||
                persona.UltimaActualizacionPedidos.Year != añoActual)
            {
                persona.PedidosRestantesMes = persona.MaxPedidosMes;
                persona.UltimaActualizacionPedidos = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet("pedidos-restantes/{userId}")]
        public async Task<ActionResult<int>> ObtenerPedidosRestantes(string userId)
        {
            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.UserId == userId);

            if (persona == null)
                return NotFound();

            // Verificar si necesita actualización mensual
            var mesActual = DateTime.Now.Month;
            var añoActual = DateTime.Now.Year;

            if (persona.UltimaActualizacionPedidos.Month != mesActual ||
                persona.UltimaActualizacionPedidos.Year != añoActual)
            {
                persona.PedidosRestantesMes = persona.MaxPedidosMes;
                persona.UltimaActualizacionPedidos = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return Ok(persona.PedidosRestantesMes);
        }
    }
}
