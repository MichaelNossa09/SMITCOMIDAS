using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMITCOMIDAS.Shared.Models;

namespace SMITCOMIDAS.Web.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }
        public DbSet<Adicional> Adicionales { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<CentroCosto> CentrosCosto { get; set; }
        public DbSet<Compania> Companias { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<ElementoMenu> ElementosMenu { get; set; }
        public DbSet<DisponibilidadElemento> DisponibilidadesElemento { get; set; }
    }
}
