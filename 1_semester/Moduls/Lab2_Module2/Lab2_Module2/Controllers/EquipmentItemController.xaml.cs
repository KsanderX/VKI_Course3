using System.Windows.Controls;
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


        }
    }
}
