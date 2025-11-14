using Microsoft.JSInterop;
using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Shared.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SMITCOMIDAS.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        private string _cachedToken;

        public AuthService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/auth/login",
                    new LoginRequest { Email = email, Password = password });
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    // Solo guardar el token
                    _cachedToken = result.Token;
                    await _js.InvokeVoidAsync("localStorage.setItem", "auth_token", result.Token);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            _cachedToken = null;
            await _js.InvokeVoidAsync("localStorage.removeItem", "auth_token");
        }

        public async Task<string> GetTokenAsync()
        {
            if (!string.IsNullOrEmpty(_cachedToken))
                return _cachedToken;
            _cachedToken = await _js.InvokeAsync<string>("localStorage.getItem", "auth_token");
            return _cachedToken;
        }

        public async Task<string> GetUserNameAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                return "Usuario";
            var claims = ParseToken(token);
            return claims.FirstOrDefault(c => c.Type == "sub")?.Value ?? "Usuario";
        }

        public async Task<string> GetUserIdAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                return null;
            var claims = ParseToken(token);
            return claims.FirstOrDefault(c => c.Type == "nameid" ||
                                               c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        }

        public async Task<string> GetUserRoleAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                return null;
            var claims = ParseToken(token);
            return claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        }

        public async Task<string> GetUserRoleByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return null;

            try
            {
                // Configura el header de autorización
                await SetAuthHeaderAsync();

                // Obtener el rol del usuario por su ID
                var response = await _http.GetAsync($"api/auth/userrole/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener rol de usuario: {ex.Message}");
                return null;
            }
        }

        public async Task UpdateUserRoleAsync(string userId, string newRole)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(newRole))
                return;

            try
            {
                // Configura el header de autorización
                await SetAuthHeaderAsync();

                // Crear el modelo de solicitud
                var request = new UpdateRoleRequest
                {
                    UserId = userId,
                    Role = newRole
                };

                // Actualizar el rol del usuario
                var response = await _http.PostAsJsonAsync("api/auth/updaterole", request);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar rol: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar rol: {ex.Message}");
                throw;
            }
        }

        private List<Claim> ParseToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.Claims.ToList();
        }

        private async Task SetAuthHeaderAsync()
        {
            var token = await GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public class UpdateRoleRequest
        {
            public string UserId { get; set; }
            public string Role { get; set; }
        }
    }
}
