using FakeStore.Dtos;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Diagnostics;

namespace FakeStore.Pages;

public partial class ProductListPage : ContentPage
{

    private HttpClient _httpClient = new HttpClient();
    public ObservableCollection<ProductDto> Products { get; set; } = new ObservableCollection<ProductDto>(); // Usa ProductDto aquí

    public ProductListPage()
	{
		InitializeComponent();
        ProductsCollection.ItemsSource = Products; // Asegúrate de que este nombre coincida con el del XAML
        GetProductsAsync();
    }
    public async Task UpdateProductList()
    {
        ProductsCollection.ItemsSource = null;
        ProductsCollection.ItemsSource = Products;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetProductsAsync();
    }

    private async Task GetProductsAsync()
    {

        try
        {
            Products.Clear();

            var response = await _httpClient.GetStringAsync("https://fakestoreapi.com/products");
            var products = JsonSerializer.Deserialize<List<ProductDto>>(response); // Usa ProductDto para deserializar

            // Verifica si la lista de productos no es nula y cuántos se han deserializado
            if (products != null)
            {
               
                foreach (var product in products)
                {
                    Products.Add(product); // Agrega cada producto al ObservableCollection
                }
            }
            else
            {
                await DisplayAlert("Error", "No se pudieron deserializar los productos.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo obtener los productos: {ex.Message}", "OK");
        }

       
    }
    private async void OnViewDetailsClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedProduct = button?.CommandParameter as ProductDto;

        if (selectedProduct != null)
        {
            // Navega a la página de detalles con el producto seleccionado
            await Navigation.PushAsync(new GetSingleProductPage(selectedProduct));
        }
    }
}

