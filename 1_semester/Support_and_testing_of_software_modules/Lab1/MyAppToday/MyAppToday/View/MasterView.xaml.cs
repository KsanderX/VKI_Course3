using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MyAppToday.Service;

namespace MyAppToday.View
{
    /// <summary>
    /// Логика взаимодействия для MasterView.xaml
    /// </summary>
    public partial class MasterView : Window
    {
        private IServiceProvider _serviceProvider;
        private IAuthService _authService;
        public MasterView(IAuthService authService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _authService = authService;
            _serviceProvider = serviceProvider;
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
