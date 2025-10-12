using Microsoft.Extensions.DependencyInjection;
using MyAppToday.Model;
using MyAppToday.Service;
using MyAppToday.ViewModel;
using System.Windows;

namespace MyAppToday.View
{
    /// <summary>
    /// Логика взаимодействия для OperatorView.xaml
    /// </summary>
    public partial class OperatorView : Window
    {
        private IServiceProvider _serviceProvider;
        private IRequestService _requestService;
        private IAuthService _authService;
        public OperatorView(IServiceProvider serviceProvider, IRequestService requestService, IAuthService authService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _requestService = requestService;
            _authService = authService;
            this.DataContext = _serviceProvider.GetRequiredService<OperatorViewModel>();
        }

        private void btnCreateRequest(object sender, RoutedEventArgs e)
        {
            var operatorViewModel = this.DataContext as OperatorViewModel;

            if (operatorViewModel.SelectedEquipment == null || operatorViewModel.SelectedClient == null)
            {
                MessageBox.Show("Заполните значения из выпадающего(их) списка(ов)");
            }
            else
            {
                if (!string.IsNullOrEmpty(operatorViewModel.ProblemDescription))
                {
                    Request request = new Request()
                    {
                        ProblemDescription = operatorViewModel.ProblemDescription,
                        EquipmentId = operatorViewModel.SelectedEquipment.Id,
                        ClientId = operatorViewModel.SelectedClient.Id
                    };

                    _requestService.CreateRequest(request);
                    MessageBox.Show("Заявка успешно создана!");
                }
                else
                {
                    MessageBox.Show("Описание проблемы нельзя оставить пустым!");
                }
            }
        }

        private void btnOpenRequestView(object sender, RoutedEventArgs e)
        {
            var requestView = _serviceProvider.GetRequiredService<RequestView>();
            this.Close();
            requestView.ShowDialog();
        }

        private void btnOpenAssignMastersView(object sender, RoutedEventArgs e)
        {
            var assignMastersView = _serviceProvider.GetRequiredService<AssignMastersView>();
            this.Close();
            assignMastersView.ShowDialog();
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
