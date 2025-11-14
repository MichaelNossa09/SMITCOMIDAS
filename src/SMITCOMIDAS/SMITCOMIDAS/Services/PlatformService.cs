using SMITCOMIDAS.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITCOMIDAS.Services
{
    public class PlatformService : IPlatformService
    {
        public bool IsMobile => true;
        public bool IsWeb => false;
    }
}
