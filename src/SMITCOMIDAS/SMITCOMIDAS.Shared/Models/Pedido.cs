using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public int? CentroCostoId { get; set; }
        public DateTime FechaPedido { get; set; } = DateTime.Now;
        public DateTime FechaEntrega { get; set; }
        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendiente;
        public decimal Total { get; set; }
        public string? Comentarios { get; set; }
        public string? MotivoDevolucion { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public bool DescontadoDeCuota { get; set; } = false;

        // Elementos del pedido
        public ApplicationUser Usuario { get; set; }
        public CentroCosto? CentroCosto { get; set; }
        public List<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }

    public enum EstadoPedido
    {
        Pendiente,
        Confirmado,
        EnPreparacion,
        Listo,
        Entregado,
        Recibido,
        Devuelto,
        Cancelado
    }
}
