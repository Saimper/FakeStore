using FakeStore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FakeStore.Services
{
    public class AddNewProductService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<ProductDto> AddProductAsync(ProductDto product)
        {
            var json = JsonSerializer.Serialize(new
            {
                title = product.title,
                price = product.price,
                description = product.description,
                image = product.image,
                category = product.category
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://fakestoreapi.com/products", content);

            if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var addedProduct = JsonSerializer.Deserialize<ProductDto>(responseBody);
                return addedProduct;
            }
            else
            {
                throw new Exception($"No se pudo agregar el producto. Código de respuesta: {(int)response.StatusCode}");
            }
        }
    }

}