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

namespace _1Laba
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = new List<UserProfile>
            {
                new UserProfile
                {
                    ImagePath = "F:\\ВКИ\\VKI_Course3\\1_semester\\Moduls\\1Laba\\1Laba\\Image\\Человек-паук.jpg",
                    UserName = "Питер Паркер",
                    BloodGroup = "O+",
                    ProfileDescription = "Человек-паук, супергерой с паучьими способностями. Умный, ловкий и ответственный. Любит науку и фотографирование. Защитник города от преступности.",
                    CreationDate = "15.08.2024",
                    ZodiacSign = "Лев"
                },
                new UserProfile
                {
                    ImagePath = "F:\\ВКИ\\VKI_Course3\\1_semester\\Moduls\\1Laba\\1Laba\\Image\\Iron Man.jpg",
                    UserName = "Тони Старк",
                    BloodGroup = "A+",
                    ProfileDescription = "Гениальный изобретатель, миллиардер и супергерой Железный человек. Создатель высокотехнологичных костюмов. Основатель команды Мстителей.",
                    CreationDate = "20.08.2024",
                    ZodiacSign = "Близнецы"
                },
                new UserProfile
                {
                    ImagePath = "F:\\ВКИ\\VKI_Course3\\1_semester\\Moduls\\1Laba\\1Laba\\Image\\Черная пантера.jpg",
                    UserName = "Т'Чалла",
                    BloodGroup = "AB-",
                    ProfileDescription = "Король Ваканды, Черная Пантера. Обладает сверхчеловеческими способностями благодаря сердечной траве. Лидер technologically advanced нации. И бла бла бла бла бла бла бла блабла бла бла блабла бла бла блабла бла бла блабла бла бла бла",
                    CreationDate = "25.08.2024",
                    ZodiacSign = "Скорпион"
                }
            };

            UsersListBox.ItemsSource = users;
        }
    }

    public class UserProfile
    {
        public string ImagePath { get; set; }
        public string UserName { get; set; }
        public string BloodGroup { get; set; }
        public string ProfileDescription { get; set; }
        public string CreationDate { get; set; }
        public string ZodiacSign { get; set; }
    }
}
