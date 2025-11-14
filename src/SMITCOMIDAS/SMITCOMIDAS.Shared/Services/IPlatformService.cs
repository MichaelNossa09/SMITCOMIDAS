using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Shared.Services
{
    public interface IPlatformService
    {
        bool IsMobile { get; }
        bool IsWeb { get; }
    }
}
