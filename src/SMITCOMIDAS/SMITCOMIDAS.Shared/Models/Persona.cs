using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Models
{
    public class Persona
    {
        public int Id { get; set; }

        public string? UserId { get; set; }

        [Required(ErrorMessage = "La identificación es requerida")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El tipo de identificación es requerido")]
        public string TipoIdentificacion { get; set; }

        [Required(ErrorMessage = "Los nombres son requeridos")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son requeridos")]
        public string Apellidos { get; set; }

        public string? Cargo { get; set; }
        public int? CentroCostoId { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        public string Telefono { get; set; }

        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        public string? TelefonoAdicional { get; set; }

        public string? Direccion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Activo { get; set; }
        public int MaxPedidosMes { get; set; } = 5;
        public int PedidosRestantesMes { get; set; } = 5;

        public DateTime UltimaActualizacionPedidos { get; set; } = DateTime.Now;

        // Navegación
        public ApplicationUser? User { get; set; }
        public CentroCosto? CentroCosto { get; set; }
    }

    public class CentroCosto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int CompaniaId { get; set; }
        public bool Activo { get; set; }

        public Compania? Compania { get; set; }
    }

    public class Compania
    {
        public int Id { get; set; }
        public string NIT { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public bool Activa { get; set; }
    }
}
