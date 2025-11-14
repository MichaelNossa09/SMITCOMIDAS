using SMITCOMIDAS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services
{
    public interface IDisponibilidadElementoService
    {
        Task<List<DisponibilidadElemento>> GetDisponibilidadesByElementoIdAsync(int elementoId);
        Task<DisponibilidadElemento> GetDisponibilidadByIdAsync(int id);
        Task<DisponibilidadElemento> CreateDisponibilidadAsync(DisponibilidadElemento disponibilidad);
        Task<DisponibilidadElemento> UpdateDisponibilidadAsync(DisponibilidadElemento disponibilidad);
        Task<bool> DeleteDisponibilidadAsync(int id);
        Task<bool> DeleteDisponibilidadByDiaAsync(int elementoId, DiaSemana dia);
        Task<int> GetDisponibilidadesCountAsync(int elementoId);

    }
}
