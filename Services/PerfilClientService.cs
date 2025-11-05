namespace Mercado_libre_frontend.Services
{
    public class PerfilClientService(HttpClient client)
    {
        public async Task<string> ObtenTiempoAsync()
        {
            return await client.GetFromJsonAsync<string>("api/auth/tiempo");
        }
    }
}
