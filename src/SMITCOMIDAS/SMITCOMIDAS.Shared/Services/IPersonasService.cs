using SMITCOMIDAS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services
{
    public interface IPersonasService
    {
        Task<Persona> GetMiPerfilAsync();
        Task<List<Persona>> GetPersonasAsync();
        Task<Persona> GetPersonaAsync(int id);
        Task<Persona> CreatePersonaAsync(Persona persona);
        Task<bool> UpdatePersonaAsync(Persona persona);
        Task<bool> DeletePersonaAsync(int id);
        Task<Persona> RegistroCompletoAsync(RegistroPersonaRequest request);
        Task<bool> CambiarPasswordAsync(string userId, string nuevaPassword);
        Task<bool> ActualizarCuotaMensualAsync(string userId); 
        Task<int> ObtenerPedidosRestantesAsync(string userId);
    }
}
