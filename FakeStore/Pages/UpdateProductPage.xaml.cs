using FakeStore.Dtos;
using System.Text.Json;
using System.Text;
using FakeStore.Services;

namespace FakeStore.Pages;

public partial class UpdateProductPage : ContentPage
{


    private readonly UpdateProductService _updateProductService = new UpdateProductService();

    public UpdateProductPage()
    {
        InitializeComponent();
    }

    private async void OnUpdateProductClicked(object sender, EventArgs e)
    {
        if (int.TryParse(IdEntry.Text, out int productId))
        {
            var updatedProduct = new ProductDto
            {
                id = productId,
                title = TitleEntry.Text,
                price = decimal.Parse(PriceEntry.Text),
                description = DescriptionEntry.Text,
                category = ProductCategoryPicker.SelectedItem.ToString(),
                image = ImageEntry.Text
            };

            var isUpdated = await _updateProductService.UpdateProductAsync(updatedProduct);

            if (isUpdated)
            {
                await DisplayAlert("Éxito", "Producto actualizado correctamente.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar el producto.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "ID del producto no es válido.", "OK");
        }

    }
  }