using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Models.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public int? CentroCostoId { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public EstadoPedido Estado { get; set; }
        public decimal Total { get; set; }
        public string? Comentarios { get; set; }
        public string? MotivoDevolucion { get; set; } 
        public DateTime? FechaRecepcion { get; set; } 
        public ApplicationUser? Usuario { get; set; }
        public List<DetallePedidoDTO>? Detalles { get; set; }
        public CentroCosto? CentroCosto { get; set; }
    }

    /// <summary>
    /// DTO para los detalles de un pedido
    /// </summary>
    public class DetallePedidoDTO
    {
        public int Id { get; set; }
        public int PedidoId { get; set; } // AGREGADO

        [Required(ErrorMessage = "El elemento es obligatorio")]
        public int ElementoMenuId { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, 10, ErrorMessage = "La cantidad debe estar entre 1 y 10")]
        public int Cantidad { get; set; } = 1;

        public decimal PrecioUnitario { get; set; }

        public string? Observaciones { get; set; }

        // Propiedades de navegación para mostrar información en la UI
        public ElementoMenuDTO? ElementoMenu { get; set; }
    }

    /// <summary>
    /// DTO para mostrar resumen de pedidos
    /// </summary>
    public class PedidoResumenDTO
    {
        public int Id { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public EstadoPedido Estado { get; set; }
        public decimal Total { get; set; }
        public string? ProveedorNombre { get; set; }
        public int NumeroElementos { get; set; }
    }

    /// <summary>
    /// DTO para mostrar detalles completos de un pedido
    /// </summary>
    public class PedidoDetalladoDTO
    {
        public int Id { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;
        public DateTime FechaPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public EstadoPedido Estado { get; set; }
        public string EstadoNombre => Estado.ToString();
        public decimal Total { get; set; }
        public string? Comentarios { get; set; }
        public List<DetallePedidoDTO> Detalles { get; set; } = new List<DetallePedidoDTO>();
        public bool PuedeCancelarse =>
            (Estado == EstadoPedido.Pendiente || Estado == EstadoPedido.Confirmado) &&
            FechaEntrega > DateTime.Now.AddHours(2);
    }
}
