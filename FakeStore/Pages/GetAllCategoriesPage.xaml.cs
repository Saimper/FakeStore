using FakeStore.Dtos;
using FakeStore.Services;
using System.Text.Json;

namespace FakeStore.Pages;

public partial class GetAllCategoriesPage : ContentPage
{
    private readonly GetAllCategoriesService _getAllCategoriesService = new GetAllCategoriesService();


    public GetAllCategoriesPage()
    {
        InitializeComponent();
        LoadCategoriesAsync();
    }

    
    private async void LoadCategoriesAsync()
    {
        try
        {
            var categories = await _getAllCategoriesService.GetAllCategoriesAsync();
            CategoryPicker.ItemsSource = categories;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al cargar las categorías: {ex.Message}", "OK");
        }
    }
    private async void OnGetProductsClicked(object sender, EventArgs e)
    {
        var selectedCategory = CategoryPicker.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selectedCategory))
        {
            await DisplayAlert("Error", "Por favor, selecciona una categoría.", "OK");
            return;
        }

        try
        {
            var products = await _getAllCategoriesService.GetProductsByCategoryAsync(selectedCategory);
            ProductsListView.ItemsSource = products;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al obtener los productos: {ex.Message}", "OK");
        }
    }

}
