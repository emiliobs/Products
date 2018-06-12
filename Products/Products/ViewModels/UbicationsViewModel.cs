namespace Products.ViewModels
{
    using Products.Models;
    using Products.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    public class UbicationsViewModel
    {
        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Constructor

        #region Properties

        public ObservableCollection<Pin> Pins { get; set; }

        #endregion

        public UbicationsViewModel()
        {
            _instance = this;
            apiService = new ApiService();
            dialogService = new DialogService();
        }

        #endregion

        #region Singleton

        static UbicationsViewModel _instance;

        public static UbicationsViewModel GetInstance()
        {
            if (_instance == null)
            {
                return new UbicationsViewModel();
            }

            return _instance;
        }


        #endregion

        #region Methods
        public async Task LoadPins()
        {
            

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
              
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var apiSecurity = Application.Current.Resources["ApiProduct"].ToString();

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Ubication>(apiSecurity, "/api", "/Ubications",
                           mainViewModel.Token.TokenType, mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
               
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            var ubications = (List<Ubication>)response.Result;

            Pins = new ObservableCollection<Pin>();

            foreach (var ubication in ubications)
            {
                Pins.Add(new Pin()
                {
                    Address = ubication.Address,
                    Label = ubication.Description,
                    Position = new Position(ubication.Latitude, ubication.Longitude),
                    Type = PinType.Place,
                });
            }
        }
        #endregion
    }
}
