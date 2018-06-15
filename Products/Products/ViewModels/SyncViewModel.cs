namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Products.Models;
    using Products.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class SyncViewModel :BaseViewModel
    {
        #region Services

        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        DataService dataService;

        #endregion

        #region Atributes
        string _message;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
       

       public string Message
        {
            get => _message;
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Contructors

        public SyncViewModel()
        {
           

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();

            Message = "Prees Sync button to start.";

           IsEnabled = true;
        }

        #endregion

        #region Commands

        public ICommand SyncCommand { get => new RelayCommand(Sync); }


        #endregion

        #region Methods

        private async void Sync()
        {
            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await dialogService.ShowMessage("Error.", connection.Message);
                return;
            }

            var products = dataService.Get<Product>(false).Where(p => p.PendingToSave).ToList();

            if (products.Count.Equals(0))
            {
                await dialogService.ShowMessage("Error.", "There aren't Producs to Sync.");

                IsRunning = false;
                IsEnabled = true;

                return;

             
            }

            var apiSecurity = Application.Current.Resources["ApiProduct"].ToString();
            var mainViewModel = MainViewModel.GetInstance();
            foreach (var product in products)
            {
                var response = await apiService.Post(apiSecurity,
                                                     "/Api",
                                                     "/Products",
                                                     mainViewModel.Token.TokenType,
                                                     mainViewModel.Token.AccessToken,
                                                     product);

                if (response.IsSuccess)
                {
                    product.PendingToSave = false;
                    dataService.Update(product);

                }
            }

            await dialogService.ShowMessage("Confirmation.", "Sync OK.");

            await navigationService.BackOnMaster();

            IsRunning = false;
            IsEnabled = true;

        }


        #endregion
    }
}
