using System.Security.Claims;
using Mercado_libre_frontend.Services;

namespace Mercado_libre_frontend.Middlewares;
    public class RefrescaTokenDelegatingHandler(AuthClientService auth, IHttpContextAccessor httpContextAccessor) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            if (response.Headers.Contains("Set-Authorization"))
            {
                string jwt = response.Headers.GetValues("Set-Authorization").FirstOrDefault()!;
                var claims = new List<Claim>
                {
                    new Claim("jwt", jwt),
                    new Claim(ClaimTypes.Name, httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name)!),
                    new Claim(ClaimTypes.GivenName, httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.GivenName)!),
                    new Claim(ClaimTypes.Role, httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role)!),
                };
                auth.IniciaSesionAsync(claims);
            }
            return response;
        }
    }

