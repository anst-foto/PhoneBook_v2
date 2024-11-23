using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneBook_v2.Components;

public partial class InputWithButton : UserControl
{
    public static readonly DependencyProperty LabelContentProperty;
    public static readonly DependencyProperty ButtonContentProperty;
    public static readonly DependencyProperty InputTextProperty;
    public static readonly DependencyProperty IsReadOnlyProperty;
    public static readonly DependencyProperty ButtonCommandProperty;

    static InputWithButton()
    {
        LabelContentProperty = DependencyProperty.Register(
            name: nameof(LabelContent),
            propertyType: typeof(string),
            ownerType:typeof(InputWithButton));
        ButtonContentProperty = DependencyProperty.Register(
            name: nameof(ButtonContent),
            propertyType: typeof(string),
            ownerType:typeof(InputWithButton));
        InputTextProperty = DependencyProperty.Register(
            name: nameof(InputText),
            propertyType: typeof(string),
            ownerType:typeof(InputWithButton));
        IsReadOnlyProperty = DependencyProperty.Register(
            name: nameof(IsReadOnly),
            propertyType: typeof(bool),
            ownerType:typeof(InputWithButton));
        ButtonCommandProperty = DependencyProperty.Register(
            name: nameof(ButtonCommand),
            propertyType: typeof(ICommand),
            ownerType:typeof(InputWithButton));
    }

    public string LabelContent
    {
        get => (string)GetValue(LabelContentProperty);
        set => SetValue(LabelContentProperty, value);
    }
    
    public string InputText 
    {
        get => (string)GetValue(InputTextProperty);
        set => SetValue(InputTextProperty, value);
    }
    
    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }
    
    public string ButtonContent
    {
        get => (string)GetValue(ButtonContentProperty);
        set => SetValue(ButtonContentProperty, value);
    }
    
    public ICommand ButtonCommand
    {
        get => (ICommand)GetValue(ButtonCommandProperty);
        set => SetValue(ButtonCommandProperty, value);
    }
    
    public InputWithButton()
    {
        InitializeComponent();
    }
}