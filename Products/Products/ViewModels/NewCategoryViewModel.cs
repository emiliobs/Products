namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Products.Models;
    using Products.Services;
    using System.Windows.Input;
    using Xamarin.Forms;


    public class NewCategoryViewModel :BaseViewModel
    {

        #region Services

        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;

        #endregion

        #region Atributes
        bool _isRunning;
        bool _isEnabled;
        string _description;
        #endregion

        #region Properties         

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
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
        public NewCategoryViewModel()
        {

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            IsEnabled = true;
         

        }
        #endregion

        #region Comands

        public ICommand SaveCommand { get => new RelayCommand(Save); }

        #endregion

        #region Methods
         private async void Save()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage("Error","You must enter a Category description.");
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

            //aqui crea la nueva categoria ingresade desde form:
            var category = new Category()
            {
              Description = Description,
            };
                
            var apiSecurity = Application.Current.Resources["ApiProduct"].ToString();

            var mainViewModel = MainViewModel.GetInstance();
            
            var response = await apiService.Post(apiSecurity,"/api","/Categories",
                           mainViewModel.Token.TokenType,mainViewModel.Token.AccessToken,category);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                 
                await dialogService.ShowMessage("Error", response.Message);

                Description = string.Empty;
                
                

                return;
            }

            //Si llega aqui el nuevo registro se ha creado fde forma correcta:
            //y lo regreso a la página anterior de forma dinamica:

            //Aqui obtengo el resultado creado de la nueva categoria:
            category = (Category)response.Result;

            //aqui devuelvo el resultado a la clase categoryviewmodel  por medio del patron singleton:
            var categoViewModel = CategoriesViewModel.GetInstance();
            categoViewModel.AddCategory(category);

            await navigationService.Back();

            IsRunning = false;
            IsEnabled = true;



        }

       

        #endregion
    }
}
