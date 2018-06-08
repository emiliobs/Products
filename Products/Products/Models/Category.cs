using GalaSoft.MvvmLight.Command;
using Products.Services;
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

        #region services

        NavigationService navigationService;

        #endregion

        #region Properties
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }
        #endregion

        #region Constructor

        public Category()
        {
            navigationService = new NavigationService();
        }

        #endregion

        #region Commands

        public ICommand SelectCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectCategory);
            }
        }

        #endregion

        #region Methods

        private async void SelectCategory()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Products = new ProductViewModel(Products);
            await navigationService.Navigate("ProductView");
            //await Application.Current.MainPage.Navigation.PushAsync(new ProductsView());

        }
        #endregion


    }
}
