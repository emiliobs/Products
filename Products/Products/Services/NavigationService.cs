﻿namespace Products.Services
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
                case "EditCategoryView":
                    await Application.Current.MainPage.Navigation.PushAsync(new EditCategoryView());
                    break;
                case "NewProductView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewProductView());
                    break;
                case "EditProductView":
                    await Application.Current.MainPage.Navigation.PushAsync(new EditProductView());
                    break;
                case "NewCustomerView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewCustomerView());
                    break;
            }

        }

        public async Task Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
