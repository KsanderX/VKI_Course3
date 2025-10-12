using Microsoft.Extensions.DependencyInjection;
using MyAppToday.Service;
using MyAppToday.View;
using MyAppToday.ViewModel;
using System.Windows;

namespace MyAppToday
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRequestService, RequestService>();

            services.AddTransient<AuthorizationView>();
            services.AddTransient<OperatorView>();
            services.AddTransient<OperatorViewModel>();
            services.AddTransient<ClientView>();
            services.AddTransient<ClientViewModel>();
            services.AddTransient<MasterView>();

            services.AddTransient<RequestView>();
            services.AddTransient<RequestViewModel>();

            services.AddTransient<AssignMastersView>();
            services.AddTransient<AssignMastersViewModel>();


            var serviceProvider = services.BuildServiceProvider();
            var authorizationView = serviceProvider.GetRequiredService<AuthorizationView>();
            authorizationView.ShowDialog();
        }
    }

}
