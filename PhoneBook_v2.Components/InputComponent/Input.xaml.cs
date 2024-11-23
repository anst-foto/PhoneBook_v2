using System.Windows;
using System.Windows.Controls;

namespace PhoneBook_v2.Components;

public partial class Input : UserControl
{
    public static readonly DependencyProperty LabelContentProperty;
    public static readonly DependencyProperty InputTextProperty;
    public static readonly DependencyProperty IsReadOnlyProperty;

    static Input()
    {
        LabelContentProperty = DependencyProperty.Register(
            name: nameof(LabelContent),
            propertyType: typeof(string),
            ownerType:typeof(Input));
        InputTextProperty = DependencyProperty.Register(
            name: nameof(InputText),
            propertyType: typeof(string),
            ownerType:typeof(Input));
        IsReadOnlyProperty = DependencyProperty.Register(
            name: nameof(IsReadOnly),
            propertyType: typeof(bool),
            ownerType:typeof(Input));
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
    
    public Input()
    {
        InitializeComponent();
    }
}