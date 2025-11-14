using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services.SnackBar
{
    public interface IToastService
    {
        void ShowInfo(string message);
        void ShowSuccess(string message);
        void ShowWarning(string message);
        void ShowError(string message);
    }
}
