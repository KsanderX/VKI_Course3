using Microsoft.Extensions.DependencyInjection;
using MyAppToday.Model;
using MyAppToday.Service;
using System.Windows;

namespace MyAppToday.View
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationView.xaml
    /// </summary>
    public partial class AuthorizationView : Window
    {
        private IAuthService _authService;
        private IServiceProvider _serviceProvider;
        public AuthorizationView(IAuthService authService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _authService = authService;
            _serviceProvider = serviceProvider;
        }

        private void btnAuthorization(object sender, RoutedEventArgs e)
        {
           var findUser = _authService.Login(tbLogin.Text, tbPassword.Text);

           if (findUser == null)
           {
              MessageBox.Show("Никто не нашелся");
           }
           else
           {
                if (findUser is Client client)
                {
                    var clientView = _serviceProvider.GetRequiredService<ClientView>();
                    this.Close();
                    clientView.ShowDialog();
                }
                else if (findUser is Operator oper)
                {
                    var operatorView = _serviceProvider.GetRequiredService<OperatorView>();
                    this.Close();
                    operatorView.ShowDialog();
                }
                else if (findUser is Master master)
                {
                    var masterView = _serviceProvider.GetRequiredService<MasterView>();
                    this.Close();
                    masterView.ShowDialog();
                }
            }
            
        }
    }
}
