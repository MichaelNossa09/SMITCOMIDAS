using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Shared.Models.DTOs;
using SMITCOMIDAS.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Services
{
    public class MenuService : IMenuService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;

        public MenuService(HttpClient http, IAuthService authService)
        {
            _http = http;
            _authService = authService;
        }
        private async Task SetAuthHeaderAsync()
        {
            var token = await _authService.GetTokenAsync();
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<Menu>> GetMenusAsync(bool includeElementos = false)
        {
            try
            {
                await SetAuthHeaderAsync();
                var url = includeElementos ? "api/menus?includeElementos=true" : "api/menus";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MustRevalidate = true
                };
                request.Headers.Add("Pragma", "no-cache");

                var response = await _http.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var menuDTOs = await response.Content.ReadFromJsonAsync<List<MenuDTO>>() ?? new List<MenuDTO>();
                return menuDTOs.Select(dto => dto.ToEntity()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los menús", ex);
            }
        }

        public async Task<List<Menu>> GetMenusByProveedorAsync(int proveedorId, bool includeElementos = false)
        {
            try
            {
                await SetAuthHeaderAsync();
                var url = includeElementos
                    ? $"api/menus/proveedor/{proveedorId}?includeElementos=true"
                    : $"api/menus/proveedor/{proveedorId}";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MustRevalidate = true
                };
                request.Headers.Add("Pragma", "no-cache");

                var response = await _http.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var menuDTOs = await response.Content.ReadFromJsonAsync<List<MenuDTO>>() ?? new List<MenuDTO>();
                return menuDTOs.Select(dto => dto.ToEntity()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los menús del proveedor {proveedorId}", ex);
            }
        }

        public async Task<Menu> GetMenuByIdAsync(int id, bool includeElementos = false)
        {
            try
            {
                await SetAuthHeaderAsync();

                var url = includeElementos
                    ? $"api/menus/{id}?includeElementos=true"
                    : $"api/menus/{id}";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MustRevalidate = true
                };
                request.Headers.Add("Pragma", "no-cache");

                var response = await _http.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error HTTP {response.StatusCode}: {errorContent}");
                }

                var menuDTO = await response.Content.ReadFromJsonAsync<MenuDTO>();

                if (menuDTO == null)
                    throw new Exception("No se pudo deserializar el menú");

                return menuDTO.ToEntity();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el menú {id}: {ex.Message}", ex);
            }
        }

        public async Task<Menu> CreateMenuAsync(Menu menu)
        {
            try
            {
                await SetAuthHeaderAsync();

                // Convertir a DTO
                var menuDTO = menu.ToDTO();

                var response = await _http.PostAsJsonAsync("api/menus", menuDTO);
                response.EnsureSuccessStatusCode();

                var menuCreadoDTO = await response.Content.ReadFromJsonAsync<MenuDTO>();
                return menuCreadoDTO?.ToEntity() ?? menu;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el menú", ex);
            }
        }

        public async Task<Menu> UpdateMenuAsync(Menu menu)
        {
            try
            {
                await SetAuthHeaderAsync();

                // Convertir a DTO
                var menuDTO = menu.ToDTO();

                var response = await _http.PutAsJsonAsync($"api/menus/{menu.Id}", menuDTO);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error HTTP {response.StatusCode}: {errorContent}");
                }

                // Devolvemos el menú original ya que la operación de actualización no retorna el objeto
                // Si necesitas el menú actualizado, podrías hacer una llamada adicional a GetMenuByIdAsync
                return menu;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el menú {menu.Id}: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteMenuAsync(int id)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _http.DeleteAsync($"api/menus/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el menú {id}", ex);
            }
        }

        public async Task<bool> PublicarMenuAsync(int id)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _http.PutAsync($"api/menus/{id}/publicar", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al publicar el menú {id}", ex);
            }
        }

        public async Task<bool> DesactivarMenuAsync(int id)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _http.PutAsync($"api/menus/{id}/desactivar", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al desactivar el menú {id}", ex);
            }
        }

        public async Task<bool> ValidarProveedorTieneElementoAsync(int proveedorId, int elementoMenuId)
        {
            await SetAuthHeaderAsync();
            var response = await _http.GetAsync($"api/menus/validar-proveedor/{proveedorId}/elemento/{elementoMenuId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>();
            }

            return false;
        }
    }
}
