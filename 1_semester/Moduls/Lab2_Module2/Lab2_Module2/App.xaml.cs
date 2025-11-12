using System.Windows;
using Lab2_Module2.Controllers;
using Lab2_Module2.Models;
using Lab2_Module2.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Lab2_Module2
{
    public partial class App : Application
    {
        private readonly IServiceProvider _service;

        public App()
        {
            ServiceCollection services = new();

            services.AddTransient<AutorizationView>();
            services.AddTransient<RegistrationView>();

            services.AddTransient<ListProduct>();
            services.AddTransient<AddProductView>();

            services.AddTransient<EquipmentItemController>();

            services.AddDbContext<AganichevMusicEquipmentRentalContext>();

            _service = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var view = _service.GetRequiredService<AutorizationView>();
            view.Show();
        }
    }
}