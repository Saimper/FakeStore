using FakeStore.Dtos;
using FakeStore.Services;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace FakeStore.Pages;

public partial class SortResultsPage : ContentPage
{

    private readonly SortResultsService _sortResultsService = new SortResultsService();
    public ObservableCollection<ProductDto> SortedProducts { get; set; } = new ObservableCollection<ProductDto>();

    public SortResultsPage()
    {
        InitializeComponent();
        SortedProductsCollection.ItemsSource = SortedProducts;
    }

    private async void OnGetSortedProductsClicked(object sender, EventArgs e)
    {
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
            SortedProducts.Clear();

            var products = await _sortResultsService.GetSortedProductsAsync(sortParam);
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