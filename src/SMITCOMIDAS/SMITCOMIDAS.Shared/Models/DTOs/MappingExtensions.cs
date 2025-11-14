using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Models.DTOs
{
    public static class MappingExtensions
    {
        public static MenuDTO ToDTO(this Menu menu, bool includeElementos = false)
        {
            if (menu == null) return null;

            var dto = new MenuDTO
            {
                Id = menu.Id,
                Nombre = menu.Nombre ?? string.Empty,
                Descripcion = menu.Descripcion ?? string.Empty,
                FechaInicio = menu.FechaInicio,
                FechaFin = menu.FechaFin,
                Tipo = menu.Tipo,
                Estado = menu.Estado,
                FechaCreacion = menu.FechaCreacion,
                ProveedorId = menu.ProveedorId,
                Proveedor = menu.Proveedor != null ? menu.Proveedor.ToDTO() : null
            };

            if (includeElementos && menu.Elementos != null)
            {
                dto.Elementos = menu.Elementos.Select(e => e.ToDTO(true)).ToList();
            }

            return dto;
        }

        // PROVEEDOR: Entidad a DTO
        public static ProveedorDTO ToDTO(this Proveedor proveedor)
        {
            if (proveedor == null) return null;

            return new ProveedorDTO
            {
                Id = proveedor.Id,
                NombreComercial = proveedor.NombreComercial ?? string.Empty,
                userId = proveedor.UserId
                // Otras propiedades necesarias
            };
        }

        // ELEMENTO MENU: Entidad a DTO
        public static ElementoMenuDTO ToDTO(this ElementoMenu elemento, bool includeDisponibilidades = false)
        {
            if (elemento == null) return null;

            var dto = new ElementoMenuDTO
            {
                Id = elemento.Id,
                Nombre = elemento.Nombre ?? string.Empty,
                Descripcion = elemento.Descripcion ?? string.Empty,
                Precio = elemento.Precio,
                Categoria = elemento.Categoria,
                TipoComida = elemento.TipoComida,
                ImagenUrl = elemento.ImagenUrl,
                Disponible = elemento.Disponible,
                Orden = elemento.Orden,
                MenuId = elemento.MenuId
            };

            if (includeDisponibilidades && elemento.Disponibilidades != null)
            {
                dto.Disponibilidades = elemento.Disponibilidades.Select(d => d.ToDTO()).ToList();
            }

            return dto;
        }

        // DISPONIBILIDAD ELEMENTO: Entidad a DTO
        public static DisponibilidadElementoDTO ToDTO(this DisponibilidadElemento disponibilidad)
        {
            if (disponibilidad == null) return null;

            return new DisponibilidadElementoDTO
            {
                Id = disponibilidad.Id,
                Dia = disponibilidad.Dia,
                DisponibleDesayuno = disponibilidad.DisponibleDesayuno,
                DisponibleAlmuerzo = disponibilidad.DisponibleAlmuerzo,
                DisponibleCena = disponibilidad.DisponibleCena,
                CantidadDisponible = disponibilidad.CantidadDisponible,
                ElementoMenuId = disponibilidad.ElementoMenuId
            };
        }

        // PEDIDO: Entidad a DTO
        public static PedidoDTO ToDTO(this Pedido pedido)
        {
            if (pedido == null) return null;

            return new PedidoDTO
            {
                Id = pedido.Id,
                UsuarioId = pedido.UsuarioId.ToString(),
                FechaPedido = pedido.FechaPedido,
                FechaEntrega = pedido.FechaEntrega,
                Estado = pedido.Estado,
                Total = pedido.Total,
                Comentarios = pedido.Comentarios,
                Detalles = pedido.Detalles?.Select(d => d.ToDTO()).ToList(),
                CentroCostoId = pedido.CentroCostoId,
                CentroCosto  = pedido.CentroCosto,
                Usuario = pedido.Usuario,
            };
        }

        // DETALLE PEDIDO: Entidad a DTO
        public static DetallePedidoDTO ToDTO(this DetallePedido detalle)
        {
            if (detalle == null) return null;

            return new DetallePedidoDTO
            {
                Id = detalle.Id,
                ElementoMenuId = detalle.ElementoMenuId,
                Cantidad = detalle.Cantidad,
                PrecioUnitario = detalle.PrecioUnitario,
                Observaciones = detalle.Observaciones,
                ElementoMenu = detalle.ElementoMenu?.ToDTO(),
            };
        }

        // MENU: DTO a Entidad
        public static Menu ToEntity(this MenuDTO dto)
        {
            if (dto == null) return null;

            var menu = new Menu
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Tipo = dto.Tipo,
                Estado = dto.Estado,
                FechaCreacion = dto.FechaCreacion,
                ProveedorId = dto.ProveedorId,
                Proveedor = dto.Proveedor?.ToEntity()
            };

            if (dto.Elementos != null)
            {
                menu.Elementos = dto.Elementos.Select(e => e.ToEntity()).ToList();
            }

            return menu;
        }

        // PROVEEDOR: DTO a Entidad
        public static Proveedor ToEntity(this ProveedorDTO dto)
        {
            
            if (dto == null) return null;

            return new Proveedor
            {
                Id = dto.Id,
                NombreComercial = dto.NombreComercial,
                UserId = dto.userId
                // Otras propiedades si es necesario
            };
        }

        // ELEMENTO MENU: DTO a Entidad
        public static ElementoMenu ToEntity(this ElementoMenuDTO dto)
        {
            if (dto == null) return null;

            var elemento = new ElementoMenu
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Categoria = dto.Categoria,
                TipoComida = dto.TipoComida,
                ImagenUrl = dto.ImagenUrl,
                Disponible = dto.Disponible,
                Orden = dto.Orden,
                MenuId = dto.MenuId,
            };

            if (dto.Disponibilidades != null)
            {
                elemento.Disponibilidades = dto.Disponibilidades.Select(d => d.ToEntity()).ToList();
            }

            return elemento;
        }

        // DISPONIBILIDAD ELEMENTO: DTO a Entidad
        public static DisponibilidadElemento ToEntity(this DisponibilidadElementoDTO dto)
        {
            if (dto == null) return null;

            return new DisponibilidadElemento
            {
                Id = dto.Id,
                Dia = dto.Dia,
                DisponibleDesayuno = dto.DisponibleDesayuno,
                DisponibleAlmuerzo = dto.DisponibleAlmuerzo,
                DisponibleCena = dto.DisponibleCena,
                CantidadDisponible = dto.CantidadDisponible,
                ElementoMenuId = dto.ElementoMenuId
            };
        }

        // PEDIDO: DTO a Entidad
        public static Pedido ToEntity(this PedidoDTO dto)
        {
            if (dto == null) return null;

            return new Pedido
            {
                Id = dto.Id,
                UsuarioId = dto.UsuarioId,
                FechaPedido = dto.FechaPedido,
                FechaEntrega = dto.FechaEntrega,
                Estado = dto.Estado,
                Total = dto.Total,
                Comentarios = dto.Comentarios,
                Detalles = dto.Detalles?.Select(d => d.ToEntity()).ToList() ?? new List<DetallePedido>(),
                CentroCostoId = dto.CentroCostoId,
                CentroCosto = dto.CentroCosto,
                Usuario = dto.Usuario,
            };
        }

        // DETALLE PEDIDO: DTO a Entidad
        public static DetallePedido ToEntity(this DetallePedidoDTO dto)
        {
            if (dto == null) return null;

            return new DetallePedido
            {
                Id = dto.Id,
                ElementoMenuId = dto.ElementoMenuId,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
                Observaciones = dto.Observaciones,
                ElementoMenu = dto.ElementoMenu?.ToEntity()
                // No asignamos ElementoMenu para evitar ciclos infinitos
            };
        }

        // Métodos adicionales para PedidoResumenDTO y PedidoDetalladoDTO
        public static PedidoResumenDTO ToResumenDTO(this Pedido pedido)
        {
            if (pedido == null) return null;

            return new PedidoResumenDTO
            {
                Id = pedido.Id,
                FechaPedido = pedido.FechaPedido,
                FechaEntrega = pedido.FechaEntrega,
                Estado = pedido.Estado,
                Total = pedido.Total,
                ProveedorNombre = pedido.Detalles?.FirstOrDefault()?.ElementoMenu?.Menu?.Proveedor?.NombreComercial ?? "Varios proveedores",
                NumeroElementos = pedido.Detalles?.Count ?? 0
            };
        }

        public static PedidoDetalladoDTO ToDetalladoDTO(this Pedido pedido)
        {
            if (pedido == null) return null;

            return new PedidoDetalladoDTO
            {
                Id = pedido.Id,
                UsuarioNombre = pedido.Usuario?.UserName ?? "Usuario desconocido",
                FechaPedido = pedido.FechaPedido,
                FechaEntrega = pedido.FechaEntrega,
                Estado = pedido.Estado,
                Total = pedido.Total,
                Comentarios = pedido.Comentarios,
                Detalles = pedido.Detalles?.Select(d => d.ToDTO()).ToList() ?? new List<DetallePedidoDTO>()
            };
        }




    }
}
