using FakeStore.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FakeStore.Services
{
    public class SortResultsService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<List<ProductDto>> GetSortedProductsAsync(string sortParam)
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://fakestoreapi.com/products");
                var products = JsonSerializer.Deserialize<List<ProductDto>>(response);

                if (products == null) return new List<ProductDto>();

                // Ordenar productos según el parámetro de orden seleccionado
                return sortParam switch
                {
                    "Precio Ascendente" => products.OrderBy(p => p.price).ToList(),
                    "Precio Descendente" => products.OrderByDescending(p => p.price).ToList(),
                    "Alfabéticamente A-Z" => products.OrderBy(p => p.title).ToList(),
                    "Alfabéticamente Z-A" => products.OrderByDescending(p => p.title).ToList(),
                    _ => products
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo obtener los productos: {ex.Message}");
            }
        }
    }

}