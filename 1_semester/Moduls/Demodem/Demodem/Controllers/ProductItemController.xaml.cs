using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Demodem.Models;

namespace Demodem.Controllers
{
    /// <summary>
    /// Логика взаимодействия для ProductItemController.xaml
    /// </summary>
    public partial class ProductItemController : UserControl
    {
        string _projPath = AppDomain.CurrentDomain.BaseDirectory;
        public ProductItemController(Product product)
        {
            InitializeComponent();
            DataContext = product;

            if(product.Discount > 15 )
            {
                stDisc.Background = new BrushConverter().ConvertFrom("#2E8B57") as SolidColorBrush ;
            }

            if (product.Discount > 0)
            {
                runPrice.TextDecorations = TextDecorations.Strikethrough;
                runPrice.Foreground = Brushes.Red;

                runNewPrice.Text = (product.Price * (1 - product.Discount/100)).ToString();
            }

            if(product.CountOnStorage == 0)
            {
                runCountOnStorage.Foreground = Brushes.Blue;
            }
            try
            {

                string path = null;
                if (!string.IsNullOrWhiteSpace(product.Photo))
                {
                    path = Path.Combine(_projPath, "Images", product.Photo);
                }
                else
                {
                    path = Path.Combine(_projPath, "Images", "Defaults", "picture.png");
                }
                BitmapImage bitmap = new BitmapImage(new Uri(path));
                imgProduct.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка с фото");
            }

            
        }
    }
}
