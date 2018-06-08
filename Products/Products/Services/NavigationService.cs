namespace Products.Services
{
    using Products.Views;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class NavigationService
    {
        public async Task Navigate( string viewName)
        {
            //NewCategoryView
            switch (viewName)
            {
                case "CategoriesView":  
                    await Application.Current.MainPage.Navigation.PushAsync(new CategoriesView());                              
                    break;
                case "ProductView":
                    await Application.Current.MainPage.Navigation.PushAsync(new ProductView());
                    break;
                case "NewCategoryView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewCategoryView());
                    break;
            }

        }

        public async Task Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
