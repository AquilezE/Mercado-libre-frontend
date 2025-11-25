using Mercado_libre_frontend.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;
using System.Security.Claims;

namespace Mercado_libre_frontend.Services
{
    public class AuthClientService(HttpClient client, IHttpContextAccessor httpContextAccessor)
    {
        public async Task<AuthUser> ObtenTokenAsync(string email, string password)
        {
            Login usuario = new Login
            {
                Email = email,
                Password = password
            };

            var response = await client.PostAsJsonAsync("api/auth", usuario);
            var token = await response.Content.ReadFromJsonAsync<AuthUser>();


            return token!;
        }

        public async Task IniciaSesionAsync(List<Claim> claims)
        {
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();
            await httpContextAccessor.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public async Task RegistrarUsuarioAsync(UsuarioPwd usuario)
        {
            var response = await client.PostAsJsonAsync("api/auth/registro", usuario);
            response.EnsureSuccessStatusCode();
        }

    }
}

