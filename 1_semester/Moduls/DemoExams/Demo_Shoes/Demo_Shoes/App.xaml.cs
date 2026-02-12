using System.Windows;
using Demo_Shoes.Models;
using Demo_Shoes.VIews;
using Microsoft.Extensions.DependencyInjection;

namespace Demo_Shoes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _service;

        public App()
        {
            ServiceCollection services = new();
            services.AddDbContext<AganichevShoesContext>();

            services.AddTransient<AuthorizationView>();

            services.AddTransient<ProductView>();
            services.AddTransient<AddProductView>();
            services.AddTransient<EditProductView>()
                ;
            services.AddTransient<OrderView>();
            services.AddTransient<EditOrderView>();
            services.AddTransient<AddOrderView>();

            _service = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _service.GetRequiredService<AuthorizationView>();
            mainWindow.Show();
        }
    }
}   


