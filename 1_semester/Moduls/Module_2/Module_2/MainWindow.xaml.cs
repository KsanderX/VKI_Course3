using System.Windows;
using System.Windows.Controls;
using Module_2.Controllers;
using Module_2.Moduls;

namespace Module_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawProduct(AppContext.Products);
        }

        public void DrawProduct(List<Product> products)
        {
            BoxProduct.ItemsSource = AppContext.Products.Select(x => new ProductItemController(x));
           
            //Другое решение
            //BoxProduct.Items.Clear();
            //foreach (var element in AppContext.Products)
            //{
            //    ProductItemController controller = new ProductItemController(element);
            //    BoxProduct.Items.Add(controller);
            //}
        }
        private void BoxProduct_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ListBox list = sender as ListBox;

            ProductItemController item = list.SelectedItem as ProductItemController;
            if (item != null)
            {
                Product product = item.DataContext as Product;

                product.Discount += 1;

                DrawProduct(AppContext.Products);
            }
        }
    }
}