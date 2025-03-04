using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System;
using TodoXaml.Models;
using TodoXaml.Converters;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;

namespace TodoXaml.Views;

public sealed partial class MainPage : Page, INotifyPropertyChanged
{
    public List<Priority> Priorities { get; } = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
    public Visibility visibility { get; set; } = Visibility.Collapsed;

    public event PropertyChangedEventHandler PropertyChanged;
    private TodoItem newTodo = null;
    public MainPage()
    {
        InitializeComponent();
    }
    public TodoItem NewTodo
    {
        get
        {
            return newTodo;
        }
        set
        {
            if (value != null) 
                visibility = Visibility.Visible;
            else 
                visibility = Visibility.Collapsed;
            newTodo = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewTodo)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(visibility)));
        }
    }
    public ObservableCollection<TodoItem> Todos { get; set; } = new()
    {
        new TodoItem()
        {
            Id = 3,
            Title = "Add Neptun code to neptun.txt",
            Description = "CQKFI2",
            Priority = Priority.Normal,
            IsDone = false,
            Deadline = new DateTime(2024, 11, 08)
        },
        new TodoItem()
        {
            Id = 1,
            Title = "Buy milk",
            Description = "Should be lactose and gluten free!",
            Priority = Priority.Low,
            IsDone = true,
            Deadline = DateTimeOffset.Now + TimeSpan.FromDays(1)
        },
        new TodoItem()
        {
            Id = 2,
            Title = "Do the Computer Graphics homework",
            Description = "Ray tracing, make it shiny and gleamy! :)",
            Priority = Priority.High,
            IsDone = false,
            Deadline = new DateTime(2024, 11, 08)
        },
    };

    public static Brush GetForeground(Priority priority)
    {
        switch (priority)
        {
            case Priority.High:
                return new SolidColorBrush(Colors.Red);
            case Priority.Normal:
                return (SolidColorBrush)App.Current.Resources["ApplicationForegroundThemeBrush"];
            case Priority.Low:
                return new SolidColorBrush(Colors.Blue);
            default:
                return (SolidColorBrush)App.Current.Resources["ApplicationForegroundThemeBrush"];
        }
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        Todos.Add(NewTodo);
        NewTodo = null;
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        NewTodo = new TodoItem()
        {
            Deadline = new DateTimeOffset(),
            Title = "",
            Description = "",
            Priority = Priority.Low,
            IsDone = false,
            Id = Todos.Count()
        };
    }
}