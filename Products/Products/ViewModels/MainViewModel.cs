using System;
using System.Collections.Generic;
using System.Text;

namespace Products.ViewModels
{
   public class MainViewModel
    {
        #region Properties
        public LoginViewModel Login { get; set; }
        #endregion

        #region Contruct
        public MainViewModel()
        {
            Login = new LoginViewModel();
        }
        #endregion

    }
}
