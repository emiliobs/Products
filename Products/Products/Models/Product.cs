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
        DialogService dialogService;
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

        public byte[] ImageArray { get; set; }
        public bool PendingToSave { get; set; }

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
        public override int GetHashCode()
        {
            return ProductId;
        }

        #endregion

        #region onstructor
        public Product()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
        }
        #endregion

        #region Commands

        public ICommand DeleteCommand { get => new RelayCommand(Delete); }

        

        public ICommand EditCommand { get => new RelayCommand(Edit); }

        #endregion

        #region Methods

        private async void Delete()
        {
            var response = await dialogService.ShowConfirm("Confirm.", "Are you sure to delete this record?");

            if (!response)
            {
                return;
            }

            ProductViewModel.GetInstance().DeleteProduct(this);
        }

        private async void Edit()
        {
            MainViewModel.GetInstance().EditProduct = new EditProductViewModel(this);
            await navigationService.NavigateOnMaster("EditProductView");
        }
        #endregion

      }

       
    }
