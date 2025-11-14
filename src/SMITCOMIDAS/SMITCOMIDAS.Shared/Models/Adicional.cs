using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Models
{
    public class Adicional
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public bool Consumido { get; set; }
    }
}
