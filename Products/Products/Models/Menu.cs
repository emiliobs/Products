namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using Products.Services;
    using Products.ViewModels;
    using System.Windows.Input;

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
                    navigationService.SetMainPage(PageName);
                    break;

                case "UbicationsView":
                    MainViewModel.GetInstance().Ubications = new UbicationsViewModel();
                    await navigationService.NavigateOnMaster(PageName);
                    break;
                case "SyncView":
                    MainViewModel.GetInstance().Sync = new SyncViewModel();
                    await navigationService.NavigateOnMaster(PageName);
                    break;
                case "MyProfileView":
                    MainViewModel.GetInstance().MyProfile = new MyProfileViewModel();
                    await navigationService.NavigateOnMaster(PageName);
                    break;
                    //SyncData();
            }
        }


        #endregion

        #region Methods

       

        

        #endregion

    }
}
