using System.Collections.ObjectModel;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Input;
using Microsoft.Win32;
using PhoneBook_v2.GUI_MVVM.Models;

namespace PhoneBook_v2.GUI_MVVM.WindowModels;

public class MainWindowModel : BaseNotify
{
    #region ObservableProperties

    private string? _filePath;
    public string? FilePath
    {
        get => _filePath;
        set => SetField(ref _filePath, value);
    }
    
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

    #endregion

    #region Commands
    
    public LambdaCommand CommandClear { get; }
    public LambdaCommand CommandSearch { get; }
    
    public LambdaCommandWithGesture CommandOpen { get; }
    public LambdaCommandWithGesture CommandSave { get; }
    public LambdaCommandWithGesture CommandSaveAs { get; }
    
    #endregion

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

        CommandOpen = new LambdaCommandWithGesture(
            execute: _ =>
            {
                var openFileDialog = new OpenFileDialog()
                {
                    Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    FilePath = openFileDialog.FileName;
                }

                OpenFile();
            },
            canExecute: _ => true)
        {
            Name = "Открыть...",
            Gesture = new KeyGesture(Key.F3)
        };
        
        CommandSave = new LambdaCommandWithGesture(
            execute: _ =>
            {
                SaveFile();
            },
            canExecute: _ => true)
        {
            Name = "Сохранить",
            Gesture = new KeyGesture(Key.F2)
        };
        
        CommandSaveAs = new LambdaCommandWithGesture(
            execute: _ =>
            {
                var saveFileDialog = new SaveFileDialog()
                {
                    Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
                };
                if (saveFileDialog.ShowDialog() == true)
                {
                    FilePath = saveFileDialog.FileName;
                }

                SaveFile();
            },
            canExecute: _ => true)
        {
            Name = "Сохранить как...",
            Gesture = new KeyGesture(Key.F2, ModifierKeys.Control)
        };
    }

    private void OpenFile()
    {
        if (FilePath is null) return;
        
        using var file = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
        var users = JsonSerializer.Deserialize<IEnumerable<User>>(file);
        
        Users.Clear();

        foreach (var user in users)
        {
            Users.Add(user);
        }
    }

    private void SaveFile()
    {
        if (FilePath is null) return;

        using var file = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        JsonSerializer.Serialize(file, Users, options);
    }
}