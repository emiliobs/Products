using GalaSoft.MvvmLight.Command;
using Products.ViewModels;
using Products.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Products.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }


        

        #region Commands

        public ICommand SelectCategoryCommand
        {
            get
            {
              return   new RelayCommand(SelectCategory);
            }
        }

        #endregion

        #region Methods

        private async void SelectCategory()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Products = new ProductViewModel(Products);
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsView());

        }
        #endregion


    }
}
