namespace Products.ViewModels 
{
    using Products.Models;
    using Products.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class ProductViewModel: BaseViewModel
    {
        #region services

        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;

        #endregion

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
            apiService = new ApiService();
            navigationService = new NavigationService();
            dialogService = new DialogService();
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

        #endregion

        #region Methods

        public void AddProduct(Product product)
        {
            IsRefreshing = true;

            products.Add(product);
            ProductList = new ObservableCollection<Product>(products.OrderBy(p => p.Description));

            IsRefreshing = false;

        }

        public void Update(Product product)
        {
            IsRefreshing = true;

            var oldProduct = products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();
            oldProduct = product;
            ProductList = new ObservableCollection<Product>(products.OrderBy(p => p.Description));

            IsRefreshing = false;
        }

        public async void DeleteProduct(Product product)
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", connection.Message);
                return;

            }

            var mainViewModel = MainViewModel.GetInstance();

            var apiSecurity = Application.Current.Resources["ApiProduct"].ToString();   
            var response = await apiService.Delete(apiSecurity,"/api","/Products",
                                                   mainViewModel.Token.TokenType, 
                                                   mainViewModel.Token.AccessToken,
                                                   product);

            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error",response.Message);
                return;
            }

            products.Remove(product);
            ProductList = new ObservableCollection<Product>(products.OrderBy(p => p.Description));

            IsRefreshing = false;

        }

        #endregion

    }
}
