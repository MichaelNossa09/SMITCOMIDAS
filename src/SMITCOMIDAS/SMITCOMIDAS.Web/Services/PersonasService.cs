using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Shared.Services;
using System.Net;

namespace SMITCOMIDAS.Web.Services
{
    public class PersonasService : IPersonasService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;

        public PersonasService(HttpClient http, IAuthService authService)
        {
            _http = http;
            _authService = authService;
        }

        private async Task SetAuthHeaderAsync()
        {
            var token = await _authService.GetTokenAsync();
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<Persona> GetMiPerfilAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<Persona>("api/personas/me");
        }

        public async Task<List<Persona>> GetPersonasAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<List<Persona>>("api/personas");
        }

        public async Task<Persona> GetPersonaAsync(int id)
        {
            try
            {
                await SetAuthHeaderAsync();
                return await _http.GetFromJsonAsync<Persona>($"api/personas/{id}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Si el perfil no existe (404), devolver null en lugar de lanzar excepción
                return null;
            }
        }

        public async Task<Persona> CreatePersonaAsync(Persona persona)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/personas", persona);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Persona>();
        }

        public async Task<bool> UpdatePersonaAsync(Persona persona)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PutAsJsonAsync($"api/personas/{persona.Id}", persona);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePersonaAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _http.DeleteAsync($"api/personas/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Persona> RegistroCompletoAsync(RegistroPersonaRequest request)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/personas/registro-completo", request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                if (errorContent.Contains("Ya existe"))
                {
                    throw new Exception("Ya existe un usuario o persona con estos datos");
                }
                else if (errorContent.Contains("contraseñas"))
                {
                    throw new Exception("Las contraseñas no coinciden");
                }
                else
                {
                    throw new Exception($"Error al registrar: {response.StatusCode}");
                }
            }

            return await response.Content.ReadFromJsonAsync<Persona>();
        }
        public async Task<bool> CambiarPasswordAsync(string userId, string nuevaPassword)
        {
            await SetAuthHeaderAsync();

            var request = new CambiarPasswordRequest
            {
                UserId = userId,
                NuevaPassword = nuevaPassword
            };

            var response = await _http.PostAsJsonAsync("api/personas/cambiar-password", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al cambiar contraseña: {error}");
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarCuotaMensualAsync(string userId)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsync($"api/personas/actualizar-cuota/{userId}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<int> ObtenerPedidosRestantesAsync(string userId)
        {
            await SetAuthHeaderAsync();
            var response = await _http.GetAsync($"api/personas/pedidos-restantes/{userId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }
    }
}
