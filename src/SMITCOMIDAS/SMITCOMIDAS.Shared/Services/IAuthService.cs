using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string email, string password);
        Task LogoutAsync();
        Task<string> GetTokenAsync();
        Task<string> GetUserRoleAsync();
        Task<string> GetUserNameAsync();
        Task<string> GetUserIdAsync();
        Task<string> GetUserRoleByIdAsync(string userId);
        Task UpdateUserRoleAsync(string userId, string newRole);
    }
}
