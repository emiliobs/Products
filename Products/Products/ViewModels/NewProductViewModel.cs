namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Products.Helpers;
    using Products.Models;
    using Products.Services;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class NewProductViewModel : BaseViewModel
    {
        #region Services
        DialogService dialogService;
        ApiService ApiService;
        NavigationService navigationService;
        #endregion

        #region Atributes

        ImageSource _imageSource;
        MediaFile file;
        bool _isRunning;
        bool _isEnabled;


        #endregion

        #region Properties


        public ImageSource ImageSource
        {
            get => _imageSource;
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
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
        public string Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastPurchas { get; set; }
        public string Stock { get; set; }
        public string Remarks { get; set; }
        public string Image { get; set; }

        #endregion

        #region Construct

        public NewProductViewModel()
        {
            ApiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            ImageSource = "noimage";
            //Image = "noimage";
            IsActive = true;
            IsEnabled = true;
            LastPurchas = DateTime.Now;
        }

        #endregion

        #region Commands
        public ICommand SaveCommand { get => new RelayCommand(Save); }
        public ICommand ChangeImageCommand { get => new RelayCommand(ChangeImage); }

        #endregion

        #region Mehotds


        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                var source = await dialogService.ShowImageOptions();

                if (source == "Cancel")
                {
                    file = null;
                    return;
                }

                if (source == "From Camera")
                {
                    file = await CrossMedia.Current.TakePhotoAsync(

                        new StoreCameraMediaOptions
                        {

                            Directory = "Sample",
                            Name = "test.jpg",
                            PhotoSize = PhotoSize.Small,
                        }    
                           );
                }
                else
                {
                    file = await CrossMedia.Current.PickPhotoAsync();
                }
            }
            else
            {
                file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() => 
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }
        private async void Save()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage("Error", "You must enter a Product descriptin");
                return;
            }

            if (string.IsNullOrEmpty(Price))
            {
                await dialogService.ShowMessage("Error", "You must enter a Product Price");
                return;
            }

            var price = decimal.Parse(Price);
            if (price < 0)
            {
                await dialogService.ShowMessage("Error", "The Price must be a value greather or equals than Zero.");
                return;
            }

            if (string.IsNullOrEmpty(Stock))
            {
                await dialogService.ShowMessage("Error", "You must enter a Product Stock");
                return;
            }

            var stock = double.Parse(Stock);
            if (stock < 0)
            {
                await dialogService.ShowMessage("Error", "The Stock must be a value grather or equals than zero");
                return;
            }


            IsRunning = true;
            IsEnabled = false;

            var mainViewModel = MainViewModel.GetInstance();

            byte[] imageArray = null;

            if (file != null)
            {
                imageArray = FilesHelper.ReadFully(file.GetStream());
                file.Dispose();
            }

            
            var product = new Product
            {
                CategoryId = mainViewModel.Category.CategoryId,
                Description = Description,
                ImageArray = imageArray,
                IsActive = IsActive,
                LastPurchase = LastPurchas,
                Price = price,
                Remarks = Remarks,
                Stock = stock,
            };


            var connection = await ApiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var apiSecurity = Application.Current.Resources["ApiProduct"].ToString();

            var response = await ApiService.Post(apiSecurity, "/Api", "/Products", mainViewModel.Token.TokenType,
                                                 mainViewModel.Token.AccessToken, product);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await dialogService.ShowMessage("Error", response.Message);

                return;
            }
            product = (Product)response.Result;

            var productsViewModel = ProductViewModel.GetInstance();
            productsViewModel.AddProduct(product);

            await navigationService.BackOnMaster();

            IsRunning = false;
            IsEnabled = true;




        }
        #endregion
    }
}
