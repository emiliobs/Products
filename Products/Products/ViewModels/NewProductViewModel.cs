namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Products.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;

    public class NewProductViewModel : BaseViewModel
    {
        #region Services
        DialogService dialogService;
        #endregion

        #region Atributes

        bool _isRunning;
        bool _isEnabled;

        #endregion

        #region Properties

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
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastPurchas { get; set; }
        public double Stock { get; set; }
        public string Remarks { get; set; }
        public string Image { get; set; }

        #endregion

        #region Construct

        public NewProductViewModel()
        {
            dialogService = new DialogService();

            IsActive = true;
            IsEnabled = true;
            LastPurchas = DateTime.Now;
        }

        #endregion

        #region Commands
        public ICommand SaveCommand { get => new RelayCommand(Save); }

        private async void Save()
        {
            await dialogService.ShowMessage("Super", "Sigue Programando.......");
        }
        #endregion

        #region Mehotds

        #endregion
    }
}
