using System.Windows;
using wpf_app.Views;

namespace wpf_app;

public class App(Window mainWindow) : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        mainWindow.Show(); 
        base.OnStartup(e);
    }
}