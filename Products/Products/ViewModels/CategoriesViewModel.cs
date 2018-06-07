namespace Products.ViewModels
{
    using Products.Models;
    using Products.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using Xamarin.Forms;

    public class CategoriesViewModel : BaseViewModel
    {

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion
        #region Atributes
        ObservableCollection<Category> _categories;
        #endregion

        #region Properties
        public ObservableCollection<Category> Categories
        { get => _categories;
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Constructor
        public CategoriesViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();

            LoadCategories();
                
        }

        #endregion

        #region Methods
        private async void LoadCategories()
        {
            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);

                return;
            }

            var apiSecurity = Application.Current.Resources["ApiProduct"].ToString();
           var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Category>(
                apiSecurity,
                "/api",
                "/Categories",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {

                await dialogService.ShowMessage("Error", response.Message);

                return;
            }

            var categories = (List<Category>)response.Result;
        }
        #endregion
    }
}
