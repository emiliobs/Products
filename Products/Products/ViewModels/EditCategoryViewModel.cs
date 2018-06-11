namespace Products.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Products.Models;
    using Products.Services;
    using Xamarin.Forms;
    public class EditCategoryViewModel : BaseViewModel
    {
        #region Services

        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;

        #endregion

        #region Atributes
        private Category category;
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

       

        #region Constructor
        public EditCategoryViewModel(Category category)
        {
            this.category = category;

            apiService = new ApiService();

            dialogService = new DialogService();

            navigationService = new NavigationService();

            Description = category.Description;

            IsEnabled = true;
           
        }
        #endregion

        #region Commands

        public ICommand EditCommand { get => new RelayCommand(Edit); }

        #endregion


        #region Methods

        private async void Edit()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage("Error", "You must enter a Category description.");
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

            //aqui esdito la el registri que envia el cosntructor:
            category.Description = Description;

            var apiSecurity = Application.Current.Resources["ApiProduct"].ToString();

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.Put(apiSecurity, "/api", "/Categories",
                           mainViewModel.Token.TokenType, mainViewModel.Token.AccessToken, category);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await dialogService.ShowMessage("Error", response.Message);

                Description = string.Empty;



                return;
            }

           
            //aqui devuelvo el resultado a la clase categoryviewmodel  por medio del patron singleton:
            var categoViewModel = CategoriesViewModel.GetInstance();
            categoViewModel.UpdateCategory(category);

            await navigationService.BackOnMaster();

            IsRunning = false;
            IsEnabled = true;
        }

        #endregion
    }
}
