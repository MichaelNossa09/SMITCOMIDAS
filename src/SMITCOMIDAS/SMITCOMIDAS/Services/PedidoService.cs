using SMITCOMIDAS.Shared.Models.DTOs;
using SMITCOMIDAS.Shared.Models;
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
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _http;
        private readonly IAuthService _authService;

        public PedidoService(HttpClient http, IAuthService authService)
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

        public async Task<List<Pedido>> GetPedidosAsync()
        {
            try
            {
                await SetAuthHeaderAsync();
                var request = new HttpRequestMessage(HttpMethod.Get, "api/pedidos");
                request.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MustRevalidate = true
                };
                request.Headers.Add("Pragma", "no-cache");

                var response = await _http.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var pedidoDTOs = await response.Content.ReadFromJsonAsync<List<PedidoDTO>>() ?? new List<PedidoDTO>();
                return pedidoDTOs.Select(dto => dto.ToEntity()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los pedidos", ex);
            }
        }

        public async Task<List<Pedido>> GetPedidosUsuarioAsync()
        {
            try
            {
                await SetAuthHeaderAsync();
                var request = new HttpRequestMessage(HttpMethod.Get, "api/pedidos/usuario");
                request.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true,
                    NoStore = true,
                    MustRevalidate = true
                };
                request.Headers.Add("Pragma", "no-cache");

                var response = await _http.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var pedidoDTOs = await response.Content.ReadFromJsonAsync<List<PedidoDTO>>() ?? new List<PedidoDTO>();
                return pedidoDTOs.Select(dto => dto.ToEntity()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los pedidos del usuario", ex);
            }
        }

        public async Task<Pedido> GetPedidoByIdAsync(int id)
        {
            try
            {
                await SetAuthHeaderAsync();

                var request = new HttpRequestMessage(HttpMethod.Get, $"api/pedidos/{id}");
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

                var pedidoDTO = await response.Content.ReadFromJsonAsync<PedidoDTO>();

                if (pedidoDTO == null)
                    throw new Exception("No se pudo deserializar el pedido");

                return pedidoDTO.ToEntity();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el pedido {id}: {ex.Message}", ex);
            }
        }

        public async Task<List<Menu>> GetMenusDisponiblesAsync(DateTime fecha, int proveedorId = 0)
        {
            try
            {
                await SetAuthHeaderAsync();
                string url = $"api/pedidos/menus-disponibles?fecha={fecha:yyyy-MM-dd}";

                if (proveedorId > 0)
                {
                    url += $"&proveedorId={proveedorId}";
                }

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
                throw new Exception($"Error al obtener los menús disponibles para la fecha {fecha:yyyy-MM-dd}", ex);
            }
        }

        public async Task<List<ElementoMenu>> GetElementosDisponiblesAsync(int menuId, DateTime fecha)
        {
            try
            {
                await SetAuthHeaderAsync();
                var url = $"api/pedidos/elementos-disponibles/{menuId}?fecha={fecha:yyyy-MM-dd}";

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

                var elementoDTOs = await response.Content.ReadFromJsonAsync<List<ElementoMenuDTO>>() ?? new List<ElementoMenuDTO>();
                return elementoDTOs.Select(dto => dto.ToEntity()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los elementos disponibles para el menú {menuId}", ex);
            }
        }

        public async Task<PedidoDTO> CrearPedidoAsync(PedidoDTO pedidoDTO)
        {
            try
            {
                await SetAuthHeaderAsync();

                var response = await _http.PostAsJsonAsync("api/pedidos", pedidoDTO);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error HTTP {response.StatusCode}: {errorContent}");
                }

                var pedidoCreadoDTO = await response.Content.ReadFromJsonAsync<PedidoDTO>();
                return pedidoCreadoDTO ?? pedidoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el pedido", ex);
            }
        }

        public async Task<bool> CancelarPedidoAsync(int id)
        {
            try
            {
                await SetAuthHeaderAsync();
                var response = await _http.PutAsync($"api/pedidos/{id}/cancelar", null);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error HTTP {response.StatusCode}: {errorContent}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cancelar el pedido {id}", ex);
            }
        }

        public async Task<List<Proveedor>> GetProveedoresActivosAsync()
        {
            try
            {
                await SetAuthHeaderAsync();
                var url = "api/proveedores/activos";

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

                var proveedorDTOs = await response.Content.ReadFromJsonAsync<List<ProveedorDTO>>() ?? new List<ProveedorDTO>();
                return proveedorDTOs.Select(dto => dto.ToEntity()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los proveedores activos", ex);
            }
        }





        public async Task<List<Pedido>> GetAllPedidosAsync()
        {
            try
            {
                await SetAuthHeaderAsync();
                var request = new HttpRequestMessage(HttpMethod.Get, "api/pedidos/admin/all");
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
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException("No tiene permisos de administrador");
                    }

                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error HTTP {response.StatusCode}: {errorContent}");
                }

                var pedidoDTOs = await response.Content.ReadFromJsonAsync<List<PedidoDTO>>() ?? new List<PedidoDTO>();
                return pedidoDTOs.Select(dto => dto.ToEntity()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener todos los pedidos: {ex.Message}", ex);
            }
        }

        public async Task<List<Pedido>> GetPedidosByProveedorIdAsync(int proveedorId)
        {
            try
            {
                await SetAuthHeaderAsync();
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/pedidos/proveedor/{proveedorId}");
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
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException("No tiene permisos para ver pedidos de este proveedor");
                    }

                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error HTTP {response.StatusCode}: {errorContent}");
                }

                var pedidoDTOs = await response.Content.ReadFromJsonAsync<List<PedidoDTO>>() ?? new List<PedidoDTO>();
                return pedidoDTOs.Select(dto => dto.ToEntity()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener pedidos del proveedor {proveedorId}: {ex.Message}", ex);
            }
        }

        public async Task<Proveedor> GetProveedorByUsuarioIdAsync(string usuarioId)
        {
            try
            {
                await SetAuthHeaderAsync();
                var url = $"api/proveedores/usuario/{usuarioId}";

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
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new KeyNotFoundException($"No se encontró un proveedor para el usuario {usuarioId}");
                    }

                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error HTTP {response.StatusCode}: {errorContent}");
                }

                var proveedorDTO = await response.Content.ReadFromJsonAsync<ProveedorDTO>();

                if (proveedorDTO == null)
                    throw new Exception("No se pudo deserializar el proveedor");

                return proveedorDTO.ToEntity();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el proveedor por usuario {usuarioId}: {ex.Message}", ex);
            }
        }

        public async Task<bool> ActualizarEstadoPedidoAsync(int pedidoId, EstadoPedido nuevoEstado, string? motivoDevolucion = null)
        {
            try
            {
                await SetAuthHeaderAsync();

                var request = new
                {
                    Estado = nuevoEstado
                };

                var response = await _http.PutAsJsonAsync($"api/pedidos/{pedidoId}/estado", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error HTTP {response.StatusCode}: {errorContent}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el estado del pedido {pedidoId}: {ex.Message}", ex);
            }
        }
    }
}
