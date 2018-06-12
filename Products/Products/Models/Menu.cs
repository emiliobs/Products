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
                    MainViewModel.GetInstance().Login = new LoginViewModel();
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
