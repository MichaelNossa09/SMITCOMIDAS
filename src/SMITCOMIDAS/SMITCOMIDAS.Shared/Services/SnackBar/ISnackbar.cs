using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services.SnackBar
{
    public interface ISnackbar
    {
        void Add(string message, Severity severity = Severity.Info);
        Task<bool> Confirm(string message);
    }

    public enum Severity
    {
        Info,
        Success,
        Warning,
        Error
    }
}
