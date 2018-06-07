using GalaSoft.MvvmLight.Command;
using Products.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Products.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        #region Services

        DialogService dialogService;

        #endregion
        #region Atributes
        string _email;
        string _password;
        bool _isToggled;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsToggled
        {
            get => _isToggled;
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Construtor
        public LoginViewModel()
        {                                       
            dialogService = new DialogService();
            IsToggled = true;
            IsEnabled = true;

        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get => new RelayCommand(Login); }

        #endregion

        #region Methods
        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Error","You must enter an E-Mail.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "You must enter a Password.");
                return;
            }


        }
        #endregion
    }
}
