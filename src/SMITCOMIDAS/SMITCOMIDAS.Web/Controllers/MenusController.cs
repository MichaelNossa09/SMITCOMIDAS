using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Shared.Models.DTOs;
using SMITCOMIDAS.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenusController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MenusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDTO>>> GetMenus([FromQuery] bool includeElementos = false)
        {
            try
            {
                List<Menu> menus;

                if (includeElementos)
                {
                    menus = await _context.Menus.AsNoTracking()
                        .Include(m => m.Elementos.OrderBy(e => e.Orden))
                        .ThenInclude(e => e.Disponibilidades)
                        .Include(m => m.Proveedor)
                        .ToListAsync();
                }
                else
                {
                    menus = await _context.Menus.AsNoTracking()
                        .Include(m => m.Proveedor)
                        .ToListAsync();
                }

                // Convertir a DTO
                var menuDTOs = menus.Select(m => m.ToDTO(includeElementos)).ToList();
                return Ok(menuDTOs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener menús: {ex.Message}");
                return StatusCode(500, $"Error al obtener menús: {ex.Message}");
            }
        }

        // GET: api/menus/proveedor/5
        [HttpGet("proveedor/{proveedorId}")]
        public async Task<ActionResult<IEnumerable<MenuDTO>>> GetMenusByProveedor(int proveedorId, [FromQuery] bool includeElementos = false)
        {
            try
            {
                List<Menu> menus;

                if (includeElementos)
                {
                    menus = await _context.Menus.AsNoTracking()
                        .Where(m => m.ProveedorId == proveedorId)
                        .Include(m => m.Elementos.OrderBy(e => e.Orden))
                        .ThenInclude(e => e.Disponibilidades)
                        .Include(m => m.Proveedor)
                        .ToListAsync();
                }
                else
                {
                    menus = await _context.Menus.AsNoTracking()
                        .Where(m => m.ProveedorId == proveedorId)
                        .Include(m => m.Proveedor)
                        .ToListAsync();
                }

                // Convertir a DTO
                var menuDTOs = menus.Select(m => m.ToDTO(includeElementos)).ToList();
                return Ok(menuDTOs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener menús del proveedor {proveedorId}: {ex.Message}");
                return StatusCode(500, $"Error al obtener menús del proveedor: {ex.Message}");
            }
        }

        // GET: api/menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDTO>> GetMenu(int id, [FromQuery] bool includeElementos = false)
        {
            try
            {
                IQueryable<Menu> query = _context.Menus.AsNoTracking();

                if (includeElementos)
                {
                    query = query
                        .Include(m => m.Elementos.OrderBy(e => e.Orden))
                        .ThenInclude(e => e.Disponibilidades)
                        .Include(m => m.Proveedor);
                }
                else
                {
                    query = query.Include(m => m.Proveedor);
                }

                var menu = await query.FirstOrDefaultAsync(m => m.Id == id);

                if (menu == null)
                    return NotFound();

                // Convertir a DTO
                var menuDTO = menu.ToDTO(includeElementos);
                return Ok(menuDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el menú {id}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Error interno: {ex.InnerException.Message}");
                }
                return StatusCode(500, $"Error al obtener el menú: {ex.Message}");
            }
        }

        // POST: api/menus
        [HttpPost]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<ActionResult<MenuDTO>> CreateMenu([FromBody] MenuDTO menuDTO)
        {
            try
            {
                // Convertir DTO a entidad
                var menu = menuDTO.ToEntity();

                // Validar que el proveedor existe
                var proveedorExists = await _context.Proveedores.AnyAsync(p => p.Id == menu.ProveedorId);
                if (!proveedorExists)
                    return BadRequest("Proveedor no encontrado");

                // Verificar que el usuario actual tenga acceso a este proveedor
                if (User.IsInRole("Proveedor"))
                {
                    var userId = User.Claims
                        .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                        .Skip(1)
                        .FirstOrDefault()?.Value;
                    var proveedor = await _context.Proveedores.FirstOrDefaultAsync(p => p.Id == menu.ProveedorId);

                    if (proveedor.UserId != userId)
                        return Forbid();
                }

                _context.Menus.Add(menu);
                await _context.SaveChangesAsync();

                // Obtener el menú completo para retornar
                var menuCreado = await _context.Menus
                    .Include(m => m.Proveedor)
                    .FirstOrDefaultAsync(m => m.Id == menu.Id);

                // Convertir a DTO para la respuesta
                var menuCreadoDTO = menuCreado.ToDTO(false);

                return CreatedAtAction(nameof(GetMenu), new { id = menuCreadoDTO.Id }, menuCreadoDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear menú: {ex.Message}");
                return StatusCode(500, $"Error al crear menú: {ex.Message}");
            }
        }

        // PUT: api/menus/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<IActionResult> UpdateMenu(int id, MenuDTO menuDTO)
        {
            try
            {
                if (id != menuDTO.Id)
                    return BadRequest();

                // Verificar que el menú existe
                var existingMenu = await _context.Menus.FindAsync(id);
                if (existingMenu == null)
                    return NotFound();

                // Verificar que el usuario actual tenga acceso a este proveedor
                if (User.IsInRole("Proveedor"))
                {
                    var userId = User.Claims
                        .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                        .Skip(1)
                        .FirstOrDefault()?.Value;
                    var proveedor = await _context.Proveedores.FirstOrDefaultAsync(p => p.Id == menuDTO.ProveedorId);

                    if (proveedor.UserId != userId)
                        return Forbid();
                }

                // Actualizar las propiedades
                existingMenu.Nombre = menuDTO.Nombre;
                existingMenu.Descripcion = menuDTO.Descripcion;
                existingMenu.FechaInicio = menuDTO.FechaInicio;
                existingMenu.FechaFin = menuDTO.FechaFin;
                existingMenu.Tipo = menuDTO.Tipo;
                existingMenu.Estado = menuDTO.Estado;
                existingMenu.ProveedorId = menuDTO.ProveedorId;

                _context.Entry(existingMenu).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar menú {id}: {ex.Message}");
                return StatusCode(500, $"Error al actualizar menú: {ex.Message}");
            }
        }

        // PUT: api/menus/5/publicar
        [HttpPut("{id}/publicar")]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<IActionResult> PublicarMenu(int id)
        {
            try
            {
                var menu = await _context.Menus.FindAsync(id);

                if (menu == null)
                    return NotFound();

                // Verificar que el usuario actual tenga acceso a este proveedor
                if (User.IsInRole("Proveedor"))
                {
                    var userId = User.Claims
                        .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                        .Skip(1)
                        .FirstOrDefault()?.Value;
                    var proveedor = await _context.Proveedores.FirstOrDefaultAsync(p => p.Id == menu.ProveedorId);

                    if (proveedor.UserId != userId)
                        return Forbid();
                }

                menu.Estado = EstadoMenu.Publicado;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al publicar menú {id}: {ex.Message}");
                return StatusCode(500, $"Error al publicar menú: {ex.Message}");
            }
        }

        // PUT: api/menus/5/desactivar
        [HttpPut("{id}/desactivar")]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<IActionResult> DesactivarMenu(int id)
        {
            try
            {
                var menu = await _context.Menus.FindAsync(id);

                if (menu == null)
                    return NotFound();

                // Verificar que el usuario actual tenga acceso a este proveedor
                if (User.IsInRole("Proveedor"))
                {
                    var userId = User.Claims
                        .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                        .Skip(1)
                        .FirstOrDefault()?.Value;
                    var proveedor = await _context.Proveedores.FirstOrDefaultAsync(p => p.Id == menu.ProveedorId);

                    if (proveedor.UserId != userId)
                        return Forbid();
                }

                menu.Estado = EstadoMenu.Inactivo;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al desactivar menú {id}: {ex.Message}");
                return StatusCode(500, $"Error al desactivar menú: {ex.Message}");
            }
        }

        // DELETE: api/menus/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Proveedor")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            try
            {
                var menu = await _context.Menus.FindAsync(id);

                if (menu == null)
                    return NotFound();

                // Verificar que el usuario actual tenga acceso a este proveedor
                if (User.IsInRole("Proveedor"))
                {
                    var userId = User.Claims
                        .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                        .Skip(1)
                        .FirstOrDefault()?.Value;
                    var proveedor = await _context.Proveedores.FirstOrDefaultAsync(p => p.Id == menu.ProveedorId);

                    if (proveedor.UserId != userId)
                        return Forbid();
                }

                _context.Menus.Remove(menu);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar menú {id}: {ex.Message}");
                return StatusCode(500, $"Error al eliminar menú: {ex.Message}");
            }
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }

        [HttpGet("validar-proveedor/{proveedorId}/elemento/{elementoMenuId}")]
        public async Task<ActionResult<bool>> ValidarProveedorTieneElemento(int proveedorId, int elementoMenuId)
        {
            var tieneElemento = await _context.ElementosMenu
                .AnyAsync(e => e.Id == elementoMenuId && e.Menu.ProveedorId == proveedorId);

            return Ok(tieneElemento);
        }
    }
}