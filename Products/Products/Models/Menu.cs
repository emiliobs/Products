namespace Products.Models
{   
    using GalaSoft.MvvmLight.Command;
    using Products.Services;
    using Products.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class Menu
    {
        #region services

        NavigationService navigationService;
        DataService dataService;
        ApiService apiService;
        DialogService dialogService;

        #endregion

        #region Properties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Contructor

        public Menu()
        {
            navigationService = new NavigationService();
            dataService = new DataService();
            apiService = new ApiService();
            dialogService = new DialogService();
       }

        #endregion

        #region Comands

        public ICommand NavigateCommand { get => new RelayCommand(Navigate); }

        #endregion

        #region Commands

        private async void Navigate()
        {
           switch(PageName)
           {
                case "LoginView":
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token.IsRemembered = false;
                    dataService.Update(mainViewModel.Token);
                    mainViewModel.Login = new LoginViewModel();
                    navigationService.SetMainPage("LoginView");
                    break;

                case "UbicationsView":
                    MainViewModel.GetInstance().Ubications = new UbicationsViewModel();
                    await navigationService.NavigateOnMaster("UbicationsView");
                    break;
                case "SyncView":
                    SyncData();
                    break;
            }
        }


        #endregion

        #region Methods

         private async void SyncData()
        {

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error.", connection.Message);
                return;
            }

            var products = dataService.Get<Product>(false).Where(p => p.PendingToSave).ToList();

            if (products.Count.Equals(0))
            {
                await dialogService.ShowMessage("Error.", "There aren't Producs to Sync.");
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

        }

        

        #endregion

    }
}
