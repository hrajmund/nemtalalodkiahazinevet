using HelloXaml.Views;
using HelloXaml.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Input;

namespace HelloXaml.ViewModels
{
    public partial class PersonListPageViewModel : ObservableObject
    {
        public PersonListPageViewModel()
        {
            IncreaseAgeCommand = new RelayCommand(IncreaseAge, canIncrement);
            NewPerson = new Person()
            {
                Name = "Eric Cartman",
                Age = 8
            };
            
            NewPerson.PropertyChanged += (sender,e) =>
            {
                OnPropertyChanged(nameof(IsDecrementEnabled));
                DecreaseAgeCommand.NotifyCanExecuteChanged();
            };
            NewPerson.PropertyChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(IsIncrementEnabled));
                IncreaseAgeCommand.NotifyCanExecuteChanged();
            }; 
            NewPerson.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(IsNameNotEmpty)); 


            People = new ObservableCollection<Person>()
            {
                new Person() { Name = "Peter Griffin", Age = 40 },
                new Person() { Name = "Homer Simpson", Age = 42 },
            };
        }

        public Person NewPerson { get; set; }
        public ObservableCollection<Person> People { get; set; }
        public void AddPersonToList()
        {
            if (IsNameNotEmpty)
            {
                People.Add(new Person()
                {
                    Name = NewPerson.Name,
                    Age = NewPerson.Age,
                });
            }

            NewPerson.Name = string.Empty;
            NewPerson.Age = 0;
        }

        [RelayCommand(CanExecute = nameof(IsDecrementEnabled))]
        public void DecreaseAge()
        {
            if (IsDecrementEnabled)
            {
                NewPerson.Age--;
            }
        }
        public RelayCommand IncreaseAgeCommand { get; }
        public void IncreaseAge()
        {
            if (IsIncrementEnabled)
            {
                NewPerson.Age++;
            }
            
        }
        public bool IsDecrementEnabled
        {
            get { return NewPerson.Age > 0; }
        }
        public bool IsIncrementEnabled
        {
            get { return NewPerson.Age < 150; }
        }
        public bool IsNameNotEmpty
        {
            get { return !String.IsNullOrWhiteSpace(NewPerson.Name); }
        }
        private bool canIncrement()
        {
            return IsIncrementEnabled;
        }
        private bool canDecrement()
        {
            return IsDecrementEnabled;
        }
    }
}
