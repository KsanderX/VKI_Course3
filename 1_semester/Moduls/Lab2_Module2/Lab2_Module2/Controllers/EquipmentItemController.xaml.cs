using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Lab2_Module2.Models;
namespace Lab2_Module2.Controllers
{
    /// <summary>
    /// Логика взаимодействия для EquipmentItemController.xaml
    /// </summary>
    public partial class EquipmentItemController : UserControl
    {
        public EquipmentItemController(Product products)
        {
            InitializeComponent();

            DataContext = products;

            if(products.Discount > 15)  
            {
                DiscountTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57"));
                DiscountTextBlock.TextDecorations = TextDecorations.Underline;
            }
        }
    }
}
