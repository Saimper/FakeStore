using FakeStore.Dtos;

namespace FakeStore.Pages;

public partial class DeleteProductPage : ContentPage
{
    private readonly HttpClient _httpClient;
    public DeleteProductPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
    }
    private async void OnDeleteProductClicked(object sender, EventArgs e)
    {
        if (int.TryParse(IdEntry.Text, out int productId))
        {
            await DeleteProductAsync(productId);
        }
        else
        {
            await DisplayAlert("Error", "ID del producto no es válido.", "OK");
        }
    }

    private async Task DeleteProductAsync(int productId)
    {
        try
        {
           
            var response = await _httpClient.DeleteAsync($"https://fakestoreapi.com/products/{productId}");

           
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Producto eliminado correctamente.", "OK");
            }
            else
            {
                await DisplayAlert("Error", $"No se pudo eliminar el producto. Código de respuesta: {(int)response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }
}