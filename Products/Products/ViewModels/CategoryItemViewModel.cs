namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Products.Models;
    using Products.Views;
    using System.Windows.Input;
    using Xamarin.Forms;


    public class CategoryItemViewModel : Category
    {
        #region Commands

        //public ICommand SelectCategoryCommand { get => new RelayCommand(SelectCategory); }

        //#endregion

        //#region Methods

        //private async void SelectCategory()
        //{
        //    MainViewModel.GetInstance().Products = new ProductViewModel(Products);
        //    await Application.Current.MainPage.Navigation.PushAsync(new ProductsView());
           
        //}

        #endregion
    }
}
