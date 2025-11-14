using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Models
{
    public class RegistroPersonaRequest
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder 100 caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$",
        ErrorMessage = "La contraseña debe contener al menos una letra minúscula, una letra mayúscula, un número y un carácter especial")]
        public string Password { get; set; }
        [Required(ErrorMessage = "La confirmación de contraseña es obligatoria")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "El rol es obligatorio")]
        [RegularExpression("^(Operativo|Administrativo)$", ErrorMessage = "Rol inválido. Seleccione Operativo o Administrativo")]
        public string Rol { get; set; }

        [Required(ErrorMessage = "La identificación es obligatoria")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "La identificación debe tener entre 5 y 20 caracteres")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El tipo de identificación es obligatorio")]
        public string TipoIdentificacion { get; set; }
        [Required(ErrorMessage = "Los nombres son obligatorios")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Los nombres deben tener entre 2 y 100 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Los nombres solo pueden contener letras y espacios")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Los apellidos deben tener entre 2 y 100 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Los apellidos solo pueden contener letras y espacios")]
        public string Apellidos { get; set; }
        [StringLength(100, ErrorMessage = "El cargo no puede exceder 100 caracteres")]
        public string? Cargo { get; set; }
        public int? CentroCostoId { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "Ingrese un número de teléfono válido")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "El teléfono debe tener entre 7 y 20 caracteres")]
        public string Telefono { get; set; }

        [Phone(ErrorMessage = "Ingrese un número de teléfono adicional válido")]
        [StringLength(20, ErrorMessage = "El teléfono adicional no puede exceder 20 caracteres")]
        public string? TelefonoAdicional { get; set; }

        [StringLength(200, ErrorMessage = "La dirección no puede exceder 200 caracteres")]
        public string? Direccion { get; set; }

        public int MaxPedidosMes { get; set; } = 5;
    }
}
