using System.IO;
using System.Windows;
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
                        break;
                }
            }
        }
        private void LoadSuppliers()
        {
            var suppliers = _context.Suppliers.ToList();
            suppliers.Insert(0, new Supplier { Id = 0, Name = "Все поставщики" });

            CbSupplier.ItemsSource = suppliers;
            CbSupplier.SelectedIndex = 0; 
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
            var allProducts = _context.Products
                .Include(p => p.FkManufacturerNavigation)
                .Include(p => p.FkProductCategoryNavigation)
                .Include(p => p.FkSupplierNavigation)
                .Include(p => p.FkUnitNavigation)
                .Include(p => p.FkProductNameNavigation)
                .ToList();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string photosFolder = "Photos";
            foreach (var item in allProducts)
            {
                string fullPath = null;
                if (!string.IsNullOrWhiteSpace(item.Photo))
                {
                    fullPath = Path.Combine(basePath, photosFolder, item.Photo);
                }
                if (string.IsNullOrWhiteSpace(fullPath))
                {
                    item.Photo = Path.Combine(basePath, photosFolder, "picture.png");
                }
                else
                {
                    item.Photo = fullPath;
                }
                ProductItemController productController = new ProductItemController();
                productController.DataContext = item;
                BoxProduct.Items.Add(productController);
            }
        }

        private void btnOpenOrder_Click(object sender, RoutedEventArgs e)
        {
            var orderView = _service.GetRequiredService<OrderView>();
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
    }
}