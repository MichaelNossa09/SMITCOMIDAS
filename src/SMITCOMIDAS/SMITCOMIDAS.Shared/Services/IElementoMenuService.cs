using SMITCOMIDAS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services
{
    public interface IElementoMenuService
    {
        Task<List<ElementoMenu>> GetElementosMenuAsync();
        Task<List<ElementoMenu>> GetElementosMenuByMenuIdAsync(int menuId);
        Task<ElementoMenu> GetElementoMenuByIdAsync(int id);
        Task<ElementoMenu> CreateElementoMenuAsync(ElementoMenu elementoMenu);
        Task<ElementoMenu> UpdateElementoMenuAsync(ElementoMenu elementoMenu);
        Task<bool> DeleteElementoMenuAsync(int id);
        Task<bool> ToggleDisponibilidadAsync(int id, bool disponible);
        Task<(bool success, int disponibilidadesRestantes)> DeleteElementoMenuDisponibilidadAsync(int id, DiaSemana dia);


    }
}
