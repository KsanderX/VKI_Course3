using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Demo_Shoes.Models;

namespace Demo_Shoes.Controllers
{
    /// <summary>
    /// Логика взаимодействия для ProductItemController.xaml
    /// </summary>
    public partial class ProductItemController : UserControl
    {
        public ProductItemController()
        {
            InitializeComponent();
            //Product product = DataContext as Product;

            //if (product.Discount > 15)
            //{
            //    BorderDiscount.Background = new SolidColorBrush(Color.FromRgb(46, 139, 87));
            //}

            //if (product.Discount > 0)
            //{
            //    RunPrice.Foreground = new SolidColorBrush(Colors.Red);
            //    RunPrice.TextDecorations.Add(TextDecorations.Strikethrough);

            //    RunNewPrice.Text = $"{product.Price - (product.Price * product.Discount) / 100}";

            //    if (product.CountOnStorage == 0)
            //    {
            //        BoxCount.Foreground = new SolidColorBrush(Colors.Blue);
            //    }
            //}

        }
    }
}
