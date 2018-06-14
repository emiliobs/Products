using Products.Models;
using Products.Services;
using Products.ViewModels;
using Products.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Products
{
	public partial class App : Application
	{
        #region Services

        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        DataService dataService;


        #endregion

        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static MasterView Master { get; internal set; }
        #endregion

        #region Constructs
        public App()
        {
            InitializeComponent();

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();

            var token = dataService.First<TokenResponse>(false);
            if (token != null &&
                token.IsRemembered &&
                token.Expires > DateTime.Now)
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = token;
                mainViewModel.Categories = new CategoriesViewModel();
                navigationService.SetMainPage("MasterView");
            }
            else
            {
                navigationService.SetMainPage("LoginView");
            }


            ////MainPage = new MasterView();
            //MainPage = new NavigationPage(new LoginView());
        }
        #endregion

        #region Methods

        public static Action LoginFacebookFail
        {
            get
            {
                return new Action(() => Current.MainPage =
                                  new NavigationPage(new LoginView()));
            }
        }

        public static void LoginFacebookSuccess(FacebookResponse profile)
        {
            Current.MainPage = new MasterView();
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
