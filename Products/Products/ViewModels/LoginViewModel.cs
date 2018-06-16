namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Products.Services;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {

        #region Services

        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        DataService dataService;

        #endregion

        #region Atributes
        string _email;
        string _password;
        bool _isToggled;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsToggled
        {
            get => _isToggled;
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
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

        #region Construtor
        public LoginViewModel()
        {
           

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();

            IsToggled = true;
            IsEnabled = true;

            //Email = "emilio@hotmail.com";
            //Password = "555555";

        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get => new RelayCommand(Login); }
        public ICommand RegisterNewUserCommand { get => new RelayCommand(RegisterNewUse); }



        #endregion

        #region Methods

        private async void RegisterNewUse()
        {
            MainViewModel.GetInstance().NewCustomer = new NewCustomerViewModel();
            await navigationService.NavigateOnLogin("NewCustomerView");
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Error","You must enter an E-Mail.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "You must enter a Password.");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", connection.Message);

                return;
            }

            var apiSecurity = Application.Current.Resources["ApiProduct"].ToString();

            //aqui obtenfo el token de seguridad:
            var response = await apiService.GetToken(apiSecurity, Email,Password);

            if (response == null)
            {
                IsRunning = false;
                IsEnabled = true;


                await dialogService.ShowMessage("Error", response.ErrorDescription);

                Password = string.Empty;

                return;
            }

            if (string.IsNullOrEmpty(response.AccessToken))
            {
                IsRunning = false;
                IsEnabled = true;

                await dialogService.ShowMessage("Error", response.ErrorDescription);

                Password = string.Empty;
                return;
            }

            //aqui llamo a la propeidad isremembered y le guardo el istoggled (si es tru or false)
            response.IsRemembered = IsToggled;
            //aqui guardo el paswword en memoria:
            response.Password = Password;
            dataService.DeleteAllAndInsert(response);
           

            //Apuntador del patron singleton
            var mainViewModel = MainViewModel.GetInstance();
            //aqui paso el toke para enviarlo a categoriesviewmodel y porder manupular los datos del api:
            mainViewModel.Token = response;
            //hago referencia a una view a otra view solo cuando la necesito invocar...
            mainViewModel.Categories = new CategoriesViewModel();
            navigationService.SetMainPage("MasterView");

            //await Application.Current.MainPage.Navigation.PushAsync(new CategoriesView());
            //await dialogService.ShowMessage("Taraaaaaaann.!!","Welcome to Product sistem...");

            Email = string.Empty;
            Password = string.Empty;

            IsRunning = false;
            IsEnabled = true; 

        }
        #endregion
    }
}
