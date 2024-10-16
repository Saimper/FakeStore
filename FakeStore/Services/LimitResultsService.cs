using FakeStore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FakeStore.Services
{
    public class LimitResultsService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<List<ProductDto>> GetLimitedProductsAsync(int limit)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://fakestoreapi.com/products?limit={limit}");
                var products = JsonSerializer.Deserialize<List<ProductDto>>(response);
                return products ?? new List<ProductDto>();
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo obtener los productos: {ex.Message}");
            }
        }
    }
}
