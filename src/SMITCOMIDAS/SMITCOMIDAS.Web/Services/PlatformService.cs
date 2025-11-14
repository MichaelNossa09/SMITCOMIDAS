using SMITCOMIDAS.Shared.Services;

namespace SMITCOMIDAS.Web.Services
{
    public class PlatformService : IPlatformService
    {
        public bool IsMobile => false;
        public bool IsWeb => true;
    }
}
