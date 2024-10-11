using FakeStore.Dtos;
using System.Text.Json;
using System.Text;

namespace FakeStore.Pages;

public partial class UpdateProductPage : ContentPage
{
    private HttpClient _httpClient = new HttpClient();
    private ProductDto _productToUpdate; // Producto que se va a actualizar

    private async void OnUpdateProductClicked(object sender, EventArgs e)
    {
        // Verifica que todos los campos tengan valores válidos
        if (string.IsNullOrWhiteSpace(ProductTitleEntry.Text) ||
            string.IsNullOrWhiteSpace(ProductPriceEntry.Text) ||
            string.IsNullOrWhiteSpace(ProductDescriptionEntry.Text) ||
            string.IsNullOrWhiteSpace(ProductImageEntry.Text) ||
            ProductCategoryPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Error", "Por favor, rellene todos los campos.", "OK");
            return;
        }

        // Actualizar el producto a partir de los datos ingresados
        var updatedProduct = new ProductDto
        {
            title = ProductTitleEntry.Text,
            price = double.Parse(ProductPriceEntry.Text),
            description = ProductDescriptionEntry.Text,
            image = ProductImageEntry.Text,
            category = ProductCategoryPicker.SelectedItem.ToString(),
            id = _productToUpdate.id // Asume que ProductDto tiene un campo ID
        };

        await UpdateProductAsync(updatedProduct);
    }

    private async Task UpdateProductAsync(ProductDto product)
    {
        try
        {
            var json = JsonSerializer.Serialize(new
            {
                title = product.title,
                price = product.price,
                description = product.description,
                image = product.image,
                category = product.category
            });

            // Realizar la solicitud PUT directamente
            var response = await _httpClient.PutAsync($"https://fakestoreapi.com/products/{product.id}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            // Verificar si la respuesta fue exitosa
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Producto actualizado correctamente", "OK");

                // Navegar de regreso a ProductListPage y actualizar la lista
                var productListPage = Navigation.NavigationStack.OfType<ProductListPage>().FirstOrDefault();
                if (productListPage != null)
                {
                    // Actualizar el producto en la lista existente
                    var index = productListPage.Products.IndexOf(_productToUpdate);
                    if (index >= 0)
                    {
                        productListPage.Products[index] = product; // Actualiza el producto en la lista
                        await productListPage.UpdateProductList();
                    }
                }

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar el producto", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }
}