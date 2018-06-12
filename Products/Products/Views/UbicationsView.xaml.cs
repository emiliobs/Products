﻿namespace Products.Views
{
    using Plugin.Geolocator.Abstractions;

    using Xamarin.Forms;
    using Xamarin.Forms.Maps;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UbicationsView : ContentPage
	{
        #region services

        GeolocatorService geolocatorService;

        #endregion

        #region Contructor
        public UbicationsView()
        {
            InitializeComponent();
            geolocatorService = new GeolocatorService();

            MovepMapToCurrentPosition();
        }


        #endregion

        #region Methods


        private async void MovepMapToCurrentPosition()
        {
            await geolocatorService.GetLocation();
            if (geolocatorService.Latitude != 0 || geolocatorService.Longitude != 0)
            {
                var position = new Xamarin.Forms.Maps.Position(geolocatorService.Latitude, geolocatorService.Longitude);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(05)));

            }
        }

        #endregion


    }
}