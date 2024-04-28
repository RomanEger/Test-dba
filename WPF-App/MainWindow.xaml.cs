using System.Windows;
using System.Windows.Input;
using wpf_app.Contracts;
using wpf_app.ViewModels;

namespace wpf_app;

public partial class MainWindow : Window
{
    private bool IsMaximized { get; set; } = false;
    
    public MainWindow(AbonentNotifyPropertyChanged notifyPropertyChanged)
    {
        InitializeComponent();
        DataContext = notifyPropertyChanged;
    }

    private void Exit(object sender, RoutedEventArgs e) => Environment.Exit(0);

    private void ChangeState(object sender, RoutedEventArgs e)
    {
        if (IsMaximized)
        {
            this.WindowState = WindowState.Normal;
            IsMaximized = false;
        }
        else
        {
            this.WindowState = WindowState.Maximized;
            IsMaximized = true;
        }
    }

    private void Minimize(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

    private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
    
}