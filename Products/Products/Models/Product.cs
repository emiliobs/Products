namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using Products.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;

    public class Product
    {
        #region Service
        NavigationService navigationService;
        #endregion

        #region Properties
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public bool IsActive { get; set; }

        public string Image { get; set; }

        public double Stock { get; set; }

        public DateTime LastPurchase { get; set; }

        public string Remarks { get; set; }

        public string ImageFullPath
        {
            get => $"http://productsbackend5.azurewebsites.net/{Image.Substring(1)}";
        }

        #endregion

        #region onstructor
        public Product()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
       


        #endregion

        #region Methods
       
        #endregion




    }
}
