using Microsoft.Extensions.DependencyInjection;
using MyAppToday.Model;
using MyAppToday.Service;
using MyAppToday.ViewModel;
using System.Windows;

namespace MyAppToday.View
{
    /// <summary>
    /// Логика взаимодействия для RequestView.xaml
    /// </summary>
    public partial class RequestView : Window
    {
        private IServiceProvider _serviceProvider;
        private IRequestService _requestService;
        private IAuthService _authService;
        public RequestView(IServiceProvider serviceProvider, IRequestService requestService, IAuthService authService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _requestService = requestService;
            _authService = authService;
            this.DataContext = _serviceProvider.GetRequiredService<RequestViewModel>();
        }

        private void btnCheckAssignMasters(object sender, RoutedEventArgs e)
        {
            var myRequestViewModel = this.DataContext as RequestViewModel;

            if (myRequestViewModel.SelectedRequest != null)
            {
                List<Master> masters = _requestService.GetAssignMasters(myRequestViewModel.SelectedRequest);

                if (masters.Count == 0)
                {
                    MessageBox.Show("Ответственных нет");
                }
                else
                {
                    foreach (Master master in masters)
                    {
                        MessageBox.Show(master.Name);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заявка не выбрана");
            }
        }
        private void btnOpenOperatorView(object sender, RoutedEventArgs e)
        {
            var operatorView = _serviceProvider.GetRequiredService<OperatorView>();
            this.Close();
            operatorView.ShowDialog();
        }
    }
}
