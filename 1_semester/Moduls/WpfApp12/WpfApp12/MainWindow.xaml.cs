using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp12.Models;

namespace WpfApp12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> products;
        private KirillDbContext _context;
        private string SortParam = "по возрастанию";
        private string FiltParam = "все поставщики";
        public MainWindow()
        {
            InitializeComponent();
            _context = new KirillDbContext();

            List<Provider> providers = new List<Provider>()
            {
                new Provider{Id = -1, Name = "все поставщики"}
            };

            providers.AddRange(_context.Providers.ToList());
            cbProvider.ItemsSource = providers;
            DrawProducts(products);
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Content.ToString() == "по возрастанию")
            {
                SortParam = "по возрастанию";
            }
            else if (radioButton.Content.ToString() == "по убыванию")
            {
                SortParam = "по убыванию";
            }
            Sort();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sort();
        }

        private void cbProvider_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            var item = (Provider)comboBox.SelectedItem;

            if (comboBox.SelectedItem != null) 
            {
                FiltParam = item.ToString();
            }
            Sort();
        }

        private void Sort()
        {
            products = _context.Products.Include(p => p.ProductCategory).Include(p => p.ProductName).Include(p => p.Unit).Include(p => p.Supplier).Include(p => p.Provider).ToList();

            products = products.Where(p => p.Description.Contains(tbFind.Text) || p.ProductName.Name.Contains(tbFind.Text) || p.Article.Contains(tbFind.Text))
                 .ToList().Where(p => p.Provider.Name == FiltParam || FiltParam == "все поставщики").ToList();

            if (SortParam == "по возрастанию")
            {
                products = products.OrderBy(q => q.Quantity).ToList();
            }
            else if (SortParam == "по убыванию")
            {
                products = products.OrderByDescending(q => q.Quantity).ToList();
            }

            DrawProducts(products);
        }

        public void DrawProducts(List<Product> products)
        {
            lbProducts.Items.Clear();
            foreach (Product product in products) 
            {
                ProductItemController productItemController = new ProductItemController(product);
                lbProducts.Items.Add(productItemController);
            }
        }

        private void lbProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedController = (ProductItemController)lbProducts.SelectedItem;

            if (selectedController != null) 
            { 
                Product selectedProduct = (Product)selectedController.DataContext;

                EditProductViewModel editProductViewModel = new EditProductViewModel();
                editProductViewModel.SelectedProduct = selectedProduct;

                var editProductView = new EditProductView(_context);
                editProductView.DataContext = editProductViewModel;
                this.Close();
                editProductView.ShowDialog();
            }
            else
            {
                MessageBox.Show("Вы ничего не выбрали");
            }
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {
            var selectedController = (ProductItemController)lbProducts.SelectedItem;

            if (selectedController != null)
            {
                Product selectedProduct = (Product)selectedController.DataContext;
                _context.Products.Remove(selectedProduct);
                _context.SaveChanges();
                MessageBox.Show("Продукт успешно удален");
                Sort();
            }
            else
            {
                MessageBox.Show("Вы ничего не выбрали");
            }
        }

        private void btnOpenAddProductWindow(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductWindow();
            this.Close();
            addProductWindow.ShowDialog();
        }
    }
}