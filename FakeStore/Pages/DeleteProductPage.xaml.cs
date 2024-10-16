using FakeStore.Dtos;
using FakeStore.Services;

namespace FakeStore.Pages;

public partial class DeleteProductPage : ContentPage
{
    private readonly DeleteProductService _deleteProductService = new DeleteProductService();


    public DeleteProductPage()
    {
        InitializeComponent();
        
    }
    private async void OnDeleteProductClicked(object sender, EventArgs e)
    {
        if (int.TryParse(IdEntry.Text, out int productId))
        {
            bool confirm = await DisplayAlert("Confirmación", "¿Está seguro de que desea eliminar este producto?", "Sí", "No");
            if (!confirm)
                return;

            try
            {
                await _deleteProductService.DeleteProductAsync(productId);
                await DisplayAlert("Éxito", $"Producto con ID: {productId} eliminado correctamente.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error al eliminar el producto: {ex.Message}", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Por favor, ingrese un ID de producto válido.", "OK");
        }

    }

  
    }
