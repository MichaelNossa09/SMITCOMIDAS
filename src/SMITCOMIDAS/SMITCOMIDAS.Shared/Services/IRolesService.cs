using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services
{
    public interface IRolesService
    {
        /// <summary>
        /// Obtiene todos los roles disponibles en el sistema
        /// </summary>
        /// <returns>Lista de nombres de roles</returns>
        Task<List<string>> GetRolesAsync();

        /// <summary>
        /// Verifica si un rol específico existe en el sistema
        /// </summary>
        /// <param name="roleName">Nombre del rol a verificar</param>
        /// <returns>Verdadero si el rol existe, falso en caso contrario</returns>
        Task<bool> RoleExistsAsync(string roleName);

        /// <summary>
        /// Obtiene los usuarios asignados a un rol específico
        /// </summary>
        /// <param name="roleName">Nombre del rol</param>
        /// <returns>Lista de IDs de usuario con ese rol</returns>
        Task<List<string>> GetUsersInRoleAsync(string roleName);
    }
}
