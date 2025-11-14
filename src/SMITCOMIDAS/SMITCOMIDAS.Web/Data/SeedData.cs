using Microsoft.AspNetCore.Identity;
using SMITCOMIDAS.Shared.Models;

namespace SMITCOMIDAS.Web.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Crear roles
            string[] roles = { "Admin", "Administrativo", "Operativo", "Proveedor" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Usuario Admin
            if (await userManager.FindByEmailAsync("admin@smitco.com.co") == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@smitco.com.co",
                    Email = "admin@smitco.com.co",
                    FullName = "Administrador",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(admin, "Admin123!");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
            else
            {
                // Si ya existe, asegurar que tenga el rol
                var admin = await userManager.FindByEmailAsync("admin@smitco.com.co");
                if (!await userManager.IsInRoleAsync(admin, "Admin"))
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
