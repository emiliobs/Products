﻿using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Products.Views
{
    public class GeolocatorService
    {
        #region Properties
        public double Latitude
        {
            get;
            set;
        }

        public double Longitude
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public GeolocatorService()
        {
           
        }

        #endregion

        #region Methods

        public async Task GetLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var location = await locator.GetPositionAsync();
                Latitude = location.Latitude;
                Longitude = location.Longitude;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        #endregion
    }
}
