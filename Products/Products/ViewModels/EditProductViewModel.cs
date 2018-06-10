namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Products.Helpers;
    using Products.Models;
    using Products.Services;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EditProductViewModel : BaseViewModel
    {

        #region Services
        DialogService dialogService;
        ApiService ApiService;
        NavigationService navigationService;
        #endregion

        #region Atributes
        ImageSource _imageSource;
        MediaFile file;
        Product product;
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

        #region Contructor
        public EditProductViewModel(Product product)
        {
            this.product = product;

            ApiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            //aqui muestros los datos a editar en los textos que me vienen desde la clase product:
            Description = product.Description;
            ImageSource = product.ImageFullPath;
            Price = product.Price.ToString();
            IsActive = product.IsActive;
            LastPurchas = product.LastPurchase;
            Stock = product.Stock.ToString();
            Remarks = product.Remarks;


            IsEnabled = true;
        }
        #endregion

        #region Command
        public ICommand SaveCommand { get => new RelayCommand(Save); }        

        public ICommand ChangeImageCommand { get => new RelayCommand(ChangeImage); }  
        
        #endregion

        #region Methods
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

            var connection = await ApiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await dialogService.ShowMessage("Error",connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            byte[] imageArray = null;
            if (file != null)
            {
                imageArray = FilesHelper.ReadFully(file.GetStream());
                file.Dispose();
            }

            product.Description = Description;
            product.IsActive = IsActive;
            product.LastPurchase = LastPurchas;
            product.Price = price;
            product.Remarks = Remarks;
            product.Stock = stock;
            product.ImageArray = imageArray;

            var apiSecurity = Application.Current.Resources["ApiProduct"].ToString();

            var response = await ApiService.Put(
                apiSecurity,
                "/api",
                "/Products",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                product);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            ProductViewModel.GetInstance().Update(product);

            await navigationService.Back();

            IsRunning = true;
            IsEnabled = false;

        }

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
        #endregion
    }
}
