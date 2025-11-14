using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Models.DTOs
{
    public class PersonaDTO
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public string Identificacion { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string? Cargo { get; set; }
        public int CentroCostoId { get; set; }
        public string? Telefono { get; set; }
        public string? TelefonoAdicional { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Activo { get; set; }
        public int MaxPedidosMes { get; set; }
        public int PedidosRestantesMes { get; set; }
        public DateTime UltimaActualizacionPedidos { get; set; }
    }
}
