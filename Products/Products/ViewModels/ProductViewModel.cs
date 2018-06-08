namespace Products.ViewModels 
{
    using Products.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ProductViewModel: BaseViewModel
    {
        #region Atributes
        private List<Product> products;

        ObservableCollection<Product> _products;

        #endregion

        #region Properties

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

            ProductList = new ObservableCollection<Product>(products.OrderBy(p => p.Description));
          
        }

        

        #endregion


        #region Methods

        #endregion

    }
}
