﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Products.Services
{
    public class DialogService
    {
        public async Task ShowMessage(string titulo, string mensaje)
        {
            await Application.Current.MainPage.DisplayAlert(titulo, mensaje, "Aceptar");


        }
    }
}
