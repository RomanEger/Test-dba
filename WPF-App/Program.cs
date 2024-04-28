using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using wpf_app.Repository;

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
                services.AddScoped<IRepository>();
            })
            .Build();
        
        var app = host.Services.GetRequiredService<App>();
        
        app.Run();
    }
}