using FakeStore.Dtos;
using System.Text.Json;
using System.Text;

namespace FakeStore.Pages;

public partial class UpdateProductPage : ContentPage
{


    private readonly HttpClient _httpClient;

    public UpdateProductPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient(); 
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

         
            await UpdateProductAsync(updatedProduct);
        }
        else
        {
            await DisplayAlert("Error", "ID del producto no es válido.", "OK");
        }
    }

    private async Task UpdateProductAsync(ProductDto updatedProduct)
    {
        try
        {
            
            var json = JsonSerializer.Serialize(new
            {
                title = updatedProduct.title,
                price = updatedProduct.price,
                description = updatedProduct.description,
                image = updatedProduct.image,
                category = updatedProduct.category
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

       
            var response = await _httpClient.PutAsync($"https://fakestoreapi.com/products/{updatedProduct.id}", content);

           
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Producto actualizado correctamente.", "OK");
            }
            else
            {
                await DisplayAlert("Error", $"No se pudo actualizar el producto. Código de respuesta: {(int)response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }


    }
    }