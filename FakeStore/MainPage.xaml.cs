using FakeStore.Pages;

namespace FakeStore
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
        }
      
        private async void OnGetAllProductsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductListPage());
        }

     
        private async void OnLimitResultsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LimitResultsPage());
        }

      
        private async void OnSortResultsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SortResultsPage());
        }

        
        private async void OnGetAllCategoriesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetAllCategoriesPage());
        }


       
        private async void OnAddNewProductClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNewProductPage());
        }

        
        private async void OnUpdateProductClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new UpdateProductPage());
        }

       
        private async void OnDeleteProductClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteProductPage());
        }
    }

}
