using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Shared.Models.DTOs;
using SMITCOMIDAS.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Services
{
    public class DisponibilidadElementoService : IDisponibilidadElementoService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;

        public DisponibilidadElementoService(HttpClient http, IAuthService authService)
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

        public async Task<List<DisponibilidadElemento>> GetDisponibilidadesByElementoIdAsync(int elementoId)
        {
            try
            {
                await SetAuthHeaderAsync();

                var request = new HttpRequestMessage(HttpMethod.Get, $"api/disponibilidades/elemento/{elementoId}");
                request.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MustRevalidate = true
                };
                request.Headers.Add("Pragma", "no-cache");

                var response = await _http.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var disponibilidadesDTOs = await response.Content.ReadFromJsonAsync<List<DisponibilidadElementoDTO>>() ?? new List<DisponibilidadElementoDTO>();
                return disponibilidadesDTOs.Select(dto => dto.ToEntity()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener disponibilidades del elemento {elementoId}", ex);
            }
        }

        public async Task<DisponibilidadElemento> GetDisponibilidadByIdAsync(int id)
        {
            try
            {
                await SetAuthHeaderAsync();

                var request = new HttpRequestMessage(HttpMethod.Get, $"api/disponibilidades/{id}");
                request.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MustRevalidate = true
                };
                request.Headers.Add("Pragma", "no-cache");

                var response = await _http.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var disponibilidadDTO = await response.Content.ReadFromJsonAsync<DisponibilidadElementoDTO>();
                return disponibilidadDTO?.ToEntity() ?? new DisponibilidadElemento();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la disponibilidad {id}", ex);
            }
        }

        public async Task<DisponibilidadElemento> CreateDisponibilidadAsync(DisponibilidadElemento disponibilidad)
        {
            try
            {
                await SetAuthHeaderAsync();

                var disponibilidadDTO = disponibilidad.ToDTO();

                var response = await _http.PostAsJsonAsync("api/disponibilidades", disponibilidadDTO);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error HTTP {response.StatusCode}: {errorContent}");
                }

                var resultDTO = await response.Content.ReadFromJsonAsync<DisponibilidadElementoDTO>();
                return resultDTO?.ToEntity() ?? disponibilidad;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear disponibilidad", ex);
            }
        }

        public async Task<DisponibilidadElemento> UpdateDisponibilidadAsync(DisponibilidadElemento disponibilidad)
        {
            try
            {
                await SetAuthHeaderAsync();

                var disponibilidadDTO = disponibilidad.ToDTO();

                var response = await _http.PutAsJsonAsync($"api/disponibilidades/{disponibilidad.Id}", disponibilidadDTO);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error HTTP {response.StatusCode}: {errorContent}");
                }

                return disponibilidad; // La API no devuelve el objeto actualizado
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la disponibilidad {disponibilidad.Id}", ex);
            }
        }

        public async Task<bool> DeleteDisponibilidadAsync(int id)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _http.DeleteAsync($"api/disponibilidades/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la disponibilidad {id}", ex);
            }
        }

        public async Task<bool> DeleteDisponibilidadByDiaAsync(int elementoId, DiaSemana dia)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _http.DeleteAsync($"api/disponibilidades/elemento/{elementoId}/dia/{dia}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error del servidor: {error}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar disponibilidad del día {dia} para el elemento {elementoId}", ex);
            }
        }

        public async Task<int> GetDisponibilidadesCountAsync(int elementoId)
        {
            try
            {
                await SetAuthHeaderAsync();

                var request = new HttpRequestMessage(HttpMethod.Get, $"api/disponibilidades/elemento/{elementoId}/count");
                request.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MustRevalidate = true
                };
                request.Headers.Add("Pragma", "no-cache");

                var response = await _http.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<int>(jsonResponse);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el conteo de disponibilidades del elemento {elementoId}", ex);
            }
        }
    }
}
