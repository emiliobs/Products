namespace Products.Models
{   
    using GalaSoft.MvvmLight.Command;
    using Products.Services;
    using Products.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;

    public class Menu
    {
        #region services

        NavigationService navigationService;
        DataService dataService;

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
            }
        }

        #endregion

    }
}
