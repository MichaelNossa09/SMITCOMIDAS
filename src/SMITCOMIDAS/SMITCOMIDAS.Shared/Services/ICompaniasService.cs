using SMITCOMIDAS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services
{
    public interface ICompaniasService
    {
        Task<List<Compania>> GetCompaniasAsync();
        Task<Compania> GetCompaniaAsync(int id);
        Task<Compania> CreateCompaniaAsync(Compania compania);
        Task<bool> UpdateCompaniaAsync(Compania compania);
        Task<bool> DeleteCompaniaAsync(int id);
    }
}
