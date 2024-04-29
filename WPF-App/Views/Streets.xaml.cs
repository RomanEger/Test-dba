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
        TextTitle.Text = Title;
        MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        DataContext = new StreetViewModel(repository);
    }

    private void Streets_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
    
    private void Close(object sender, RoutedEventArgs e) => Close();
    
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
}