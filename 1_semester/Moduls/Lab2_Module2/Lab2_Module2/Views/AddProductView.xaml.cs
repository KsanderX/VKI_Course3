using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Lab2_Module2.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace Lab2_Module2.Views
{
	/// <summary>
	/// Логика взаимодействия для AddProductView.xaml
	/// </summary>
	public partial class AddProductView : Window
	{
		private AganichevMusicEquipmentRentalContext _context;
		private IServiceProvider _serviceProvider;
		private string _selectedPhotoFullPath = null;
		public AddProductView(IServiceProvider serviceProvider, AganichevMusicEquipmentRentalContext context)
		{
			InitializeComponent();
			_serviceProvider = serviceProvider;
			_context = context;
			LoadComboBoxes();

		}

		public void LoadComboBoxes()
		{
			cbEquipmentType.ItemsSource = _context.EquipmentTypes.ToList();
			cbManufacturer.ItemsSource = _context.Manufacturers.ToList();
			cbSupplier.ItemsSource = _context.Suppliers.ToList();
		}

		private void btSelectPhoto_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
				Title = "Выберите фотографию продукта"
			};

			if (openFileDialog.ShowDialog() == true)
			{
				_selectedPhotoFullPath = openFileDialog.FileName;

				ImgPhotoPreview.Source = new BitmapImage(new Uri(_selectedPhotoFullPath));
			}
		}


		private void btSave_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(tbArticle.Text) || string.IsNullOrWhiteSpace(tbName.Text) ||
			string.IsNullOrWhiteSpace(tbPrice.Text) || string.IsNullOrWhiteSpace(tbDiscount.Text) ||
			string.IsNullOrWhiteSpace(tbNumberUnits.Text) || string.IsNullOrWhiteSpace(tbRentalUnit.Text) ||
			string.IsNullOrWhiteSpace(tbDescription.Text) || cbEquipmentType.SelectedItem == null ||
			cbManufacturer.SelectedItem == null || cbSupplier.SelectedItem == null)
			{
				MessageBox.Show("Пожалуйста, заполните все обязательные поля.",
					"Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			double price, discount, numberUnits;
			if (!double.TryParse(tbPrice.Text, out price)) { MessageBox.Show("Цена должна быть числом."); return; }
			if (!double.TryParse(tbDiscount.Text, out discount)) { MessageBox.Show("Скидка должна быть числом."); return; }
			if (!double.TryParse(tbNumberUnits.Text, out numberUnits)) { MessageBox.Show("Кол-во должно быть числом."); return; }

			try
			{		
				string photoFileName = null; 
				
				if (!string.IsNullOrWhiteSpace(_selectedPhotoFullPath))
				{
					string photosDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photos");

					if (!Directory.Exists(photosDirectory))
					{
						Directory.CreateDirectory(photosDirectory);
					}

					string originalFileName = Path.GetFileName(_selectedPhotoFullPath);

					string destinationPath = Path.Combine(photosDirectory, originalFileName);

					File.Copy(_selectedPhotoFullPath, destinationPath, true);

				photoFileName = originalFileName;
				}

				Product newProduct = new Product
				{
					Id = (_context.Products.Any() ? _context.Products.Max(p => p.Id) : 0) + 1,
					Article = tbArticle.Text,
					EquipmentName = tbName.Text,
					Price = price,
					Discount = discount,
					NumberUnits = numberUnits,
					RentalUnit = tbRentalUnit.Text,
					Description = tbDescription.Text,
					FkEquipmentType = (int)cbEquipmentType.SelectedValue,
					FkManufacturer = (int)cbManufacturer.SelectedValue,
					FkSupplier = (int)cbSupplier.SelectedValue,
					Photo = photoFileName 
				};

				_context.Products.Add(newProduct);
				_context.SaveChanges();
				MessageBox.Show("Новый товар успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при сохранении продукта: " + ex.Message,
					"Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btCancel_Click(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Вы уверены, что хотите выйти? \nВсе несохраненные данные будут утеряны!", "Отмена", MessageBoxButton.OKCancel, MessageBoxImage.Question);
			if (result == MessageBoxResult.Cancel) 
			{
				return;
			}
			var productsView = _serviceProvider.GetRequiredService<MainWindow>();
			this.Close();
			productsView.Show();
		}

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти? \nВсе несохраненные данные будут утеряны!", "Отмена", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel)
            {
                return;
            }
            var productView = _serviceProvider.GetRequiredService<MainWindow>();
			this.Close();
			productView.Show();
        }
    }
}