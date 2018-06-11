using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Products.Models
{
    public class Menu
    {
        #region Properties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string pageName { get; set; }
        #endregion

        #region Comands

        public ICommand MyProperty { get; set; }

        #endregion

    }
}
