using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Shared.Models.RegistroRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services
{
    public interface IProveedoresService
    {
        Task<List<Proveedor>> GetProveedoresAsync();
        Task<List<Proveedor>> GetProveedoresActivosAsync();
        Task<Proveedor> GetProveedorByUsuarioIdAsync(string userId);
        Task<Proveedor> GetProveedorAsync(int id);
        Task<Proveedor> RegistroCompletoAsync(RegistroProveedorRequest request);
        Task UpdateProveedorAsync(Proveedor proveedor);
        Task<bool> CambiarPasswordAsync(string userId, string nuevaPassword);
        Task DeleteProveedorAsync(int id);
        Task<Proveedor> GetMiProveedorAsync();


    }
}
