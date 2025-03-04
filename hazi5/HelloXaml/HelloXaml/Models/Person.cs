using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace HelloXaml.Models;

public partial class Person : ObservableObject
{
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private int age;
}
/*public class Person : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string name;
    public string Name
    {
        get { return name; }
        set
        {
            if (name != value)
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
    }

    private int age;
    public int Age
    {
        get { return age; }
        set
        {
            if (age != value)
            {
                age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Age)));
            }
        }
    }
}
*/