using Products.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Products
{
	public partial class App : Application
	{
        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static MasterView Master { get; internal set; }
        #endregion

        #region Constructs
        public App()
        {
            InitializeComponent();

            //MainPage = new MasterView();
            MainPage = new NavigationPage(new LoginView());
        } 
        #endregion

        #region Methods
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
