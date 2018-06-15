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
        public void SetMainPage(string pageName)
        {
            switch(pageName)
            {
                case "LoginView":
                    Application.Current.MainPage = new NavigationPage(new LoginView());
                    break;
                case "MasterView":
                    Application.Current.MainPage = new MasterView();
                    break;
            }
        }


        public async Task NavigateOnLogin(string viewName)
        {  
            //NewCategoryView
            switch (viewName)
            {
                  case "NewCustomerView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewCustomerView());
                    break;
            }

        }

        public async Task NavigateOnMaster( string viewName)
        {
            //Aqui oculto la navegación:
            App.Master.IsPresented = false;

            //NewCategoryView
            switch (viewName)
            {
                case "CategoriesView":
                    await App.Navigator.PushAsync(new CategoriesView());                           
                    break;
                case "ProductView":
                    await App.Navigator.PushAsync(new ProductView());      
                    break;
                case "NewCategoryView":
                    await App.Navigator.PushAsync(new NewCategoryView());  
                    break;
                case "EditCategoryView":
                    await App.Navigator.PushAsync(new EditCategoryView());
                    break;
                case "NewProductView":
                    await App.Navigator.PushAsync(new NewProductView());
                    break;
                case "EditProductView":
                    await App.Navigator.PushAsync(new EditProductView());
                    break;
                case "UbicationsView":
                    await  App.Navigator.PushAsync(new UbicationsView());
                    break;
                case "SyncView":
                    await App.Navigator.PushAsync(new SyncView());
                    break;

            }

        }

        public async Task BackOnLogin()
        {
            await Application.Current.MainPage.Navigation.PopAsync();

        }

        public async Task BackOnMaster()
        {
            await App.Navigator.PopAsync();
        }
        
    }
}
