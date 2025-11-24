using Mercado_libre_frontend.Models;
namespace Mercado_libre_frontend.Services
{
    public class PedidoClientService(HttpClient client)
    {

        public async Task<List<Pedido>?> GetPedidosAsync()
        {

                var response = await client.GetAsync("api/pedido");
                response.EnsureSuccessStatusCode();
                var jsonContent = await response.Content.ReadAsStringAsync();
                var pedidos = await response.Content.ReadFromJsonAsync<List<Pedido>>();
                return pedidos;
            
        }

        public async Task<Pedido?> GetPedidoAsync(int id)
        {
           
                var response = await client.GetAsync($"api/pedido/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Pedido>();
            
        }

        public async Task<List<Direccion>?> GetDireccionesAsync()
        {

                var response = await client.GetAsync("api/direccion");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<List<Direccion>>();
           

        }

        public async Task<Direccion?> GetDireccionAsync(int id)
        {
                var response = await client.GetAsync($"api/direccion/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Direccion>();
        }

        public async Task CrearPedidoAsync(int direccionId)
        {
            var response = await client.PostAsJsonAsync("api/pedido", new { direccionId });
            response.EnsureSuccessStatusCode();
        }

        public async Task<Direccion?> CrearDireccionAsync(Direccion direccion)
        {
            var requestData = new
            {
                calle = direccion.Calle,
                ciudad = direccion.Ciudad,
                codigoPostal = direccion.CodigoPostal
            };

            var response = await client.PostAsJsonAsync("api/direccion", requestData);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Direccion>();
        }


    }
}
