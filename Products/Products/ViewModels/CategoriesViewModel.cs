namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Products.Models;
    using Products.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class CategoriesViewModel : BaseViewModel
    {

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Atributes
        List<Category> categories;
        bool _isRefreshing;

        ObservableCollection<Category> _categories;
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
        public ObservableCollection<Category> CategoriesList
        { get => _categories;
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Constructor
        public CategoriesViewModel()
        {
            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();

            LoadCategories();
                
        }

        #endregion

        #region Singlenton

        static CategoriesViewModel instance;

        public static CategoriesViewModel GetInstance()
        {
            if (instance == null)
            {
                return new CategoriesViewModel();
            }

            return instance;
        }

        #endregion

        #region Commands

        public ICommand RefreshCommand { get => new RelayCommand(Refresh); }


        #endregion

        #region Methods

        private void Refresh()
        {
            LoadCategories();
        }

        public async Task  DeleteCategory(Category category)
        {
            IsRefreshing = true;                             

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", connection.Message);  
                return;
            }

             var apiSecurity = Application.Current.Resources["ApiProduct"].ToString();

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.Delete(apiSecurity, "/api", "/Categories",
                           mainViewModel.Token.TokenType, mainViewModel.Token.AccessToken, category);

            if (!response.IsSuccess)
            {                

                await dialogService.ShowMessage("Error", response.Message); 
                return;
            }

            categories.Remove(category);

            CategoriesList = new ObservableCollection<Category>(
                categories.OrderBy(c => c.Description));

            IsRefreshing = false;
        }

        public void AddCategory (Category category)
        {
            IsRefreshing = true;
            //aqui tomo el registro nuevo desde newcategoooryviewmodel y lo adicciones a la colecion
            //catogeryList que se refresca de forma dinamica.
            categories.Add(category);

            CategoriesList = new ObservableCollection<Category>(
                categories.OrderBy(c => c.Description));

            IsRefreshing = false;
        }
        public void UpdateCategory(Category category)
        {
            IsRefreshing = true;
            //aqui busco la categoria a editar:
            var oldCategory = categories.Where(c =>c.CategoryId.Equals(category.CategoryId)).FirstOrDefault();
            //aqui ya acti¡aulizao la vieja por la nueva categoria:
            oldCategory = category;
            CategoriesList = new ObservableCollection<Category>(
                categories.OrderBy(c => c.Description));

            IsRefreshing = false;
        }
        private async void LoadCategories()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);

                return;
            }

            var apiSecurity = Application.Current.Resources["ApiProduct"].ToString();
           var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Category>(
                apiSecurity,
                "/api",
                "/Categories",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {

                await dialogService.ShowMessage("Error", response.Message);

                return;
            }

            //aqui me llega una lista_
             categories = (List<Category>)response.Result;
            //aqui porgo la lista en el la colleccion a de la propiedad categorris
            CategoriesList = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));

            IsRefreshing = false;
        }
        #endregion
    }
}
