using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeStore.Services
{
    public class DeleteProductService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task DeleteProductAsync(int productId)
        {
            var response = await _httpClient.DeleteAsync($"https://fakestoreapi.com/products/{productId}");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"No se pudo eliminar el producto. Código de respuesta: {(int)response.StatusCode}");
            }
        }

    }
}
