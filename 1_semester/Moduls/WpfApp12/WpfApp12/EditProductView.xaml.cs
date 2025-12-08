using System.Windows;
using WpfApp12.Models;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для EditProductView.xaml
    /// </summary>
    public partial class EditProductView : Window
    {
        private KirillDbContext _context;
        public EditProductView(KirillDbContext myContext)
        {
            InitializeComponent();
            _context = myContext;
            cbCategory.ItemsSource = _context.ProductCategories.ToList();
            cbProductNames.ItemsSource = _context.ProductNames.ToList();
            cbProviders.ItemsSource = _context.Providers.ToList();
            cbSuppliers.ItemsSource = _context.Suppliers.ToList();
            cbUnits.ItemsSource = _context.Units.ToList();
        }

        private void btnLoadPhoto(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            var myViewModel = this.DataContext as EditProductViewModel;
            
           // myViewModel.SelectedProduct.Price
            _context.SaveChanges();
        }
    }
}
