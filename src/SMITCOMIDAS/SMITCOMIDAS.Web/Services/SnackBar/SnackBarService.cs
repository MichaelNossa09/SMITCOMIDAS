using SMITCOMIDAS.Shared.Services.SnackBar;

namespace SMITCOMIDAS.Web.Services.SnackBar
{
    public class SnackBarService : ISnackbar
    {
        public event Action<SnackbarNotification>? OnNotification;

        private readonly List<TaskCompletionSource<bool>> _confirmationTaskSources = new();

        public void Add(string message, Severity severity = Severity.Info)
        {
            var notification = new SnackbarNotification
            {
                Message = message,
                Severity = severity,
                IsConfirm = false
            };

            OnNotification?.Invoke(notification);
            SnackbarEvents.RaiseMessage(message, severity);

        }

        public async Task<bool> Confirm(string message)
        {
            var tcs = new TaskCompletionSource<bool>();
            _confirmationTaskSources.Add(tcs);

            var notification = new SnackbarNotification
            {
                Message = message,
                Severity = Severity.Warning,
                IsConfirm = true,
                ConfirmCallback = async (confirmed) =>
                {
                    tcs.SetResult(confirmed);
                }
            };

            OnNotification?.Invoke(notification);
            SnackbarEvents.RaiseConfirmation(message, tcs);

            return await tcs.Task;
        }

        public class SnackbarNotification
        {
            public string Message { get; set; } = string.Empty;
            public Severity Severity { get; set; }
            public bool IsConfirm { get; set; }
            public Func<bool, Task>? ConfirmCallback { get; set; }
        }
    }
   
}
