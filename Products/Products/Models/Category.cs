namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using Products.Services;
    using Products.ViewModels;
    using Products.Views;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class Category
    {

        #region services

        NavigationService navigationService;
        DialogService dialogService;

        #endregion

        #region Properties
        [PrimaryKey]
        public int CategoryId { get; set; }
        public string Description { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Product> Products { get; set; }
        #endregion

        #region Constructor

        public Category()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
        }

        #endregion

        #region Commands

        public ICommand DeleteCommand { get => new RelayCommand(Delete); }        
        public ICommand EditCommand { get => new RelayCommand(Edit); }
        public ICommand SelectCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectCategory);
            }
        }


        #endregion

        #region Methods

        private async void Delete()
        {
            var response = await dialogService.ShowConfirm("Confirm.","Are you sure to delete this record?");

            if (!response)
            {
                return;
            }

            await CategoriesViewModel.GetInstance().DeleteCategory(this);
        }


        public override int GetHashCode()
        {
            return CategoryId;
        }

        private async void Edit()
        {

            MainViewModel.GetInstance().EditCategory = new EditCategoryViewModel(this);
            await navigationService.NavigateOnMaster("EditCategoryView");
        }

        private async void SelectCategory()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Category = this;
            mainViewModel.Products = new ProductViewModel(Products);
            await navigationService.NavigateOnMaster("ProductView");
            //await Application.Current.MainPage.Navigation.PushAsync(new ProductsView());

        }
        #endregion


    }
}
