using System.Net.Http.Headers;
using Mercado_libre_frontend.Models;
namespace Mercado_libre_frontend.Services
{
    public class ArchivosClientService(HttpClient client)
    {

        public async Task<List<Archivo>?> GetAsync()
        {
            return await client.GetFromJsonAsync<List<Archivo>>("api/archivos");
        }

        public async Task<Archivo?> GetAsync(int id)
        {
            return await client.GetFromJsonAsync<Archivo>($"api/archivos/{id}/detalle");
        }

        public async Task PostAsync(Upload archivo)
        {
            var memoryStream = new MemoryStream();
            await archivo.Portada.CopyToAsync(memoryStream);
            var contenido = memoryStream.ToArray();
            memoryStream.Close();
            var filecontent = new ByteArrayContent(contenido);
            filecontent.Headers.ContentType = new MediaTypeHeaderValue(archivo.Portada.ContentType);
            using var form = new MultipartFormDataContent
            {
                {filecontent, "file", archivo.Portada.FileName! }
            };

            var response = await client.PostAsync("api/archivos", form);
            response.EnsureSuccessStatusCode();
        }

        public async Task PutAsync(Upload archivo)
        {
            var memoryStream = new MemoryStream();
            await archivo.Portada.CopyToAsync(memoryStream);
            var contenido = memoryStream.ToArray();
            memoryStream.Close();
            var filecontent = new ByteArrayContent(contenido);
            filecontent.Headers.ContentType = new MediaTypeHeaderValue(archivo.Portada.ContentType);
            using var form = new MultipartFormDataContent
            {
                {filecontent, "file", archivo.Portada.FileName! }
            };

            var response = await client.PutAsync($"api/archivos/{archivo.ArchivoId}", form);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await client.DeleteAsync($"api/archivos/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
