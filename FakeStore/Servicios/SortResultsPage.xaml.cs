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
            await GetSortedProductsAsync(selectedSortOption);
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
            // Realizar la solicitud GET para obtener todos los productos
            var response = await _httpClient.GetStringAsync("https://fakestoreapi.com/products");
            var products = JsonSerializer.Deserialize<List<ProductDto>>(response);

            // Filtrar productos según la opción seleccionada
            switch (sortParam)
            {
                case "Precio Ascendente":
                    products = products.OrderBy(p => p.price).ToList();
                    break;
                case "Precio Descendente":
                    products = products.OrderByDescending(p => p.price).ToList();
                    break;
                case "Alfabéticamente A-Z":
                    products = products.OrderBy(p => p.title).ToList();
                    break;
                case "Alfabéticamente Z-A":
                    products = products.OrderByDescending(p => p.title).ToList();
                    break;
                default:
                    break;
            }

            // Limpiar la lista antes de agregar los productos ordenados
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