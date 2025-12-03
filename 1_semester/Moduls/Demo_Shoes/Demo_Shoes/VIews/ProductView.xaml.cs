using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Demo_Shoes.Controllers;
using Demo_Shoes.Models;
using Demo_Shoes.VIews;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo_Shoes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        private readonly IServiceProvider _service;
        private readonly AganichevShoesContext _context;
        private User _currentUser;
        private List<Product> _cachedProducts = new List<Product>();
        private string SortParam = "По возрастанию";
        public ProductView(AganichevShoesContext context, IServiceProvider service)
        {
            InitializeComponent();
            _service = service;
            _context = context;
            LoadSuppliers();
            GetAllProducts();

           
        }
        public void SetCurrentUser(User user)
        {
            _currentUser = user;
            BoxProduct.MouseDoubleClick -= BoxProduct_MouseDoubleClick;
            if (user == null)
            {
                runUserName.Text = "Гость";
            }
            else
            {
                runUserName.Text = _currentUser.Fio;

                switch (_currentUser.FkUserRoleNavigation.Name)
                {
                    case "Администратор":
                        panelCRUD.Visibility = Visibility.Visible;
                        panelFind.Visibility = Visibility.Visible;
                        panelOrder.Visibility = Visibility.Visible; 
                        BoxProduct.MouseDoubleClick += BoxProduct_MouseDoubleClick;
                        break;

                    case "Менеджер":
                        panelOrder.Visibility = Visibility.Visible;
                        panelFind.Visibility = Visibility.Visible;
                        break;
                }
            }
        }
        private void BoxProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
            var selectedController = BoxProduct.SelectedItem as ProductItemController;

            if (selectedController != null && selectedController.DataContext is Product selectedProduct)
            {
               
                var editView = _service.GetRequiredService<EditProductView>();

                
                editView.InitializeData(selectedProduct);

                
                if (editView.ShowDialog() == true)
                {
                    GetAllProducts(); 
                    MessageBox.Show("Товар успешно изменен!");
                }
            }
        }
        public void GetAllProducts()
        {
            _cachedProducts = _context.Products
                .Include(p => p.FkManufacturerNavigation)
                .Include(p => p.FkProductCategoryNavigation)
                .Include(p => p.FkSupplierNavigation)
                .Include(p => p.FkUnitNavigation)
                .Include(p => p.FkProductNameNavigation)
                .ToList();
            Sort();
        }
        private void btnOpenOrder_Click(object sender, RoutedEventArgs e)
        {
            var orderView = _service.GetRequiredService<OrderView>();
            orderView.SetCurrentUser(_currentUser);
            this.Close();
            orderView.Show();
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedController = BoxProduct.SelectedItem as ProductItemController;

            if (selectedController == null)
            {
                MessageBox.Show("Пожалуйста, выберите товар для удаления из списка.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedController.DataContext is Product productToDelete)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить товар: {productToDelete.FkProductNameNavigation?.Name}?",
                                             "Подтверждение удаления",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Products.Remove(productToDelete);
                        _context.SaveChanges();

                        MessageBox.Show("Товар успешно удален!");

                        GetAllProducts();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении. Возможно, товар используется в заказах.\nПодробности: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addProductView = _service.GetRequiredService<AddProductView>();
            if (addProductView.ShowDialog() == true)
            {
                GetAllProducts();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var authView = _service.GetRequiredService<AuthorizationView>();
            MessageBox.Show("Вы вышли из аккаунта");
            this.Close();
            authView.Show();
        }
       
        private void LoadSuppliers()
        {
            var suppliers = _context.Suppliers.ToList();
            suppliers.Insert(0, new Supplier { Id = 0, Name = "Все поставщики" });

            CbSupplier.ItemsSource = suppliers;
            CbSupplier.SelectedIndex = 0; 
        }
        public void Sort()
        {
            var products = _cachedProducts;
            string searchText = BoxFind.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                products = products.Where(q =>
                    (q.Description != null && q.Description.ToLower().Contains(searchText)) ||
                    (q.Article != null && q.Article.ToLower().Contains(searchText)) ||
                    (q.FkProductNameNavigation != null && 
                    q.FkProductNameNavigation.Name.ToLower().Contains(searchText)) ||
                    q.FkProductCategoryNavigation.Name.ToLower().Contains(searchText)
                ).ToList();
            }
            if (CbSupplier.SelectedItem is Supplier selectedSupplier)
            {
                if(selectedSupplier.Name != "Все поставщики")
                { 
                    products = products.Where(q => q.FkSupplier == selectedSupplier.Id).ToList();
                }
            }
            if(SortParam == "По возрастанию")
            {
                products = products.OrderBy(q => q.CountOnStorage).ToList();
            }
            else if(SortParam == "По убыванию")
            {
                products = products.OrderByDescending(q => q.CountOnStorage).ToList();
            }
            DrawProduct(products);
        }
        private void DrawProduct(List<Product> productsToDraw)
        {
            BoxProduct.Items.Clear();

            foreach (var item in productsToDraw)
            {
                ProductItemController productController = new ProductItemController();
                productController.DataContext = item;

                BoxProduct.Items.Add(productController);
            }
        }
        private void CbSupplier_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Sort();
        }

        private void BoxFind_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Sort();
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radio)
            {
                SortParam = radio.Content.ToString();
                Sort();
            }
        }
    }
}