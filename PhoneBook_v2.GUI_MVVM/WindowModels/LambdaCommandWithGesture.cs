using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace PhoneBook_v2.GUI_MVVM.WindowModels;

public class LambdaCommandWithGesture : DependencyObject, ICommand
{
    #region Dependencies

    public static readonly DependencyProperty GestureProperty;
    public static readonly DependencyProperty NameProperty;

    static LambdaCommandWithGesture()
    {
        GestureProperty = DependencyProperty.Register(
            name: nameof(Gesture), 
            propertyType: typeof(KeyGesture),
            ownerType: typeof(LambdaCommandWithGesture), 
            typeMetadata: new PropertyMetadata(default(KeyGesture)));

        NameProperty = DependencyProperty.Register(
            name: nameof(Name),
            propertyType: typeof(string),
            ownerType: typeof(LambdaCommandWithGesture));
    }

    #endregion
    
    private readonly Action<object?> _execute;
    private readonly Predicate<object?> _canExecute;

    public KeyGesture Gesture
    {
        get => (KeyGesture)GetValue(GestureProperty);
        init => SetValue(GestureProperty, value);
    }
    public string GestureText => Gesture.GetDisplayStringForCulture(CultureInfo.CurrentUICulture)!;

    public string Name
    {
        get => (string)GetValue(NameProperty);
        init => SetValue(NameProperty, value);
    }

    public LambdaCommandWithGesture(Action<object?> execute, Predicate<object?>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute ?? (_ => true);
    }
    
    public bool CanExecute(object? parameter)
    {
        return _canExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        _execute(parameter);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}