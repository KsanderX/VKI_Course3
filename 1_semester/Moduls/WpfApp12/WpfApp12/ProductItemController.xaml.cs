using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
    /// Логика взаимодействия для ProductItemController.xaml
    /// </summary>
    public partial class ProductItemController : UserControl
    {
        private string _projPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public ProductItemController(Product product)
        {
            InitializeComponent();
            this.DataContext = product;

            string path;

            if (product.Photo != null)
            {
                path = System.IO.Path.Combine(_projPath, "Images", product.Photo);
            }
            else
            {
                path = System.IO.Path.Combine(_projPath, "Images", "Defaults", "picture.png");
            }

            BitmapImage bitmap = new BitmapImage(new Uri(path));

            boxImage.Source = bitmap;

            if (product.Discount > 15)
            {
                gridDiscount.Background = new BrushConverter().ConvertFrom("#2E8B57") as SolidColorBrush;
            }

            if (product.Discount > 0)
            {
                RunPrice.TextDecorations = TextDecorations.Strikethrough;
                RunPrice.Foreground = Brushes.Red;
                RunNewPrice.Text = (product.Price * (1 - product.Discount / 100.0)).ToString();
            }

            if (product.Quantity == 0)
            {
                tbQuantity.Foreground = Brushes.Blue;
            }
        }
    }
}
