using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Shared.Models.RegistroRequest;
using SMITCOMIDAS.Shared.Services;

namespace SMITCOMIDAS.Web.Services
{
    public class ProveedoresService : IProveedoresService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;

        public ProveedoresService(HttpClient http, IAuthService authService)
        {
            _http = http;
            _authService = authService;
        }

        private async Task SetAuthHeaderAsync()
        {
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<Proveedor>> GetProveedoresAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<List<Proveedor>>("api/proveedores") ?? new();
        }

        public async Task<List<Proveedor>> GetProveedoresActivosAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<List<Proveedor>>("api/proveedores/activos") ?? new();
        }

        public async Task<Proveedor> GetProveedorAsync(int id)
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<Proveedor>($"api/proveedores/{id}");
        }
        public async Task<Proveedor> GetProveedorByUsuarioIdAsync(string userId)
        {
            try
            {
                await SetAuthHeaderAsync();

                var response = await _http.GetAsync($"api/proveedores/usuario/{userId}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }

                response.EnsureSuccessStatusCode(); // Lanza excepción para otros errores HTTP

                return await response.Content.ReadFromJsonAsync<Proveedor>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener proveedor para el usuario {userId}: {ex.Message}");
                throw new Exception($"Error al obtener proveedor para el usuario {userId}", ex);
            }
        }
        public async Task<Proveedor> CreateProveedorAsync(Proveedor proveedor)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/proveedores", proveedor);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Proveedor>();
        }

        public async Task UpdateProveedorAsync(Proveedor proveedor)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PutAsJsonAsync($"api/proveedores/{proveedor.Id}", proveedor);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProveedorAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _http.DeleteAsync($"api/proveedores/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<Proveedor> RegistroCompletoAsync(RegistroProveedorRequest request)
        {
            await SetAuthHeaderAsync();
            request.Rol = "Proveedor";
            var response = await _http.PostAsJsonAsync("api/proveedores/registro-completo", request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al registrar: {errorContent}");
            }

            return await response.Content.ReadFromJsonAsync<Proveedor>();
        }

        public async Task<bool> CambiarPasswordAsync(string userId, string nuevaPassword)
        {
            await SetAuthHeaderAsync();

            var request = new CambiarPasswordRequest
            {
                UserId = userId,
                NuevaPassword = nuevaPassword
            };

            var response = await _http.PostAsJsonAsync("api/proveedores/cambiar-password", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al cambiar contraseña: {error}");
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<Proveedor> GetMiProveedorAsync()
        {
            await SetAuthHeaderAsync();
            var response = await _http.GetAsync("api/proveedores/mi-perfil");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Proveedor>();
        }
    }
}
