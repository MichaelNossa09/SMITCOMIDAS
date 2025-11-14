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
    public class ElementosMenuController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ElementosMenuController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/elementosmenu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElementoMenuDTO>>> GetElementosMenu()
        {
            try
            {
                var elementos = await _context.ElementosMenu
                    .AsNoTracking()
                    .Include(e => e.Menu)
                    .Include(e => e.Disponibilidades)
                    .ToListAsync();

                var elementosDTOs = elementos.Select(e => e.ToDTO(true)).ToList();
                return Ok(elementosDTOs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener elementos: {ex.Message}");
                return StatusCode(500, $"Error al obtener elementos: {ex.Message}");
            }
        }

        // GET: api/elementosmenu/menu/5
        [HttpGet("menu/{menuId}")]
        public async Task<ActionResult<IEnumerable<ElementoMenuDTO>>> GetElementosMenuByMenu(int menuId)
        {
            try
            {
                var elementos = await _context.ElementosMenu
                    .AsNoTracking()
                    .Where(e => e.MenuId == menuId)
                    .Include(e => e.Disponibilidades)
                    .OrderBy(e => e.Orden)
                    .ToListAsync();

                var elementosDTOs = elementos.Select(e => e.ToDTO(true)).ToList();
                return Ok(elementosDTOs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener elementos del menú {menuId}: {ex.Message}");
                return StatusCode(500, $"Error al obtener elementos del menú: {ex.Message}");
            }
        }

        // GET: api/elementosmenu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ElementoMenuDTO>> GetElementoMenu(int id)
        {
            try
            {
                var elemento = await _context.ElementosMenu
                    .AsNoTracking()
                    .Include(e => e.Menu)
                    .Include(e => e.Disponibilidades)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (elemento == null)
                    return NotFound();

                var elementoDTO = elemento.ToDTO(true);
                return Ok(elementoDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener elemento {id}: {ex.Message}");
                return StatusCode(500, $"Error al obtener elemento: {ex.Message}");
            }
        }

        // POST: api/elementosmenu
        [HttpPost]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<ActionResult<ElementoMenuDTO>> CreateElementoMenu(ElementoMenuDTO elementoMenuDTO)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Validar que el menú existe
                    var menuExists = await _context.Menus.AnyAsync(m => m.Id == elementoMenuDTO.MenuId);
                    if (!menuExists)
                        return BadRequest("Menú no encontrado");

                    // Verificar que el usuario actual tenga acceso a este menú
                    if (User.IsInRole("Proveedor"))
                    {
                        var userId = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Skip(1).FirstOrDefault()?.Value;
                        var menu = await _context.Menus
                            .Include(m => m.Proveedor)
                            .FirstOrDefaultAsync(m => m.Id == elementoMenuDTO.MenuId);

                        if (menu.Proveedor.UserId != userId)
                            return Forbid();
                    }

                    // Convertir DTO a entidad
                    var elementoMenu = new ElementoMenu
                    {
                        Nombre = elementoMenuDTO.Nombre,
                        Descripcion = elementoMenuDTO.Descripcion,
                        Precio = elementoMenuDTO.Precio,
                        Categoria = elementoMenuDTO.Categoria,
                        TipoComida = elementoMenuDTO.TipoComida,
                        ImagenUrl = elementoMenuDTO.ImagenUrl,
                        Disponible = elementoMenuDTO.Disponible,
                        Orden = elementoMenuDTO.Orden,
                        MenuId = elementoMenuDTO.MenuId
                    };

                    _context.ElementosMenu.Add(elementoMenu);
                    await _context.SaveChangesAsync();

                    // Agregar disponibilidades
                    if (elementoMenuDTO.Disponibilidades != null)
                    {
                        foreach (var dispDTO in elementoMenuDTO.Disponibilidades)
                        {
                            var disponibilidad = new DisponibilidadElemento
                            {
                                Dia = dispDTO.Dia,
                                DisponibleDesayuno = dispDTO.DisponibleDesayuno,
                                DisponibleAlmuerzo = dispDTO.DisponibleAlmuerzo,
                                DisponibleCena = dispDTO.DisponibleCena,
                                CantidadDisponible = dispDTO.CantidadDisponible,
                                ElementoMenuId = elementoMenu.Id
                            };

                            _context.DisponibilidadesElemento.Add(disponibilidad);
                        }

                        await _context.SaveChangesAsync();
                    }

                    transaction.Commit();

                    // Cargar el elemento con sus disponibilidades
                    var elementoCompleto = await _context.ElementosMenu
                        .Include(e => e.Disponibilidades)
                        .FirstOrDefaultAsync(e => e.Id == elementoMenu.Id);

                    // Convertir a DTO para la respuesta
                    var resultDTO = elementoCompleto.ToDTO(true);

                    return Ok(resultDTO);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error al crear elemento de menú: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Error interno: {ex.InnerException.Message}");
                    }
                    return StatusCode(500, $"Error al crear elemento de menú: {ex.Message}");
                }
            }
        }

        // PUT: api/elementosmenu/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<IActionResult> UpdateElementoMenu(int id, ElementoMenuDTO elementoMenuDTO)
        {
            if (id != elementoMenuDTO.Id)
                return BadRequest();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Verificar que el elemento existe
                    var existingElemento = await _context.ElementosMenu
                        .Include(e => e.Disponibilidades)
                        .Include(e => e.Menu)
                        .ThenInclude(m => m.Proveedor)
                        .FirstOrDefaultAsync(e => e.Id == id);

                    if (existingElemento == null)
                        return NotFound();

                    // Verificar que el usuario actual tenga acceso a este menú
                    if (User.IsInRole("Proveedor"))
                    {
                        var userId = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Skip(1).FirstOrDefault()?.Value;
                        if (existingElemento.Menu.Proveedor.UserId != userId)
                            return Forbid();
                    }

                    // Actualizar propiedades básicas
                    existingElemento.Nombre = elementoMenuDTO.Nombre;
                    existingElemento.Descripcion = elementoMenuDTO.Descripcion;
                    existingElemento.Precio = elementoMenuDTO.Precio;
                    existingElemento.Categoria = elementoMenuDTO.Categoria;
                    existingElemento.TipoComida = elementoMenuDTO.TipoComida;
                    existingElemento.ImagenUrl = elementoMenuDTO.ImagenUrl;
                    existingElemento.Disponible = elementoMenuDTO.Disponible;
                    existingElemento.Orden = elementoMenuDTO.Orden;

                    _context.Entry(existingElemento).State = EntityState.Modified;

                    // Eliminar disponibilidades existentes
                    _context.DisponibilidadesElemento.RemoveRange(existingElemento.Disponibilidades);
                    await _context.SaveChangesAsync();

                    // Agregar nuevas disponibilidades
                    if (elementoMenuDTO.Disponibilidades != null)
                    {
                        foreach (var dispDTO in elementoMenuDTO.Disponibilidades)
                        {
                            var disponibilidad = new DisponibilidadElemento
                            {
                                Dia = dispDTO.Dia,
                                DisponibleDesayuno = dispDTO.DisponibleDesayuno,
                                DisponibleAlmuerzo = dispDTO.DisponibleAlmuerzo,
                                DisponibleCena = dispDTO.DisponibleCena,
                                CantidadDisponible = dispDTO.CantidadDisponible,
                                ElementoMenuId = id
                            };

                            _context.DisponibilidadesElemento.Add(disponibilidad);
                        }

                        await _context.SaveChangesAsync();
                    }

                    transaction.Commit();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error al actualizar elemento: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Error interno: {ex.InnerException.Message}");
                    }
                    return StatusCode(500, $"Error interno al actualizar elemento: {ex.Message}");
                }
            }
        }

        // DELETE: api/elementosmenu/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<IActionResult> DeleteElementoMenu(int id)
        {
            try
            {
                var elementoMenu = await _context.ElementosMenu
                    .Include(e => e.Menu)
                    .ThenInclude(m => m.Proveedor)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (elementoMenu == null)
                    return NotFound();

                // Verificar que el usuario actual tenga acceso a este menú
                if (User.IsInRole("Proveedor"))
                {
                    var userId = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Skip(1).FirstOrDefault()?.Value;
                    if (elementoMenu.Menu.Proveedor.UserId != userId)
                        return Forbid();
                }

                _context.ElementosMenu.Remove(elementoMenu);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar elemento: {ex.Message}");
                return StatusCode(500, $"Error al eliminar elemento: {ex.Message}");
            }
        }

        // DELETE: api/elementosmenu/{id}/disponibilidad/{dia}
        [HttpDelete("{id}/disponibilidad/{dia}")]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<ActionResult<object>> DeleteElementoMenuDisponibilidad(int id, DiaSemana dia)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Obtener el elemento con sus disponibilidades
                    var elementoMenu = await _context.ElementosMenu
                        .Include(e => e.Menu)
                        .ThenInclude(m => m.Proveedor)
                        .Include(e => e.Disponibilidades)
                        .FirstOrDefaultAsync(e => e.Id == id);

                    if (elementoMenu == null)
                        return NotFound("Elemento no encontrado");

                    // Verificar permisos
                    if (User.IsInRole("Proveedor"))
                    {
                        var userId = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Skip(1).FirstOrDefault()?.Value;
                        if (elementoMenu.Menu.Proveedor.UserId != userId)
                            return Forbid();
                    }

                    // Buscar la disponibilidad específica para ese día
                    var disponibilidad = elementoMenu.Disponibilidades
                        .FirstOrDefault(d => d.Dia == dia);

                    if (disponibilidad == null)
                        return NotFound($"No se encontró disponibilidad para el día {dia}");

                    // Eliminar solo esa disponibilidad
                    _context.DisponibilidadesElemento.Remove(disponibilidad);
                    await _context.SaveChangesAsync();

                    // Contar cuántas disponibilidades quedan para el elemento
                    var disponibilidadesRestantes = await _context.DisponibilidadesElemento
                        .CountAsync(d => d.ElementoMenuId == id);

                    transaction.Commit();

                    return Ok(new { success = true, disponibilidadesRestantes });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error al eliminar disponibilidad: {ex.Message}");
                    return StatusCode(500, $"Error al eliminar disponibilidad: {ex.Message}");
                }
            }
        }

        // PUT: api/elementosmenu/5/disponibilidad
        [HttpPut("{id}/disponibilidad")]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<IActionResult> ToggleDisponibilidad(int id, [FromBody] bool disponible)
        {
            try
            {
                var elementoMenu = await _context.ElementosMenu
                    .Include(e => e.Menu)
                    .ThenInclude(m => m.Proveedor)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (elementoMenu == null)
                    return NotFound();

                // Verificar que el usuario actual tenga acceso a este menú
                if (User.IsInRole("Proveedor"))
                {
                    var userId = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Skip(1).FirstOrDefault()?.Value;
                    if (elementoMenu.Menu.Proveedor.UserId != userId)
                        return Forbid();
                }

                elementoMenu.Disponible = disponible;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar disponibilidad: {ex.Message}");
                return StatusCode(500, $"Error al cambiar disponibilidad: {ex.Message}");
            }
        }

    }
}
