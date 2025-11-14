using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Shared.Services;

namespace SMITCOMIDAS.Web.Services
{
    public class CentrosCostoService : ICentrosCostoService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;

        public CentrosCostoService(HttpClient http, IAuthService authService)
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

        public async Task<List<CentroCosto>> GetCentrosCostoAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<List<CentroCosto>>("api/centroscosto") ?? new();
        }

        public async Task<CentroCosto> GetCentroCostoAsync(int id)
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<CentroCosto>($"api/centroscosto/{id}");
        }

        public async Task<CentroCosto> CreateCentroCostoAsync(CentroCosto centroCosto)
        {
            try
            {
                await SetAuthHeaderAsync();

                Console.WriteLine($"[CentroCosto] URL: {_http.BaseAddress}api/centroscosto");
                Console.WriteLine($"[CentroCosto] Token: {_http.DefaultRequestHeaders.Authorization?.Parameter?.Substring(0, 20)}...");
                Console.WriteLine($"[CentroCosto] CompaniaId: {centroCosto.CompaniaId}");

                var response = await _http.PostAsJsonAsync("api/centroscosto", centroCosto);

                Console.WriteLine($"[CentroCosto] Status: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[CentroCosto] Error: {error}");
                    throw new Exception($"Error: {error}");
                }

                return await response.Content.ReadFromJsonAsync<CentroCosto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CentroCosto] Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateCentroCostoAsync(CentroCosto centroCosto)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PutAsJsonAsync($"api/centroscosto/{centroCosto.Id}", centroCosto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCentroCostoAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _http.DeleteAsync($"api/centroscosto/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CentroCosto>> GetCentrosCostoByProveedorIdAsync(int proveedorId)
        {
            await SetAuthHeaderAsync();

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/centroscosto/proveedor/{proveedorId}");
            request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true,
                MustRevalidate = true
            };
            request.Headers.Add("Pragma", "no-cache");

            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<CentroCosto>>() ?? new List<CentroCosto>();
        }
    }
}
