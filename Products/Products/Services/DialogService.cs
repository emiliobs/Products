using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Products.Services
{
    public class DialogService
    {
        public async Task<String> ShowImageOptions()
        {
            return await Application.Current.MainPage.DisplayActionSheet(
                "Where do you take the Image.?",
                "Cancel",
                null,
                "From gallery",
                "From Camera");
        }

        public async Task<bool> ShowConfirm(string titulo, string mensaje)
        {
           return  await Application.Current.MainPage.DisplayAlert(titulo, mensaje, "Yes","No");

        }
        public async Task ShowMessage(string titulo, string mensaje)
        {
            await Application.Current.MainPage.DisplayAlert(titulo, mensaje, "Aceptar");     

        }
    }
}
