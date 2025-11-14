using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Models.DTOs
{
    public class ElementoMenuDTO
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string? Categoria { get; set; }
        public TipoComida TipoComida { get; set; }
        public string? ImagenUrl { get; set; }
        public bool Disponible { get; set; } = true;
        public int Orden { get; set; }
        public int MenuId { get; set; }
        public int ProveedorId { get; set; }
        public List<DisponibilidadElementoDTO>? Disponibilidades { get; set; }
    }
}
