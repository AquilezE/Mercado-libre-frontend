using Mercado_libre_frontend.Models;
using System.Diagnostics.Contracts;

namespace Mercado_libre_frontend.Services
{
    public class CarritoClientService(HttpClient client)
    {
        public async Task<Carrito?> GetAsync()
        {
                var response = await client.GetAsync("api/carrito");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Carrito>();
            
        }

        public async Task PostAsync(int productoId, int cantidad)
        {
            var response = await client.PostAsJsonAsync($"api/carrito/items", new { productoId, cantidad });
            Console.WriteLine(response.Content);
            response.EnsureSuccessStatusCode();
        }

        public async Task PatchAsync(int itemId, int cantidad)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/carrito/items/{itemId}")
            {
                Content = JsonContent.Create(new { cantidad })
            };
            var response = await client.SendAsync(request);
            Console.WriteLine(response.Content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int itemId)
        {
            
                var response = await client.DeleteAsync($"api/carrito/items/{itemId}");

                var responseString = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
            
        }

        public async Task ClearAsync()
        {
            var response = await client.DeleteAsync("api/carrito");
            Console.WriteLine(response.Content);
            response.EnsureSuccessStatusCode();
        }

    }
}
