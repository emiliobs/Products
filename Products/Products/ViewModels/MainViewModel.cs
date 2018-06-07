using Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Products.ViewModels
{
   public class MainViewModel
    {
        #region Properties
        public LoginViewModel Login { get; set; }
        public CategoriesViewModel Categories { get; set; }
        public ProductViewModel  Products { get; set; }
        public TokenResponse Token { get; set; }
        #endregion

        #region Contruct
        public MainViewModel()
        {
            //Patrón singleton
            _instance = this;

            Login = new LoginViewModel();
        }
        #endregion

        #region Singleton

        static MainViewModel _instance;

        public static MainViewModel GetInstance()
        {
            if (_instance == null)
            {
                return new MainViewModel();
            }

            return _instance;
        }


        #endregion

    }
}
