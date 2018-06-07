namespace Products.ViewModels
{
    using Products.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    public class CategoriesViewModel
    {
        #region Atributes

        #endregion

        #region Properties
        public ObservableCollection<Category> Categories { get; set; }
        #endregion

        #region Constructor
        public CategoriesViewModel()
        {
            LoadCategories();
                
        }

        #endregion

        #region Methods
        private void LoadCategories()
        {
           
        }
        #endregion
    }
}
