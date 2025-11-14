using Microsoft.JSInterop;
using SMITCOMIDAS.Shared.Services.SnackBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Services.SnackBark
{
    public class ToastService : IToastService
    {
        private readonly IJSRuntime _jsRuntime;

        public ToastService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public void ShowInfo(string message)
        {
            _jsRuntime.InvokeVoidAsync("showToast", message, "info");
        }

        public void ShowSuccess(string message)
        {
            _jsRuntime.InvokeVoidAsync("showToast", message, "success");
        }

        public void ShowWarning(string message)
        {
            _jsRuntime.InvokeVoidAsync("showToast", message, "warning");
        }

        public void ShowError(string message)
        {
            _jsRuntime.InvokeVoidAsync("showToast", message, "error");
        }
    }
}
