using System.Windows;
using System.Windows.Input;
using wpf_app.ViewModels;

namespace wpf_app.Views;

public partial class MainWindow : Window
{
    private bool IsMaximized { get; set; }
    
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        TextTitle.Text = Title;
        DataContext = viewModel;
    }

    private void Exit(object sender, RoutedEventArgs e) => Environment.Exit(0);

    private void ChangeState(object sender, RoutedEventArgs e)
    {
        if (IsMaximized)
        {
            WindowState = WindowState.Normal;
            IsMaximized = false;
        }
        else
        {
            WindowState = WindowState.Maximized;
            IsMaximized = true;
        }
    }

    private void Minimize(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

    private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
    
}