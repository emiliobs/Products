namespace Products.Models
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;
    using ViewModels;

    public class Product
    {
        #region Service
        NavigationService navigationService;
        #endregion

        #region Properties
       
        [PrimaryKey, AutoIncrement]
        public int ProductId { get; set; }

        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }

        [ManyToOne]
        public Category Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public string Image { get; set; }

        public double Stock { get; set; }

        public DateTime LastPurchase { get; set; }

        public string Remarks { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(Image))
                {
                    return "noimage";
                }
                else
                {
                    //return  $"http://productsbackend5.azurewebsites.net/{Image.Substring(1)}";
                    return $"https://productsapi5.azurewebsites.net/{Image.Substring(1)}";
                }
            }
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
