using FakeStore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FakeStore.Services
{
    public class UpdateProductService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<bool> UpdateProductAsync(ProductDto updatedProduct)
        {
            try
            {
                var json = JsonSerializer.Serialize(new
                {
                    title = updatedProduct.title,
                    price = updatedProduct.price,
                    description = updatedProduct.description,
                    image = updatedProduct.image,
                    category = updatedProduct.category
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://fakestoreapi.com/products/{updatedProduct.id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al actualizar el producto: {ex.Message}");
            }
        }
    }

}