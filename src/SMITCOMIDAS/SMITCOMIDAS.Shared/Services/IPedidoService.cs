using SMITCOMIDAS.Shared.Models;
using SMITCOMIDAS.Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services
{
    public interface IPedidoService
    {
        Task<List<Pedido>> GetPedidosAsync();
        Task<List<Pedido>> GetPedidosUsuarioAsync();
        Task<Pedido> GetPedidoByIdAsync(int id);
        Task<List<Menu>> GetMenusDisponiblesAsync(DateTime fecha, int proveedorId = 0);
        Task<List<ElementoMenu>> GetElementosDisponiblesAsync(int menuId, DateTime fecha);
        Task<PedidoDTO> CrearPedidoAsync(PedidoDTO pedidoDTO);
        Task<bool> CancelarPedidoAsync(int id);
        Task<List<Proveedor>> GetProveedoresActivosAsync();


        Task<List<Pedido>> GetAllPedidosAsync();
        Task<List<Pedido>> GetPedidosByProveedorIdAsync(int proveedorId);
        Task<Proveedor> GetProveedorByUsuarioIdAsync(string usuarioId);
        Task<bool> ActualizarEstadoPedidoAsync(int pedidoId, EstadoPedido nuevoEstado, string? motivoDevolucion = null);
    }
}
