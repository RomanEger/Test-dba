using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using wpf_app.Contracts;
using wpf_app.Service;
using wpf_app.ViewModels;

namespace wpf_app;
 
public class Program
{
    [STAThread]
    public static void Main()
    {
        
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<App>();
                services.AddSingleton<MainWindow>();
                services.AddScoped<IRepository, Repository>();
                services.AddSingleton<IConfigurationBuilder, ConfigurationBuilder>();
                services.AddScoped<AbonentNotifyPropertyChanged>();
            })
            .Build();
        
        var app = host.Services.GetRequiredService<App>();
        
        app.Run();
    }
}