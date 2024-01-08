using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using PizzaAppDBAccessLib.DataServices;

namespace PizzaAppDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceProvider? _serviceProvider;

        public static IServiceProvider? ServiceProvider()
        {
            return _serviceProvider;
        } 

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }



        private static void ConfigureServices(IServiceCollection services)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            services.AddSingleton(configuration);
            services.AddTransient<MainWindow>();
            services.AddTransient<DeliveryDone>();

            services.AddTransient<ISQLDataBase, SQLDataBase>();
            services.AddTransient<ISqliteDataBase, SqliteDataBase>();
            services.AddTransient<IDataService, SqlDataService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _serviceProvider?.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
