using System.Text.Json.Serialization;

namespace PhoneBook_v2.GUI_MVVM.Models;

public class User
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    [JsonIgnore]
    public string FullName => $"{LastName} {FirstName}";
    
    public List<string> Phones { get; set; }
    
    public Uri PhotoUri { get; set; }
}