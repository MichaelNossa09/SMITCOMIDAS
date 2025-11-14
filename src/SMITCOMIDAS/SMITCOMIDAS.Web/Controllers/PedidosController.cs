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
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/pedidos
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetPedidos()
        {
            try
            {
                var pedidos = await _context.Pedidos.AsNoTracking()
                    .Include(p => p.Usuario)
                    .Include(p => p.Detalles)
                        .ThenInclude(d => d.ElementoMenu)
                            .ThenInclude(e => e.Menu)
                                .ThenInclude(m => m.Proveedor)
                    .OrderByDescending(p => p.FechaPedido)
                    .ToListAsync();

                // Convertir a DTO
                var pedidoDTOs = pedidos.Select(p => p.ToDTO()).ToList();
                return Ok(pedidoDTOs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener pedidos: {ex.Message}");
                return StatusCode(500, $"Error al obtener pedidos: {ex.Message}");
            }
        }

        // GET: api/pedidos/usuario
        [HttpGet("usuario")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetPedidosUsuario()
        {
            try
            {
                // Obtener el ID del usuario (GUID), saltando el primer claim
                var userId = User.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Skip(1)
                    .FirstOrDefault()?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                // Usar el GUID con el campo UsuarioId1 que es nvarchar(450)
                var pedidos = await _context.Pedidos.AsNoTracking()
                    .Include(p => p.Detalles)
                        .ThenInclude(d => d.ElementoMenu)
                            .ThenInclude(e => e.Menu)
                                .ThenInclude(m => m.Proveedor)
                    .Where(p => p.UsuarioId == userId)
                    .OrderByDescending(p => p.FechaPedido)
                    .ToListAsync();

                // Convertir a DTO
                var pedidoDTOs = pedidos.Select(p => p.ToDTO()).ToList();
                return Ok(pedidoDTOs);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener pedidos del usuario: {ex.Message}");
                return StatusCode(500, $"Error al obtener pedidos: {ex.Message}");
            }
        }

        // GET: api/pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDTO>> GetPedido(int id)
        {
            try
            {
                var userId = User.Claims
                    .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                    .Skip(1)
                    .FirstOrDefault()?.Value;

                var isAdmin = User.IsInRole("Admin");
                var isProveedor = User.IsInRole("Proveedor");

                // Proyección directa a DTO
                var pedidoDTO = await _context.Pedidos.AsNoTracking()
                    .Where(p => p.Id == id)
                    .Select(p => new PedidoDTO
                    {
                        Id = p.Id,
                        UsuarioId = p.UsuarioId,
                        CentroCostoId = p.CentroCostoId,
                        FechaPedido = p.FechaPedido,
                        FechaEntrega = p.FechaEntrega,
                        Estado = p.Estado,
                        Total = p.Total,
                        Comentarios = p.Comentarios,
                        MotivoDevolucion = p.MotivoDevolucion,
                        FechaRecepcion = p.FechaRecepcion,
                        Usuario = p.Usuario != null ? new ApplicationUser
                        {
                            Id = p.Usuario.Id,
                            FullName = p.Usuario.FullName,
                            Email = p.Usuario.Email
                        } : null,
                        CentroCosto = p.CentroCosto != null ? new CentroCosto
                        {
                            Id = p.CentroCosto.Id,
                            Nombre = p.CentroCosto.Nombre,
                            Codigo = p.CentroCosto.Codigo
                        } : null,
                        Detalles = p.Detalles.Select(d => new DetallePedidoDTO
                        {
                            Id = d.Id,
                            PedidoId = d.PedidoId,
                            ElementoMenuId = d.ElementoMenuId,
                            Cantidad = d.Cantidad,
                            PrecioUnitario = d.PrecioUnitario,
                            Observaciones = d.Observaciones,
                            ElementoMenu = d.ElementoMenu != null ? new ElementoMenuDTO
                            {
                                Id = d.ElementoMenu.Id,
                                MenuId = d.ElementoMenu.MenuId,
                                ProveedorId = d.ElementoMenu.Menu.ProveedorId,
                                Nombre = d.ElementoMenu.Nombre,
                                Descripcion = d.ElementoMenu.Descripcion,
                                Precio = d.ElementoMenu.Precio,
                                Categoria = d.ElementoMenu.Categoria,
                                TipoComida = d.ElementoMenu.TipoComida,
                                ImagenUrl = d.ElementoMenu.ImagenUrl,
                                Disponible = d.ElementoMenu.Disponible,
                                Orden = d.ElementoMenu.Orden
                            } : null
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (pedidoDTO == null)
                    return NotFound();

                // Verificar permisos usando el DTO
                bool tienePermiso = false;

                if (isAdmin)
                {
                    tienePermiso = true;
                }
                else if (pedidoDTO.UsuarioId == userId)
                {
                    tienePermiso = true;
                }
                else if (isProveedor)
                {
                    var proveedorId = await _context.Proveedores
                        .Where(p => p.UserId == userId)
                        .Select(p => p.Id)
                        .FirstOrDefaultAsync();

                    if (proveedorId > 0)
                    {
                        tienePermiso = pedidoDTO.Detalles.Any(d => d.ElementoMenu?.ProveedorId == proveedorId);
                    }
                }

                if (!tienePermiso)
                    return Forbid();

                return Ok(pedidoDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el pedido {id}: {ex.Message}");
                return StatusCode(500, $"Error al obtener el pedido: {ex.Message}");
            }
        }
        // GET: api/pedidos/menus-disponibles
        [HttpGet("menus-disponibles")]
        public async Task<ActionResult<IEnumerable<MenuDTO>>> GetMenusDisponibles([FromQuery] DateTime fecha, [FromQuery] int proveedorId = 0)
        {
            try
            {
                var query = _context.Menus.AsNoTracking()
                    .Include(m => m.Proveedor)
                    .Where(m =>
                        m.Estado == EstadoMenu.Publicado &&
                        m.FechaInicio <= fecha &&
                        m.FechaFin >= fecha);

                // Filtrar por proveedor si se especifica
                if (proveedorId > 0)
                {
                    query = query.Where(m => m.ProveedorId == proveedorId);
                }

                var menus = await query.ToListAsync();

                // Convertir a DTO
                var menuDTOs = menus.Select(m => m.ToDTO(false)).ToList();
                return Ok(menuDTOs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener menús disponibles: {ex.Message}");
                return StatusCode(500, $"Error al obtener menús disponibles: {ex.Message}");
            }
        }

        // GET: api/pedidos/elementos-disponibles/5
        [HttpGet("elementos-disponibles/{menuId}")]
        public async Task<ActionResult<IEnumerable<ElementoMenuDTO>>> GetElementosDisponibles(int menuId, [FromQuery] DateTime fecha)
        {
            try
            {
                // Obtener el menú con sus elementos y disponibilidades
                var menu = await _context.Menus.AsNoTracking()
                    .Include(m => m.Elementos)
                        .ThenInclude(e => e.Disponibilidades)
                    .FirstOrDefaultAsync(m => m.Id == menuId);

                if (menu == null)
                    return NotFound("Menú no encontrado");

                DiaSemana diaSemana;
                if (fecha.DayOfWeek == DayOfWeek.Sunday)
                    diaSemana = DiaSemana.Domingo;
                else
                    diaSemana = (DiaSemana)((int)fecha.DayOfWeek - 1);

                // Filtrar elementos disponibles para ese día
                var elementosDisponibles = menu.Elementos
                    .Where(e => e.Disponible &&
                                e.Disponibilidades.Any(d => d.Dia == diaSemana &&
                                                      (d.DisponibleAlmuerzo || d.DisponibleCena || d.DisponibleDesayuno)))
                    .ToList();

                // Convertir a DTO
                var elementoDTOs = elementosDisponibles.Select(e => e.ToDTO()).ToList();
                return Ok(elementoDTOs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener elementos disponibles: {ex.Message}");
                return StatusCode(500, $"Error al obtener elementos disponibles: {ex.Message}");
            }
        }

        // POST: api/pedidos
        [HttpPost]
        public async Task<ActionResult<PedidoDTO>> CrearPedido([FromBody] PedidoDTO pedidoDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (string.IsNullOrEmpty(pedidoDTO.UsuarioId))
                    return Unauthorized();

                // Verificar que la fecha de entrega sea válida (futura)
                if (pedidoDTO.FechaEntrega.Date < DateTime.Now.Date)
                    return BadRequest("La fecha de entrega debe ser futura.");

                // Verificar que el pedido tenga detalles
                if (pedidoDTO.Detalles == null || !pedidoDTO.Detalles.Any())
                    return BadRequest("El pedido debe tener al menos un elemento.");

                // Obtener el centro de costo del usuario
                var persona = await _context.Personas
                    .FirstOrDefaultAsync(p => p.UserId == pedidoDTO.UsuarioId);

                if (persona != null)
                {
                    pedidoDTO.CentroCostoId = persona.CentroCostoId;
                }
                else
                {
                    pedidoDTO.CentroCostoId = 1;
                }

                // Verificar que el usuario pueda realizar el pedido (1 elemento por usuario, a menos que tenga rol especial)
                var tieneRolEspecial = User.IsInRole("Admin");

                if (!tieneRolEspecial)
                {
                    foreach (var detalle in pedidoDTO.Detalles)
                    {
                        var pedidoExistente = await _context.Pedidos
                            .Include(p => p.Detalles)
                            .Where(p =>
                                p.UsuarioId == pedidoDTO.UsuarioId &&
                                p.Estado != EstadoPedido.Cancelado &&
                                p.FechaEntrega.Date == pedidoDTO.FechaEntrega.Date &&
                                p.Detalles.Any(d => d.ElementoMenuId == detalle.ElementoMenuId))
                            .AnyAsync();

                        if (pedidoExistente)
                            return BadRequest($"Ya tienes un pedido para el elemento con ID {detalle.ElementoMenuId} en la fecha seleccionada.");
                    }
                }

                // Verificar disponibilidad y obtener precios
                foreach (var detalle in pedidoDTO.Detalles)
                {
                    var elemento = await _context.ElementosMenu
                        .Include(e => e.Disponibilidades)
                        .Include(e => e.Menu)
                        .FirstOrDefaultAsync(e => e.Id == detalle.ElementoMenuId);

                    if (elemento == null)
                        return BadRequest($"El elemento con ID {detalle.ElementoMenuId} no existe.");

                    if (elemento.Menu.FechaInicio > pedidoDTO.FechaEntrega || elemento.Menu.FechaFin < pedidoDTO.FechaEntrega)
                        return BadRequest($"El elemento '{elemento.Nombre}' no está disponible en la fecha seleccionada.");

                    DiaSemana diaSemana;
                    if (pedidoDTO.FechaEntrega.DayOfWeek == DayOfWeek.Sunday)
                        diaSemana = DiaSemana.Domingo;
                    else
                        diaSemana = (DiaSemana)((int)pedidoDTO.FechaEntrega.DayOfWeek - 1);

                    if ((int)diaSemana == 0) diaSemana = DiaSemana.Domingo;

                    var disponibilidad = elemento.Disponibilidades
                        .FirstOrDefault(d => d.Dia == diaSemana &&
                                       (d.DisponibleAlmuerzo || d.DisponibleCena));

                    if (disponibilidad == null)
                        return BadRequest($"El elemento '{elemento.Nombre}' no está disponible para la fecha seleccionada.");

                    if (disponibilidad.CantidadDisponible.HasValue)
                    {
                        int cantidadPedida = await _context.DetallesPedido
                            .Include(d => d.Pedido)
                            .Where(d =>
                                d.ElementoMenuId == detalle.ElementoMenuId &&
                                d.Pedido.FechaEntrega.Date == pedidoDTO.FechaEntrega.Date &&
                                d.Pedido.Estado != EstadoPedido.Cancelado)
                            .SumAsync(d => d.Cantidad);

                        if (cantidadPedida + detalle.Cantidad > disponibilidad.CantidadDisponible.Value)
                            return BadRequest($"No hay suficiente cantidad disponible del elemento '{elemento.Nombre}'.");
                    }

                    detalle.PrecioUnitario = elemento.Precio;
                }

                decimal total = pedidoDTO.Detalles.Sum(d => d.PrecioUnitario * d.Cantidad);

                var pedido = new Pedido
                {
                    UsuarioId = pedidoDTO.UsuarioId,
                    CentroCostoId = pedidoDTO.CentroCostoId,
                    FechaPedido = DateTime.Now,
                    FechaEntrega = pedidoDTO.FechaEntrega,
                    Estado = EstadoPedido.Pendiente,
                    Total = total,
                    Comentarios = pedidoDTO.Comentarios,
                    Detalles = pedidoDTO.Detalles.Select(d => new DetallePedido
                    {
                        ElementoMenuId = d.ElementoMenuId,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Observaciones = d.Observaciones
                    }).ToList()
                };

                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();

                var pedidoCreado = await _context.Pedidos
                    .Include(p => p.Usuario)
                    .Include(p => p.CentroCosto)
                    .Include(p => p.Detalles)
                        .ThenInclude(d => d.ElementoMenu)
                    .FirstOrDefaultAsync(p => p.Id == pedido.Id);

                var pedidoCreadoDTO = pedidoCreado.ToDTO();
                return CreatedAtAction(nameof(GetPedido), new { id = pedidoCreadoDTO.Id }, pedidoCreadoDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear pedido: {ex.Message}");
                return StatusCode(500, $"Error al crear pedido: {ex.Message}");
            }
        }

        // PUT: api/pedidos/5/cancelar
        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> CancelarPedido(int id)
        {
            try
            {
                var userId = User.Claims
                         .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                         .Skip(1)
                         .FirstOrDefault()?.Value;
                var isAdmin = User.IsInRole("Admin");

                var pedido = await _context.Pedidos.FindAsync(id);

                // Verificar que el pedido exista
                if (pedido == null)
                    return NotFound("Pedido no encontrado.");

                // Verificar que el pedido pertenezca al usuario (a menos que sea admin)
                if (pedido.UsuarioId.ToString() != userId && !isAdmin)
                    return Forbid();

                // Verificar que el pedido esté en un estado que permita cancelarlo
                if (pedido.Estado != EstadoPedido.Pendiente && pedido.Estado != EstadoPedido.Confirmado)
                    return BadRequest("No se puede cancelar un pedido que ya está en preparación o entregado.");

                // Verificar que el pedido se cancele con al menos 2 horas de antelación a la entrega
                if (pedido.FechaEntrega < DateTime.Now.AddHours(2))
                    return BadRequest("Los pedidos deben cancelarse con al menos 2 horas de antelación.");

                pedido.Estado = EstadoPedido.Cancelado;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cancelar pedido {id}: {ex.Message}");
                return StatusCode(500, $"Error al cancelar pedido: {ex.Message}");
            }
        }






        // GET: api/pedidos/admin/all
        [HttpGet("admin/all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetAllPedidos()
        {
            try
            {
                var pedidos = await _context.Pedidos.AsNoTracking()
                    .Include(p => p.Usuario)
                    .Include(p => p.CentroCosto)
                    .Include(p => p.Detalles)
                        .ThenInclude(d => d.ElementoMenu)
                            .ThenInclude(e => e.Menu)
                                .ThenInclude(m => m.Proveedor)
                    .OrderByDescending(p => p.FechaPedido)
                    .ToListAsync();

                var pedidoDTOs = pedidos.Select(p => p.ToDTO()).ToList();
                return Ok(pedidoDTOs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los pedidos: {ex.Message}");
                return StatusCode(500, $"Error al obtener pedidos: {ex.Message}");
            }
        }

        // GET: api/pedidos/proveedor/{proveedorId}
        [HttpGet("proveedor/{proveedorId}")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetPedidosByProveedorId(int proveedorId)
        {
            try
            {
                var userId = User.Claims
                        .Where(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)
                        .Skip(1)
                        .FirstOrDefault()?.Value;
                var isAdmin = User.IsInRole("Admin");

                // Si no es admin, verificar que el proveedor pertenece al usuario
                if (!isAdmin)
                {
                    var proveedor = await _context.Proveedores
                        .FirstOrDefaultAsync(p => p.Id == proveedorId && p.UserId == userId);

                    if (proveedor == null)
                        return Forbid();
                }

                var pedidos = await _context.Pedidos.AsNoTracking()
                    .Include(p => p.Usuario)
                    .Include(p => p.CentroCosto)
                    .Include(p => p.Detalles)
                        .ThenInclude(d => d.ElementoMenu)
                            .ThenInclude(e => e.Menu)
                    .Where(p => p.Detalles.Any(d => d.ElementoMenu.Menu.ProveedorId == proveedorId))
                    .OrderByDescending(p => p.FechaPedido)
                    .ToListAsync();

                var pedidoDTOs = pedidos.Select(p => p.ToDTO()).ToList();
                return Ok(pedidoDTOs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener pedidos del proveedor: {ex.Message}");
                return StatusCode(500, $"Error al obtener pedidos: {ex.Message}");
            }
        }

        // PUT: api/pedidos/{id}/estado
        [HttpPut("{id}/estado")]
        public async Task<IActionResult> ActualizarEstadoPedido(int id, [FromBody] ActualizarEstadoRequest request)
        {
            try
            {
                var pedido = await _context.Pedidos
                    .Include(p => p.Detalles)
                        .ThenInclude(d => d.ElementoMenu)
                            .ThenInclude(e => e.Menu)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (pedido == null)
                    return NotFound();

                var isAdmin = User.IsInRole("Admin");
                var userId = User.Claims
                            .Where(c => c.Type == ClaimTypes.NameIdentifier)
                            .Skip(1)
                            .FirstOrDefault()?.Value;

                // Verificar permisos
                if (!isAdmin)
                {
                    var isProveedor = User.IsInRole("Proveedor");
                    var isOperativo = User.IsInRole("Operativo");
                    var isAdministrativo = User.IsInRole("Administrativo");

                    if (isOperativo || isAdministrativo)
                    {
                        if (pedido.UsuarioId != userId ||
                        pedido.Estado != EstadoPedido.Entregado ||
                        (request.Estado != EstadoPedido.Recibido && request.Estado != EstadoPedido.Devuelto))
                        {
                            return Forbid();
                        }

                        if (request.Estado == EstadoPedido.Devuelto && string.IsNullOrWhiteSpace(request.MotivoDevolucion))
                        {
                            return BadRequest("El motivo de devolución es obligatorio");
                        }
                    }
                    else if (isProveedor)
                    {
                        var proveedorId = pedido.Detalles.First().ElementoMenu.Menu.ProveedorId;
                        var proveedor = await _context.Proveedores
                            .FirstOrDefaultAsync(p => p.Id == proveedorId && p.UserId == userId);

                        if (proveedor == null)
                            return Forbid();

                        // Validar transiciones permitidas para proveedor
                        if (!(pedido.Estado == EstadoPedido.Confirmado && request.Estado == EstadoPedido.EnPreparacion) &&
                            !(pedido.Estado == EstadoPedido.EnPreparacion && request.Estado == EstadoPedido.Listo) &&
                            !(pedido.Estado == EstadoPedido.Listo && request.Estado == EstadoPedido.Entregado))
                        {
                            return BadRequest("Transición de estado no permitida para proveedor");
                        }
                    }
                    else
                    {
                        return Forbid();
                    }
                }

                pedido.Estado = request.Estado;

                // Si cambia a Recibido y no se había descontado antes
                if (request.Estado == EstadoPedido.Recibido && !pedido.DescontadoDeCuota)
                {
                    var persona = await _context.Personas.FirstOrDefaultAsync(p => p.UserId == pedido.UsuarioId);

                    if (persona != null)
                    {
                        // Verificar actualización mensual
                        var mesActual = DateTime.Now.Month;
                        var añoActual = DateTime.Now.Year;

                        if (persona.UltimaActualizacionPedidos.Month != mesActual ||
                            persona.UltimaActualizacionPedidos.Year != añoActual)
                        {
                            persona.PedidosRestantesMes = persona.MaxPedidosMes;
                            persona.UltimaActualizacionPedidos = DateTime.Now;
                        }

                        persona.PedidosRestantesMes--;
                        pedido.DescontadoDeCuota = true;
                        pedido.FechaRecepcion = DateTime.Now;
                    }
                }

                // Si se devuelve un pedido que ya había sido descontado, reintegrar
                if (request.Estado == EstadoPedido.Devuelto && pedido.DescontadoDeCuota)
                {
                    var persona = await _context.Personas.FirstOrDefaultAsync(p => p.UserId == pedido.UsuarioId);

                    if (persona != null)
                    {
                        persona.PedidosRestantesMes++;
                        pedido.DescontadoDeCuota = false;
                        pedido.MotivoDevolucion = request.MotivoDevolucion;
                    }
                }
                else if (request.Estado == EstadoPedido.Devuelto)
                {
                    pedido.MotivoDevolucion = request.MotivoDevolucion;
                }
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar estado: {ex.Message}");
                return StatusCode(500, $"Error al actualizar estado: {ex.Message}");
            }
        }

        // Clase auxiliar para el request
        public class ActualizarEstadoRequest
        {
            public EstadoPedido Estado { get; set; }
            public string? MotivoDevolucion { get; set; }
        }
    }
}