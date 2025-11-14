using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Shared.Services;

namespace SMITCOMIDAS.Web.Services
{
    public class CompaniasService : ICompaniasService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;

        public CompaniasService(HttpClient http, IAuthService authService)
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

        public async Task<List<Compania>> GetCompaniasAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<List<Compania>>("api/companias") ?? new();
        }

        public async Task<Compania> GetCompaniaAsync(int id)
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<Compania>($"api/companias/{id}");
        }

        public async Task<Compania> CreateCompaniaAsync(Compania compania)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/companias", compania);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Compania>();
        }

        public async Task<bool> UpdateCompaniaAsync(Compania compania)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PutAsJsonAsync($"api/companias/{compania.Id}", compania);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCompaniaAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _http.DeleteAsync($"api/companias/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
