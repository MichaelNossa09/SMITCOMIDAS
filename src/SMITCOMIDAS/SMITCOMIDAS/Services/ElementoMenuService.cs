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
    public class ElementoMenuService : IElementoMenuService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;

        public ElementoMenuService(HttpClient http, IAuthService authService)
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

        public async Task<List<ElementoMenu>> GetElementosMenuAsync()
        {
            try
            {
                await SetAuthHeaderAsync();

                var request = new HttpRequestMessage(HttpMethod.Get, "api/elementosmenu");
                request.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MustRevalidate = true
                };
                request.Headers.Add("Pragma", "no-cache");

                var response = await _http.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var elementosDTOs = await response.Content.ReadFromJsonAsync<List<ElementoMenuDTO>>() ?? new();
                return elementosDTOs.Select(dto => dto.ToEntity()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener elementos del menú", ex);
            }
        }

        public async Task<List<ElementoMenu>> GetElementosMenuByMenuIdAsync(int menuId)
        {
            try
            {
                await SetAuthHeaderAsync();

                var request = new HttpRequestMessage(HttpMethod.Get, $"api/elementosmenu/menu/{menuId}");
                request.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MustRevalidate = true
                };
                request.Headers.Add("Pragma", "no-cache");

                var response = await _http.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var elementosDTOs = await response.Content.ReadFromJsonAsync<List<ElementoMenuDTO>>() ?? new();
                return elementosDTOs.Select(dto => dto.ToEntity()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener elementos del menú {menuId}", ex);
            }
        }

        public async Task<ElementoMenu> GetElementoMenuByIdAsync(int id)
        {
            try
            {
                await SetAuthHeaderAsync();

                var request = new HttpRequestMessage(HttpMethod.Get, $"api/elementosmenu/{id}");
                request.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MustRevalidate = true
                };
                request.Headers.Add("Pragma", "no-cache");

                var response = await _http.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var elementoDTO = await response.Content.ReadFromJsonAsync<ElementoMenuDTO>() ?? new();
                return elementoDTO.ToEntity();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el elemento de menú {id}", ex);
            }
        }

        public async Task<ElementoMenu> CreateElementoMenuAsync(ElementoMenu elementoMenu)
        {
            try
            {
                await SetAuthHeaderAsync();

                // Convertir a DTO
                var elementoDTO = elementoMenu.ToDTO(true);

                var response = await _http.PostAsJsonAsync("api/elementosmenu", elementoDTO);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error HTTP {response.StatusCode}: {errorContent}");
                }

                // Deserializar respuesta
                var resultDTO = await response.Content.ReadFromJsonAsync<ElementoMenuDTO>() ?? new ElementoMenuDTO();
                return resultDTO.ToEntity();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear elemento de menú", ex);
            }
        }

        public async Task<ElementoMenu> UpdateElementoMenuAsync(ElementoMenu elementoMenu)
        {
            try
            {
                await SetAuthHeaderAsync();

                // Convertir a DTO
                var elementoDTO = elementoMenu.ToDTO(true);

                var response = await _http.PutAsJsonAsync($"api/elementosmenu/{elementoMenu.Id}", elementoDTO);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error HTTP {response.StatusCode}: {errorContent}");
                }

                return elementoMenu; // La API no devuelve el objeto actualizado
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el elemento de menú {elementoMenu.Id}", ex);
            }
        }

        public async Task<bool> DeleteElementoMenuAsync(int id)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _http.DeleteAsync($"api/elementosmenu/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el elemento de menú {id}", ex);
            }
        }

        public async Task<bool> ToggleDisponibilidadAsync(int id, bool disponible)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _http.PutAsJsonAsync($"api/elementosmenu/{id}/disponibilidad", disponible);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cambiar disponibilidad del elemento de menú {id}", ex);
            }
        }

        public async Task<(bool success, int disponibilidadesRestantes)> DeleteElementoMenuDisponibilidadAsync(int id, DiaSemana dia)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _http.DeleteAsync($"api/elementosmenu/{id}/disponibilidad/{dia}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<dynamic>();
                return (result.GetProperty("success").GetBoolean(), result.GetProperty("disponibilidadesRestantes").GetInt32());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar disponibilidad del día {dia} para el elemento {id}", ex);
            }
        }
    }
}
