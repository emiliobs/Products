﻿using GalaSoft.MvvmLight.Command;
using Products.Models;
using Products.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Products.ViewModels
{
   public class MainViewModel
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Properties
        public LoginViewModel Login { get; set; }
        public CategoriesViewModel Categories { get; set; }         
        public ProductViewModel  Products { get; set; } 
        public NewCategoryViewModel NewCategory { get; set; }
        public TokenResponse Token { get; set; }
        #endregion

        #region Contruct
        public MainViewModel()
        {
            //Patrón singleton
            _instance = this;

            Login = new LoginViewModel();
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand NewCategoryCommand { get => new RelayCommand(GoNewCategory); }


        #endregion

        #region Methods 

        private async void GoNewCategory()
        {
            NewCategory = new NewCategoryViewModel();
            await navigationService.Navigate("NewCategoryView"); 
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
