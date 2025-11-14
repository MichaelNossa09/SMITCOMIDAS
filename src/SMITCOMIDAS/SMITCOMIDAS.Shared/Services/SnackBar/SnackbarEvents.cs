using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services.SnackBar
{
    public static class SnackbarEvents
    {
        public static event Action<string, Severity>? OnMessage;
        public static event Action<string, TaskCompletionSource<bool>>? OnConfirmation;
        public static event Action<string>? OnRemove; 
        public static event Action? OnClear;
        public static void RaiseMessage(string message, Severity severity)
        {
            OnMessage?.Invoke(message, severity);
        }

        public static void RaiseConfirmation(string message, TaskCompletionSource<bool> tcs)
        {
            OnConfirmation?.Invoke(message, tcs);
        }
        public static void RaiseRemove(string messageId)
        {
            OnRemove?.Invoke(messageId);
        }

        public static void RaiseClear()
        {
            OnClear?.Invoke();
        }
    }
}
