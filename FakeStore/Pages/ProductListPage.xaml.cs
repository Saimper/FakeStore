using FakeStore.Dtos;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Diagnostics;
using FakeStore.Services;

namespace FakeStore.Pages;

public partial class ProductListPage : ContentPage
{

    private readonly ProductListService _productListService = new ProductListService();
    public ObservableCollection<ProductDto> Products { get; set; } = new ObservableCollection<ProductDto>();

    public ProductListPage()
	{
		InitializeComponent();
        ProductsCollection.ItemsSource = Products; 
        GetProductsAsync();
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

            var products = await _productListService.GetProductsAsync();
            foreach (var product in products)
            {
                Products.Add(product);
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
            await Navigation.PushAsync(new GetSingleProductPage(selectedProduct));
        }
    }
}

