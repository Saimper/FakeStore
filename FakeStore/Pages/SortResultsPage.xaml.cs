using FakeStore.Dtos;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace FakeStore.Pages;

public partial class SortResultsPage : ContentPage
{

    private HttpClient _httpClient = new HttpClient();
    public ObservableCollection<ProductDto> SortedProducts { get; set; } = new ObservableCollection<ProductDto>();


    public SortResultsPage()
	{
		InitializeComponent();
        SortedProductsCollection.ItemsSource = SortedProducts;
    }
    private async void OnGetSortedProductsClicked(object sender, EventArgs e)
    {
        // Obtener el valor seleccionado en el Picker
        var selectedSortOption = SortPicker.SelectedItem as string;

        if (selectedSortOption != null)
        {
            string sortParam = selectedSortOption.Contains("Ascendente") ? "asc" : "desc";
            await GetSortedProductsAsync(sortParam);
        }
        else
        {
            await DisplayAlert("Error", "Por favor, selecciona una opción de orden", "OK");
        }
    }

    private async Task GetSortedProductsAsync(string sortParam)
    {
        try
        {
            // Construir la URL para la API con el parámetro de orden
            var response = await _httpClient.GetStringAsync($"https://fakestoreapi.com/products?sort={sortParam}");
            var products = JsonSerializer.Deserialize<List<ProductDto>>(response);

            // Limpiar la lista antes de agregar nuevos productos
            SortedProducts.Clear();
            foreach (var product in products)
            {
                SortedProducts.Add(product);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo obtener los productos: {ex.Message}", "OK");
        }
    }
}