using Microsoft.JSInterop;
using SMITCOMIDAS.Shared.Services.SnackBar;

namespace SMITCOMIDAS.Web.Services.SnackBar
{
    public class DialogService : IDialogService
    {
        private readonly IJSRuntime _jsRuntime;

        public DialogService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task AlertAsync(string message)
        {
            await _jsRuntime.InvokeVoidAsync("alert", message);
        }

        public async Task<bool> ConfirmAsync(string message)
        {
            return await _jsRuntime.InvokeAsync<bool>("confirm", message);
        }

        public async Task<string> PromptAsync(string message, string defaultValue = "")
        {
            return await _jsRuntime.InvokeAsync<string>("prompt", message, defaultValue);
        }
    }
}
