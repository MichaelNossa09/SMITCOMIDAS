using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Models.DTOs
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public TipoMenu Tipo { get; set; }
        public EstadoMenu Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int ProveedorId { get; set; }
        public ProveedorDTO? Proveedor { get; set; }
        public List<ElementoMenuDTO>? Elementos { get; set; }
    }

    public class ProveedorDTO
    {
        public int Id { get; set; }
        public string NombreComercial { get; set; } = string.Empty;
        public string userId { get; set; } = string.Empty;
        // Otras propiedades del proveedor si las necesitas
    }
}
