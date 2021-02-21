using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using ProfileBook.Views;
using ProfileBook.TreeView;

namespace ProfileBook.ViewModels
{
    public class PersonsListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PersonViewModel> Persons { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreatePersonCommand { protected set; get; }
        public ICommand DeletePersonCommand { protected set; get; }
        public ICommand SavePersonCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        PersonViewModel selectedPerson;

        public INavigation Navigation { get; set; }

        public PersonsListViewModel()
        {
            Persons = new ObservableCollection<PersonViewModel>();
            CreatePersonCommand = new Command(CreatePerson);
            DeletePersonCommand = new Command(DeletePerson);
            SavePersonCommand = new Command(SavePerson);
            BackCommand = new Command(Back);
        }

        public PersonViewModel SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                if (selectedPerson != value)
                {
                    PersonViewModel tempPerson = value;
                    selectedPerson = null;
                    OnPropertyChanged("SelectedPerson");
                    Navigation.PushAsync(new AddEditProfileView(tempPerson));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void CreatePerson()
        {
            Navigation.PushAsync(new AddEditProfileView(new PersonViewModel() { ListViewModel = this }));
        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        private void SavePerson(object personObject)
        {
            PersonViewModel person = personObject as PersonViewModel;
            //if (person != null && person.IsValid && !Persons.Contains(person))//проверка валидации данных
            {
                Persons.Add(person);
            }
            Back();
        }
        private void DeletePerson(object personObject)
        {
            PersonViewModel person = personObject as PersonViewModel;
            if (person != null)
            {
                Persons.Remove(person);
            }
            Back();
        }
    }
}