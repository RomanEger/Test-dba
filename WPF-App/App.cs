using System.Windows;

namespace wpf_app;

public class App : Application
{
    readonly MainWindow _mainWindow;
 
    // через систему внедрения зависимостей получаем объект главного окна
    public App(MainWindow mainWindow)
    {
        this._mainWindow = mainWindow;
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        _mainWindow.Show();  // отображаем главное окно на экране
        base.OnStartup(e);
    }
}