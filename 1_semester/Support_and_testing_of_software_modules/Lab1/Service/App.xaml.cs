using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Service
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var service = new ServiceCollection();

            service.AddTransient<MainWindow>();

            var serviceProvider = service.BuildServiceProvider();
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.ShowDialog();
        }
    }

}
