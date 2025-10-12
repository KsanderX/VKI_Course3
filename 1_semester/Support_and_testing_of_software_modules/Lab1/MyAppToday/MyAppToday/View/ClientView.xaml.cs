using Microsoft.Extensions.DependencyInjection;
using MyAppToday.Service;
using MyAppToday.ViewModel;
using System.Windows;

namespace MyAppToday.View
{
    /// <summary>
    /// Логика взаимодействия для ClientView.xaml
    /// </summary>
    public partial class ClientView : Window
    {
        private IServiceProvider _serviceProvider;
        private IAuthService _authService;
        private IRequestService _requestService;
        public ClientView(IServiceProvider serviceProvider, IAuthService authService, IRequestService requestService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _authService = authService;
            _requestService = requestService;
            this.DataContext = _serviceProvider.GetRequiredService<ClientViewModel>();
        }

        private void btnCreateRequest(object sender, RoutedEventArgs e)
        {
            var myContext = this.DataContext as ClientViewModel;

            if (myContext.SelectedEquipment == null)
            {
                MessageBox.Show("Выберите оборудование!");
            }
            else
            {
                if (!string.IsNullOrEmpty(myContext.ProblemDescription))
                {
                    Model.Request request = new Model.Request()
                    {
                        ClientId = _authService.Client.Id,
                        EquipmentId = myContext.SelectedEquipment.Id,
                        ProblemDescription = myContext.ProblemDescription
                    };

                    _requestService.CreateRequest(request);
                    MessageBox.Show("Заявка успешно создана!");
                }
                else
                {
                    MessageBox.Show("Описание проблемы не может быть пустое");
                }
            }
        }

        private void btnOpenAuthorizationView(object sender, RoutedEventArgs e)
        {
            _authService.Logout();
            var authorizationView = _serviceProvider.GetRequiredService<AuthorizationView>();
            this.Close();
            authorizationView.ShowDialog();
        }
    }
}
