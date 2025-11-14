using SMITCOMIDAS.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Services
{
    public class RolesService : IRolesService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;
        private List<string> _cachedRoles;

        public RolesService(HttpClient http, IAuthService authService)
        {
            _http = http;
            _authService = authService;
        }
        /// <summary>
        /// Configura el header de autorización con el token JWT
        /// </summary>
        private async Task SetAuthHeaderAsync()
        {
            var token = await _authService.GetTokenAsync();
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        /// Obtiene todos los roles disponibles en el sistema
        /// </summary>
        public async Task<List<string>> GetRolesAsync()
        {
            // Si ya tenemos los roles en caché, los devolvemos
            if (_cachedRoles != null && _cachedRoles.Count > 0)
                return _cachedRoles;

            try
            {
                await SetAuthHeaderAsync();
                _cachedRoles = await _http.GetFromJsonAsync<List<string>>("api/roles");
                return _cachedRoles;
            }
            catch (Exception)
            {
                // Si hay un error, devolvemos los roles predeterminados
                return new List<string> { "Admin", "Administrativo", "Operativo", "Proveedor" };
            }
        }

        /// <summary>
        /// Verifica si un rol específico existe en el sistema
        /// </summary>
        public async Task<bool> RoleExistsAsync(string roleName)
        {
            try
            {
                await SetAuthHeaderAsync();
                return await _http.GetFromJsonAsync<bool>($"api/roles/exists/{roleName}");
            }
            catch
            {
                // Si hay un error, verificamos contra los roles predeterminados
                var defaultRoles = new List<string> { "Admin", "Administrativo", "Operativo", "Proveedor" };
                return defaultRoles.Contains(roleName);
            }
        }

        /// <summary>
        /// Obtiene los usuarios asignados a un rol específico
        /// </summary>
        public async Task<List<string>> GetUsersInRoleAsync(string roleName)
        {
            try
            {
                await SetAuthHeaderAsync();
                return await _http.GetFromJsonAsync<List<string>>($"api/roles/users/{roleName}");
            }
            catch
            {
                // Si hay un error, devolvemos una lista vacía
                return new List<string>();
            }
        }
    }
}
