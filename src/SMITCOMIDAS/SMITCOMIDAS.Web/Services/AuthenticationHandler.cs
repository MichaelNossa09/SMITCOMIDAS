using SMITCOMIDAS.Shared.Services;

namespace SMITCOMIDAS.Web.Services
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthService _authService;

        public AuthenticationHandler(IAuthService authService)
        {
            _authService = authService;
            InnerHandler = new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
