using System.Windows;
using System.Windows.Input;
using wpf_app.Contracts;
using wpf_app.ViewModels;

namespace wpf_app.Views;

public partial class Streets : Window
{
    private bool IsMaximized { get; set; } = false;
    
    public Streets(IRepository repository)
    {
        InitializeComponent();
        DataContext = new StreetViewModel(repository);
    }

    private void Streets_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
    
    private void Close(object sender, RoutedEventArgs e) => Close();
    
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
}