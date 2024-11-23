using System.Collections.ObjectModel;
using PhoneBook_v2.GUI_MVVM.Models;

namespace PhoneBook_v2.GUI_MVVM.WindowModels;

public class MainWindowModel : BaseNotify
{
    public ObservableCollection<User> Users { get; set; } = [];
    
    private User? _selectedUser;
    public User? SelectedUser
    {
        get => _selectedUser;
        set
        {
            var result = SetField(ref _selectedUser, value);

            if (!result) return;
            
            Id = SelectedUser!.Id;
            FirstName = SelectedUser!.FirstName;
            LastName = SelectedUser!.LastName;
            Phones = SelectedUser!.Phones;
        }
    }

    private Guid? _id;
    public Guid? Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }
    
    private string? _firstName;
    public string? FirstName
    {
        get => _firstName;
        set => SetField(ref _firstName, value);
    }
    
    private string? _lastName;
    public string? LastName
    {
        get => _lastName;
        set => SetField(ref _lastName, value);
    }
    
    private List<string> _phones;
    public List<string> Phones
    {
        get => _phones;
        set => SetField(ref _phones, value);
    }
    
    private string? _statusBar;
    public string? StatusBar
    {
        get => _statusBar;
        set => SetField(ref _statusBar, value);
    }
    
    private string? _searchText;
    public string? SearchText
    {
        get => _searchText;
        set => SetField(ref _searchText, value);
    }
    
    public LambdaCommand CommandClear { get; }
    public LambdaCommand CommandSearch { get; }

    public MainWindowModel()
    {
        Users.Add(new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Phones = ["+1234567890", "+1234567891"]
        });
        
        CommandClear = new LambdaCommand(
            execute: _ =>
        {
            Id = null;
            FirstName = null;
            LastName = null;
            Phones = [];
        },
            canExecute: _ => true);

        CommandSearch = new LambdaCommand(_ =>
        {
            StatusBar = $"Searching...{SearchText}";
        });
    }
}