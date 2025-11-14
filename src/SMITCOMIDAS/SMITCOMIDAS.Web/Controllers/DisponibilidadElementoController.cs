using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Shared.Models.DTOs;
using SMITCOMIDAS.Web.Data;
using System.Security.Claims;

namespace SMITCOMIDAS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisponibilidadElementoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisponibilidadElementoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/disponibilidades/elemento/5
        [HttpGet("elemento/{elementoId}")]
        public async Task<ActionResult<IEnumerable<DisponibilidadElementoDTO>>> GetDisponibilidadesByElemento(int elementoId)
        {
            try
            {
                var disponibilidades = await _context.DisponibilidadesElemento
                    .AsNoTracking()
                    .Where(d => d.ElementoMenuId == elementoId)
                    .ToListAsync();

                var disponibilidadesDTOs = disponibilidades.Select(d => d.ToDTO()).ToList();
                return Ok(disponibilidadesDTOs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener disponibilidades: {ex.Message}");
                return StatusCode(500, $"Error al obtener disponibilidades: {ex.Message}");
            }
        }

        // GET: api/disponibilidades/elemento/5/count
        [HttpGet("elemento/{elementoId}/count")]
        public async Task<ActionResult<int>> GetDisponibilidadesCount(int elementoId)
        {
            try
            {
                var count = await _context.DisponibilidadesElemento
                    .CountAsync(d => d.ElementoMenuId == elementoId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener conteo de disponibilidades: {ex.Message}");
                return StatusCode(500, $"Error al obtener conteo de disponibilidades: {ex.Message}");
            }
        }

        // GET: api/disponibilidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DisponibilidadElementoDTO>> GetDisponibilidad(int id)
        {
            try
            {
                var disponibilidad = await _context.DisponibilidadesElemento
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (disponibilidad == null)
                    return NotFound();

                return Ok(disponibilidad.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener disponibilidad: {ex.Message}");
                return StatusCode(500, $"Error al obtener disponibilidad: {ex.Message}");
            }
        }

        // POST: api/disponibilidades
        [HttpPost]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<ActionResult<DisponibilidadElementoDTO>> CreateDisponibilidad(DisponibilidadElementoDTO disponibilidadDTO)
        {
            try
            {
                // Verificar que el elemento existe
                var elemento = await _context.ElementosMenu
                    .Include(e => e.Menu)
                    .ThenInclude(m => m.Proveedor)
                    .FirstOrDefaultAsync(e => e.Id == disponibilidadDTO.ElementoMenuId);

                if (elemento == null)
                    return BadRequest("Elemento de menú no encontrado");

                // Verificar permisos
                if (User.IsInRole("Proveedor"))
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (elemento.Menu.Proveedor.UserId != userId)
                        return Forbid();
                }

                // Verificar si ya existe una disponibilidad para ese día y elemento
                var existingDisponibilidad = await _context.DisponibilidadesElemento
                    .FirstOrDefaultAsync(d =>
                        d.ElementoMenuId == disponibilidadDTO.ElementoMenuId &&
                        d.Dia == disponibilidadDTO.Dia);

                if (existingDisponibilidad != null)
                    return BadRequest("Ya existe una disponibilidad para este día y elemento");

                // Crear la disponibilidad
                var disponibilidad = new DisponibilidadElemento
                {
                    Dia = disponibilidadDTO.Dia,
                    DisponibleDesayuno = disponibilidadDTO.DisponibleDesayuno,
                    DisponibleAlmuerzo = disponibilidadDTO.DisponibleAlmuerzo,
                    DisponibleCena = disponibilidadDTO.DisponibleCena,
                    CantidadDisponible = disponibilidadDTO.CantidadDisponible,
                    ElementoMenuId = disponibilidadDTO.ElementoMenuId
                };

                _context.DisponibilidadesElemento.Add(disponibilidad);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDisponibilidad), new { id = disponibilidad.Id }, disponibilidad.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear disponibilidad: {ex.Message}");
                return StatusCode(500, $"Error al crear disponibilidad: {ex.Message}");
            }
        }

        // PUT: api/disponibilidades/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<IActionResult> UpdateDisponibilidad(int id, DisponibilidadElementoDTO disponibilidadDTO)
        {
            try
            {
                if (id != disponibilidadDTO.Id)
                    return BadRequest();

                // Verificar que la disponibilidad existe
                var disponibilidad = await _context.DisponibilidadesElemento
                    .Include(d => d.ElementoMenu)
                    .ThenInclude(e => e.Menu)
                    .ThenInclude(m => m.Proveedor)
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (disponibilidad == null)
                    return NotFound();

                // Verificar permisos
                if (User.IsInRole("Proveedor"))
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (disponibilidad.ElementoMenu.Menu.Proveedor.UserId != userId)
                        return Forbid();
                }

                // Actualizar propiedades
                disponibilidad.Dia = disponibilidadDTO.Dia;
                disponibilidad.DisponibleDesayuno = disponibilidadDTO.DisponibleDesayuno;
                disponibilidad.DisponibleAlmuerzo = disponibilidadDTO.DisponibleAlmuerzo;
                disponibilidad.DisponibleCena = disponibilidadDTO.DisponibleCena;
                disponibilidad.CantidadDisponible = disponibilidadDTO.CantidadDisponible;

                _context.Entry(disponibilidad).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar disponibilidad: {ex.Message}");
                return StatusCode(500, $"Error al actualizar disponibilidad: {ex.Message}");
            }
        }

        // DELETE: api/disponibilidades/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<IActionResult> DeleteDisponibilidad(int id)
        {
            try
            {
                var disponibilidad = await _context.DisponibilidadesElemento
                    .Include(d => d.ElementoMenu)
                    .ThenInclude(e => e.Menu)
                    .ThenInclude(m => m.Proveedor)
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (disponibilidad == null)
                    return NotFound();

                // Verificar permisos
                if (User.IsInRole("Proveedor"))
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (disponibilidad.ElementoMenu.Menu.Proveedor.UserId != userId)
                        return Forbid();
                }

                _context.DisponibilidadesElemento.Remove(disponibilidad);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar disponibilidad: {ex.Message}");
                return StatusCode(500, $"Error al eliminar disponibilidad: {ex.Message}");
            }
        }

        // DELETE: api/disponibilidades/elemento/5/dia/Lunes
        [HttpDelete("elemento/{elementoId}/dia/{dia}")]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<ActionResult<object>> DeleteDisponibilidadByDia(int elementoId, DiaSemana dia)
        {
            try
            {
                // Verificar que el elemento existe
                var elemento = await _context.ElementosMenu
                    .Include(e => e.Menu)
                    .ThenInclude(m => m.Proveedor)
                    .FirstOrDefaultAsync(e => e.Id == elementoId);

                if (elemento == null)
                    return NotFound("Elemento no encontrado");

                // Verificar permisos
                if (User.IsInRole("Proveedor"))
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (elemento.Menu.Proveedor.UserId != userId)
                        return Forbid();
                }

                // Buscar la disponibilidad para ese día
                var disponibilidad = await _context.DisponibilidadesElemento
                    .FirstOrDefaultAsync(d => d.ElementoMenuId == elementoId && d.Dia == dia);

                if (disponibilidad == null)
                    return NotFound($"No se encontró disponibilidad para el día {dia}");

                // Eliminar la disponibilidad
                _context.DisponibilidadesElemento.Remove(disponibilidad);
                await _context.SaveChangesAsync();

                // Devolver cuántas disponibilidades quedan
                var disponibilidadesRestantes = await _context.DisponibilidadesElemento
                    .CountAsync(d => d.ElementoMenuId == elementoId);

                return Ok(new { success = true, disponibilidadesRestantes });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar disponibilidad: {ex.Message}");
                return StatusCode(500, $"Error al eliminar disponibilidad: {ex.Message}");
            }
        }

    }
}
