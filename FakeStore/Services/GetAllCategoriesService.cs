using FakeStore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FakeStore.Services
{
    public class GetAllCategoriesService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        // Obtener todas las categorías
        public async Task<List<string>> GetAllCategoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://fakestoreapi.com/products/categories");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<string>>(responseBody);
                return categories ?? new List<string>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al obtener las categorías: {ex.Message}");
            }
        }

        // Obtener productos por categoría
        public async Task<List<ProductDto>> GetProductsByCategoryAsync(string category)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://fakestoreapi.com/products/category/{category}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<ProductDto>>(responseBody);
                return products ?? new List<ProductDto>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al obtener productos: {ex.Message}");
            }
        }


    }
}