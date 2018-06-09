﻿namespace Products.ViewModels 
{
    using Products.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ProductViewModel: BaseViewModel
    {
        #region Atributes
        private List<Product> products;

        ObservableCollection<Product> _products;

        bool _isRefreshing;

        #endregion

        #region Properties

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Product> ProductList
        {
            get => _products;
            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructor  
        public ProductViewModel(List<Product> products)
        {
            this.products = products;

            instance = this;

            ProductList = new ObservableCollection<Product>(products.OrderBy(p => p.Description));
          
        }



        #endregion

        #region Singlenton

        static ProductViewModel instance;

        public static ProductViewModel GetInstance()
        {
           return instance;
        }

        internal void  AddProduct(Product product)
        {
            IsRefreshing = true;

            products.Add(product);
            ProductList = new ObservableCollection<Product>(products.OrderBy(p => p.Description));

            IsRefreshing = false;
            
        }

        #endregion

        #region Methods

        #endregion

    }
}
