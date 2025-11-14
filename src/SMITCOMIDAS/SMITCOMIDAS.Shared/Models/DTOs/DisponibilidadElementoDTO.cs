using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Models.DTOs
{
    public class DisponibilidadElementoDTO
    {
        public int Id { get; set; }
        public DiaSemana Dia { get; set; }
        public bool DisponibleDesayuno { get; set; }
        public bool DisponibleAlmuerzo { get; set; }
        public bool DisponibleCena { get; set; }
        public int? CantidadDisponible { get; set; }
        public int ElementoMenuId { get; set; }
    }
}
