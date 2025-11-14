using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Models
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string NIT { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string Telefono { get; set; }
        public string? TelefonoAdicional { get; set; }
        public string Email { get; set; }
        public string? EmailAdicional { get; set; }
        public string Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Departamento { get; set; }
        public string? Pais { get; set; } = "Colombia";
        public string? Contacto { get; set; }
        public string? TelefonoContacto { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación con Usuario (si el proveedor tiene acceso al sistema)
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
