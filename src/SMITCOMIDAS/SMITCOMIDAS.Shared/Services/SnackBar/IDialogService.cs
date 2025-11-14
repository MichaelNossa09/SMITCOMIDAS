using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services.SnackBar
{
    public interface IDialogService
    {
        Task<bool> ConfirmAsync(string message);
        Task<string> PromptAsync(string message, string defaultValue = "");
        Task AlertAsync(string message);
    }
}
