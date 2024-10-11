using FakeStore.Dtos;

namespace FakeStore.Pages;

public partial class GetSingleProductPage : ContentPage
{
	public GetSingleProductPage(ProductDto product)
	{
		InitializeComponent();
        LoadProductDetails(product);
    }
    private void LoadProductDetails(ProductDto product)
    {
        ProductImage.Source = product.image;
        ProductTitle.Text = product.title;
        ProductPrice.Text = $"${product.price:F2}";
        
    }

}