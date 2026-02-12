using System.Windows;
using System.Windows.Input;
using Demodem.Controllers;
using Demodem.Models;
using Demodem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Demodem.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        private AganichevShoesContext _context;
        private User _currentUser;
        private List<Product> _products;
        public ProductView(User user = null)
        {
            InitializeComponent();
            _context = new AganichevShoesContext();
            _currentUser = user;
            LoadProduct();
            CurrentUser(_currentUser);
        }

        public void LoadProduct()
        {
            _products = _context.Products
                .Include(q => q.FkManufacturerNavigation)
                .Include(q => q.FkProductNameNavigation)
                .Include(q => q.FkProductCategoryNavigation)
                .Include(q => q.FkUnitNavigation)
                .Include(q => q.FkSupplierNavigation).ToList();
            foreach (var products in _products)
            {
                ProductItemController controlles = new ProductItemController(products);
                listProducts.Items.Add(controlles);
            }
        }

        public void CurrentUser(User user)
        {
            _currentUser = user;
            if (_currentUser != null)
            {
                runFio.Text = user.Fio;
                if(user.FkUserRole == 1)
                {
                    listProducts.MouseDoubleClick += ListProducts_MouseDoubleClick;
                }
                if (user.FkUserRole == 2)
                {
                    stButt.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                runFio.Text = "Гость";
                stButt.Visibility = Visibility.Collapsed;
                stFind.Visibility = Visibility.Collapsed;
            }
        }

        private void ListProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectController = listProducts.SelectedItem as ProductItemController;

            if (selectController != null)
            {
                var openEditProduct = new EditProductView(_context, _currentUser);
             
                var productViewModel = new EditProductViewModel();

                productViewModel.SelectedProduct = selectController.DataContext as Product;

                openEditProduct.DataContext = productViewModel;
                this.Close();
                openEditProduct.Show();
            }
        }     
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var openAuth = new AuthView();
            this.Close();
            openAuth.Show();
        }     

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectController = listProducts.SelectedItem as ProductItemController;

            if (selectController != null)
            {
                try
                {
                    Product selectedProduct = selectController.DataContext as Product;
                    _context.Products.Remove(selectedProduct);
                    _context.SaveChanges();
                    MessageBox.Show("Продукт успешно удален");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Выбранный продукт состоит в заказе. \nВы не сможете его удалить");
                }
                               
            }
            else
            {
                MessageBox.Show("Выберите продукт для его удаления");
            }
        }
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var openAddProduct = new AddProductView(_currentUser);
            this.Close();
            openAddProduct.Show();
        }
        private void radioCheck(object sender, RoutedEventArgs e)
        {

        }

    }
}
