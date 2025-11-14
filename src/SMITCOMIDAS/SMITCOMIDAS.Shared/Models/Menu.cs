using SMITCOMIDAS.Shared.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Models
{
    public class Menu
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del menú es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción del menú es obligatoria")]
        [StringLength(255, ErrorMessage = "La descripción no puede exceder 255 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de inicio")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de finalización es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de finalización")]
        [DateGreaterThan("FechaInicio", ErrorMessage = "La fecha de finalización debe ser posterior a la fecha de inicio")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El tipo de menú es obligatorio")]
        public TipoMenu Tipo { get; set; }

        public EstadoMenu Estado { get; set; } = EstadoMenu.Borrador;

        [DataType(DataType.DateTime)]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Debe seleccionar un proveedor")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un proveedor válido")]
        public int ProveedorId { get; set; }

        public Proveedor? Proveedor { get; set; }

        public ICollection<ElementoMenu> Elementos { get; set; } = new List<ElementoMenu>();
    }
    public enum TipoMenu
    {
        [Display(Name = "Diario")]
        Diario,
        [Display(Name = "Semanal")]
        Semanal
    }

    public enum EstadoMenu
    {
        [Display(Name = "Borrador")]
        Borrador,
        [Display(Name = "Publicado")]
        Publicado,
        [Display(Name = "Inactivo")]
        Inactivo
    }

    public class ElementoMenu
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 100000, ErrorMessage = "El precio debe ser mayor que cero y menor a 100,000")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio debe tener máximo 2 decimales")]
        public decimal Precio { get; set; }

        [StringLength(50, ErrorMessage = "La categoría no puede exceder 50 caracteres")]
        public string? Categoria { get; set; }

        public TipoComida TipoComida { get; set; }

        [Url(ErrorMessage = "Ingrese una URL válida para la imagen")]
        public string? ImagenUrl { get; set; }
        public bool Disponible { get; set; } = true;
        [Range(0, 1000, ErrorMessage = "El orden debe estar entre 0 y 1000")]
        public int Orden { get; set; }

        // Relaciones
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }
        public ICollection<DisponibilidadElemento> Disponibilidades { get; set; } = new List<DisponibilidadElemento>();
    }


    public class DisponibilidadElemento
    {
        public int Id { get; set; }
        public DiaSemana Dia { get; set; }
        public bool DisponibleDesayuno { get; set; } = true;
        public bool DisponibleAlmuerzo { get; set; } = true;
        public bool DisponibleCena { get; set; } = true;
        [Range(0, 10000, ErrorMessage = "La cantidad disponible debe estar entre 0 y 10,000")]
        public int? CantidadDisponible { get; set; }

        // Relación con ElementoMenu
        public int ElementoMenuId { get; set; }
        public ElementoMenu? ElementoMenu { get; set; }
    }

    public enum TipoComida
    {
        Desayuno = 0,
        Almuerzo = 1,
        Cena = 2,
        Otro = 3
    }
    public enum DiaSemana
    {
        Lunes,
        Martes,
        Miercoles,
        Jueves,
        Viernes,
        Sabado,
        Domingo
    }
}
