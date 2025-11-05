using Mercado_libre_frontend.Models;

namespace Mercado_libre_frontend.Services
{
    public class BitacoraClientService(HttpClient client)
    {
        public async Task<List<Bitacora>> GetAsync()
        {
            return await client.GetFromJsonAsync<List<Bitacora>>("api/bitacora");
        }
    }
}
