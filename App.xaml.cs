using System.Configuration;
using System.Data;
using System.ServiceProcess;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace GmapDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfirureServices();
            this.InitializeComponent();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private IServiceProvider ConfirureServices()
        {
            var services = new ServiceCollection();
            return services.BuildServiceProvider();
        }
    }
}
