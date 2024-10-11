using FakeStore.Dtos;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace FakeStore.Pages;

public partial class LimitResultsPage : ContentPage
{

    private HttpClient _httpClient = new HttpClient();
    public ObservableCollection<ProductDto> LimitedProducts { get; set; } = new ObservableCollection<ProductDto>();

    public LimitResultsPage()
	{
		InitializeComponent();
        LimitedProductsCollection.ItemsSource = LimitedProducts;
    }

    private async void OnGetLimitedProductsClicked(object sender, EventArgs e)
    {
        // Obtener el valor introducido en la Entry
        var limitText = LimitEntry.Text;

        // Verificar si el valor es un número válido
        if (int.TryParse(limitText, out int limit) && limit > 0)
        {
            await GetLimitedProductsAsync(limit);
        }
        else
        {
            await DisplayAlert("Error", "Introduce un número válido", "OK");
        }
    }

    private async Task GetLimitedProductsAsync(int limit)
    {
        try
        {
            // Construir la URL para la API con el límite de productos
            var response = await _httpClient.GetStringAsync($"https://fakestoreapi.com/products?limit={limit}");
            var products = JsonSerializer.Deserialize<List<ProductDto>>(response);

            // Limpiar la lista antes de agregar nuevos productos
            LimitedProducts.Clear();
            foreach (var product in products)
            {
                LimitedProducts.Add(product);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo obtener los productos: {ex.Message}", "OK");
        }
    }
}