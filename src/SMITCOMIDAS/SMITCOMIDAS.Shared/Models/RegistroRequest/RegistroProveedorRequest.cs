using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Models.RegistroRequest
{
    public class RegistroProveedorRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Rol { get; set; } = "Proveedor";


        // Datos de proveedor
        public string NIT { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string Telefono { get; set; }
        public string? TelefonoAdicional { get; set; }
        public string? EmailAdicional { get; set; }
        public string Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Departamento { get; set; }
        public string? Pais { get; set; } = "Colombia";
        public string? Contacto { get; set; }
        public string? TelefonoContacto { get; set; }
    }
}
