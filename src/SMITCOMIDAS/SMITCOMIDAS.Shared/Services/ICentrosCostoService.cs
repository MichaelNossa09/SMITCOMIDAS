using SMITCOMIDAS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services
{
    public interface ICentrosCostoService
    {
        Task<List<CentroCosto>> GetCentrosCostoAsync();
        Task<CentroCosto> GetCentroCostoAsync(int id);
        Task<CentroCosto> CreateCentroCostoAsync(CentroCosto centroCosto);
        Task<bool> UpdateCentroCostoAsync(CentroCosto centroCosto);
        Task<bool> DeleteCentroCostoAsync(int id);

        Task<List<CentroCosto>> GetCentrosCostoByProveedorIdAsync(int proveedorId);

    }
}
