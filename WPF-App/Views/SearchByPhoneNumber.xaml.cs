using System.Windows;
using System.Windows.Input;
using wpf_app.ViewModels;

namespace wpf_app.Views;

public partial class SearchByPhoneNumber : Window
{
    public SearchByPhoneNumber(MainViewModel viewModel)
    {
        InitializeComponent();
        TextTitle.Text = Title;
        DataContext = viewModel;
    }

    private void SearchByPhoneNumber_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
    
    private void Close(object sender, RoutedEventArgs e) => Close();
    
    private void Minimize(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
}