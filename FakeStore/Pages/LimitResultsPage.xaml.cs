using FakeStore.Dtos;
using FakeStore.Services;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace FakeStore.Pages;

public partial class LimitResultsPage : ContentPage
{

    private readonly LimitResultsService _limitResultsService = new LimitResultsService();
    public ObservableCollection<ProductDto> LimitedProducts { get; set; } = new ObservableCollection<ProductDto>();

    public LimitResultsPage()
	{
		InitializeComponent();
        LimitedProductsCollection.ItemsSource = LimitedProducts;
    }

    private async void OnGetLimitedProductsClicked(object sender, EventArgs e)
    {
        
        var limitText = LimitEntry.Text;

        
        if (int.TryParse(limitText, out int limit) && limit > 0)
        {
            await LoadLimitedProductsAsync(limit);
        }
        else
        {
            await DisplayAlert("Error", "Introduce un número válido", "OK");
        }
    }
    private async Task LoadLimitedProductsAsync(int limit)
    {
        try
        {
            var products = await _limitResultsService.GetLimitedProductsAsync(limit);

         
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