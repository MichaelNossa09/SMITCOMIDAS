using SMITCOMIDAS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services
{
    public interface IMenuService
    {
        Task<List<Menu>> GetMenusAsync(bool includeElementos = false);
        Task<List<Menu>> GetMenusByProveedorAsync(int proveedorId, bool includeElementos = false);
        Task<Menu> GetMenuByIdAsync(int id, bool includeElementos = false);
        Task<Menu> CreateMenuAsync(Menu menu);
        Task<Menu> UpdateMenuAsync(Menu menu);
        Task<bool> DeleteMenuAsync(int id);
        Task<bool> PublicarMenuAsync(int id);
        Task<bool> DesactivarMenuAsync(int id);
        Task<bool> ValidarProveedorTieneElementoAsync(int proveedorId, int elementoMenuId);
    }
}
