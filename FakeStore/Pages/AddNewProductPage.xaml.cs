using System.Text.Json;
using System.Text;
using FakeStore.Dtos;

namespace FakeStore.Pages;

public partial class AddNewProductPage : ContentPage
{
    private HttpClient _httpClient = new HttpClient();
    public AddNewProductPage()
	{
		InitializeComponent();
	}

    private async void OnAddProductClicked(object sender, EventArgs e)
    {
        // Verifica que todos los campos tengan valores válidos
        if (string.IsNullOrWhiteSpace(ProductTitleEntry.Text)
            || string.IsNullOrWhiteSpace(ProductPriceEntry.Text)
            || string.IsNullOrWhiteSpace(ProductDescriptionEntry.Text)
            || string.IsNullOrWhiteSpace(ProductImageEntry.Text)
            || ProductCategoryPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Error", "Por favor, rellene todos los campos.", "OK");
            return;
        }

        // Crear el nuevo producto a partir de los datos ingresados
        var newProduct = new ProductDto
        {
            title = ProductTitleEntry.Text,
            price = decimal.Parse(ProductPriceEntry.Text),
            description = ProductDescriptionEntry.Text,
            image = ProductImageEntry.Text,
            category = ProductCategoryPicker.SelectedItem.ToString()
        };

        await AddProductAsync(newProduct);
    }

    private async Task AddProductAsync(ProductDto product)
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

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            
            var response = await _httpClient.PostAsync("https://fakestoreapi.com/products", content);

            if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var addedProduct = JsonSerializer.Deserialize<ProductDto>(responseBody);

              
                await DisplayAlert("Éxito", $"Producto agregado correctamente con ID: {addedProduct.id}", "OK");

               
                await Navigation.PushAsync(new GetSingleProductPage(addedProduct));
            }
            else
            {
                await DisplayAlert("Error", $"No se pudo agregar el producto. Código de respuesta: {(int)response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }



}
