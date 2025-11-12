using System.IO;
using System.Windows;
using Lab2_Module2.Controllers;
using Lab2_Module2.Models;
using Lab2_Module2.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lab2_Module2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ListProduct : Window
    {
        private IServiceProvider _serviceProvider;
        AganichevMusicEquipmentRentalContext _context = new AganichevMusicEquipmentRentalContext();

        public ListProduct(AganichevMusicEquipmentRentalContext context, IServiceProvider service)
        {
            InitializeComponent();
            _serviceProvider = service;
            _context = context;
            GetAllProducts();
        }

        public void GetAllProducts()
        {
            BoxEquipment.Items.Clear();

            var allProducts = _context.Products
                                .Include(p => p.FkEquipmentTypeNavigation)
                                .Include(p => p.FkManufacturerNavigation)
                                .Include(p => p.FkSupplierNavigation)
                                .ToList();

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string photosFolder = "Photos";

            foreach (var item in allProducts)
            {
                if (string.IsNullOrWhiteSpace(item.Photo))
                {
                    item.Photo = Path.Combine(basePath, photosFolder, "picture.png");
                }
                else
                {
                    item.Photo = Path.Combine(basePath, photosFolder, item.Photo);
                }

                EquipmentItemController equipment = new(item);
                BoxEquipment.Items.Add(equipment);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            EquipmentItemController selectedControl = BoxEquipment.SelectedItem as EquipmentItemController;

            if (selectedControl == null)
            {
                MessageBox.Show("Пожалуйста, выберите товар для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Product productToDelete = selectedControl.DataContext as Product;

            if (productToDelete == null)
            {
                MessageBox.Show("Не удалось получить данные о товаре.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var result = MessageBox.Show($"Вы уверены, что хотите удалить этот товар?\n\n{productToDelete.EquipmentName}",
                                         "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return;
            }
            try
            {
                bool isReferencedInOrders = _context.OrderEquipments.Any(oe => oe.FkEquipment == productToDelete.Id);

                if (isReferencedInOrders)
                {
                    MessageBox.Show("Невозможно удалить товар, так как он связан с существующими заказами.", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _context.Products.Remove(productToDelete);
                _context.SaveChanges();

                MessageBox.Show("Товар успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                GetAllProducts();
            }
            catch (DbUpdateException ex) 
            {
                MessageBox.Show("Ошибка при удалении товара из базы данных.\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденная ошибка:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти из аккаунта?" , "Отмена", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel)
            {
                return;
            }
            var authView = _serviceProvider.GetRequiredService<AutorizationView>();
            this.Close();
            authView.Show();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addProduct = _serviceProvider.GetRequiredService<AddProductView>();
            this.Close();
            addProduct.Show();           
        }
    }    
}