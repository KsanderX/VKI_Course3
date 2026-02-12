using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Demodemo.Controllers;
using Demodemo.Models;
using Demodemo.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Demodemo.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        private AganichevShoesContext _context;
        private List<Product> _products;
        private User _currentUser;
        private string _sortParam = "По возрастанию";
        private string FiltParam = "Все поставщики";
        public ProductView(User user = null)
        {
            InitializeComponent();
            _context = new AganichevShoesContext();
            _currentUser = user;

            LoadProducts();

            CurrentUser(_currentUser);
            
        }

         public void LoadProducts()
        {
            _products = _context.Products
                .Include(p => p.FkUnitNavigation)
                .Include(p => p.FkProductCategoryNavigation)
                .Include(p => p.FkProductNameNavigation)
                .Include(p => p.FkSupplierNavigation)
                .Include(p => p.FkManufacturerNavigation).ToList();

            foreach (var product in _products)
            {
                ProductItemController control = new ProductItemController(product);
                listBoxProducts.Items.Add(control);
            }
        }
        public void CurrentUser(User user)
        {
            _currentUser = user;
            {
                if (user == null)
                {
                    runFIO.Text = "Гость";
                    spCRUID.Visibility = Visibility.Collapsed;
                    spFind.Visibility = Visibility.Collapsed;
                }
                else
                {
                    runFIO.Text = user.Fio;
                    if(user.FkUserRole == 1)
                    {
                        listBoxProducts.MouseDoubleClick += listBoxProduct_MouseDoubleClick;
                    }
                    else if (user.FkUserRole == 2)
                    {
                        spCRUID.Visibility = Visibility.Collapsed;
                    }
                    else if(user.FkUserRole == 3)
                    {
                        spCRUID.Visibility = Visibility.Collapsed;
                        spFind.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MessageBox.Show("Неизветсная роль");
                    }
                }
                
            }
        }
        private void listBoxProduct_MouseDoubleClick(object sender, MouseButtonEventArgs a)
        {
            var selectedProduct = listBoxProducts.SelectedItem as ProductItemController;

            if(selectedProduct != null)
            {
                var editProductView = new ProductEditView(_context, _currentUser);

                var viewModel = new EditProductViewModel();

                viewModel.SelectedProduct = selectedProduct.DataContext as Product;
                
                editProductView.DataContext = viewModel;
                this.Close();
                editProductView.Show();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var authView = new AuthView();
            this.Close();
            authView.Show();
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedProducts = listBoxProducts.SelectedItem as ProductItemController;
            if(selectedProducts == null)
            {
                MessageBox.Show("Выберите товар для удаления");
                return;
            }
            if (selectedProducts.DataContext is Product product)
            {
                try
                {
               
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    MessageBox.Show("Товар удален");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении.Возможно товар присутствует в заказе.", ex.Message);
                }
            }   
        }


        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addProductView = new AddProductView(_currentUser);
            this.Close();
            addProductView.Show();
        }

        private void tbFind_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //Sort();
        }
        private void CbSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            var item = (Supplier)comboBox.SelectedItem;

            if (comboBox.Items.Count != null)
            {
                FiltParam = item.ToString();
            }
            Sort();
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            if (radio.Content.ToString() == "По возрастанию")
            {
                _sortParam = "По возрастанию";
            }
            else
            {
                _sortParam = "По убыванию";
            }
            Sort();
        }
        public void Sort()
        {
            _products = _context.Products.Include(p => p.FkUnitNavigation)
                .Include(p => p.FkProductCategoryNavigation)
                .Include(p => p.FkProductNameNavigation)
                .Include(p => p.FkSupplierNavigation)
                .Include(p => p.FkManufacturerNavigation).ToList();

            _products = _context.Products.Where(p => p.Description.Contains(tbFind.Text) ||
            p.FkProductNameNavigation.Name.Contains(tbFind.Text) ||
            p.Article.Contains(tbFind.Text)).ToList()
            .Where(p => p.FkSupplierNavigation.Name == FiltParam || FiltParam == "Все поставщики").ToList();

            if (_sortParam == "По возрастанию")
            {
                _products = _products.OrderBy(q => q.CountOnStorage).ToList();
            }
            else if (_sortParam == "По убыванию")
            {
                _products = _products.OrderByDescending(q => q.CountOnStorage).ToList();
            }
            LoadProducts();
        }
    }
}


//Scaffold-DbContext "Server=server;DataBase=DB;Trust Server Certificate=True;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer OutputDir Models