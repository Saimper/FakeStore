using FakeStore.Pages;

namespace FakeStore
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
        }
        // Navega a la página que muestra todos los productos
        private async void OnGetAllProductsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductListPage());
        }

        // Navega a la página que limita los resultados
        private async void OnLimitResultsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LimitResultsPage());
        }

        // Navega a la página para ordenar los resultados
        private async void OnSortResultsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SortResultsPage());
        }

        // Navega a la página que muestra todas las categorías
        private async void OnGetAllCategoriesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetAllCategoriesPage());
        }


        // Navega a la página para añadir un nuevo producto
        private async void OnAddNewProductClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNewProductPage());
        }

        // Navega a la página para actualizar un producto
        private async void OnUpdateProductClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new UpdateProductPage());
        }

        // Navega a la página para eliminar un producto
        private async void OnDeleteProductClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteProductPage());
        }
    }

}
