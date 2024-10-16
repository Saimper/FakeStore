using System.Text.Json;
using System.Text;
using FakeStore.Dtos;
using FakeStore.Services;

namespace FakeStore.Pages;

public partial class AddNewProductPage : ContentPage

    //Para el profesor: Aquí solamente obtenemos los datos, y llamamos al servicio para consumir la API
{
    private readonly AddNewProductService _addNewProductService = new AddNewProductService();

    public AddNewProductPage()
	{
		InitializeComponent();
	}
    private async void OnAddProductClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ProductTitleEntry.Text)
            || string.IsNullOrWhiteSpace(ProductPriceEntry.Text)
            || string.IsNullOrWhiteSpace(ProductDescriptionEntry.Text)
            || string.IsNullOrWhiteSpace(ProductImageEntry.Text)
            || ProductCategoryPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Error", "Por favor, rellene todos los campos.", "OK");
            return;
        }

        var newProduct = new ProductDto
        {
            title = ProductTitleEntry.Text,
            price = decimal.Parse(ProductPriceEntry.Text),
            description = ProductDescriptionEntry.Text,
            image = ProductImageEntry.Text,
            category = ProductCategoryPicker.SelectedItem.ToString()
        };

        try
        {
            var addedProduct = await _addNewProductService.AddProductAsync(newProduct);
            await DisplayAlert("Éxito", $"Producto agregado correctamente con ID: {addedProduct.id}", "OK");
            await Navigation.PushAsync(new GetSingleProductPage(addedProduct));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }

}




