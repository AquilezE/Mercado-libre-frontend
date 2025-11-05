using Mercado_libre_frontend.Models;

namespace Mercado_libre_frontend.Services
{
    public class RolesClientService(HttpClient client)
    {
        public async Task<List<Rol>> GetAsync()
        {
            return await client.GetFromJsonAsync<List<Rol>>("api/roles");
        }
    }
}
