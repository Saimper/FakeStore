using FakeStore.Dtos;
using System.Text.Json;

namespace FakeStore.Pages;

public partial class GetAllCategoriesPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public GetAllCategoriesPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
    }

    // Manejador del evento SelectedIndexChanged para el Picker
    private void OnCategorySelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private async void OnGetProductsClicked(object sender, EventArgs e)
    {
        // Obtener la categoría seleccionada
        var selectedCategory = CategoryPicker.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selectedCategory))
        {
            await DisplayAlert("Error", "Por favor, selecciona una categoría.", "OK");
            return;
        }

        
        var products = await GetProductsByCategoryAsync(selectedCategory);
        ProductsListView.ItemsSource = products;
    }

    private async Task<List<ProductDto>> GetProductsByCategoryAsync(string category)
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
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            return new List<ProductDto>();
        }
    }

}
