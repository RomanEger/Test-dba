using System.Windows.Input;

namespace wpf_app.ViewModels;

public class RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    : ICommand
{
    private Action<object> _execute = execute;
    private Func<object, bool> _canExecute = canExecute;
 
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter) => this._canExecute == null || this._canExecute(parameter);
    
    public void Execute(object parameter) => this._execute(parameter);
    
}