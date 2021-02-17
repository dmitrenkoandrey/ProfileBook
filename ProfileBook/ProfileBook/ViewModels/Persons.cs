using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ProfileBook.ViewModels;
using ProfileBook.Models;

namespace ProfileBook.TreeView
{
    public class Persons : INotifyPropertyChanged, IPersons, ITreeItem
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Persons()
        {
            Id = Guid.NewGuid();
        }


        private Guid id;
        public Guid Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        private string email;
        public string EMail
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged("EMail");
                }
            }
        }

        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                if (dateOfBirth != value)
                {
                    dateOfBirth = value;
                    OnPropertyChanged("DateOfBirth");
                }
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                if (phone != value)
                {
                    phone = value;
                    OnPropertyChanged("Phone");
                }
            }
        }

        private double performance;
        public double Performance
        {
            get { return performance; }
            set
            {
                if (performance != value)
                {
                    performance = value;
                    OnPropertyChanged("Performance");
                }
            }
        }

        public bool IsPhoneExist { get { return !string.IsNullOrEmpty(phone); } }

        
        public string Name
        {
            get { return string.Format("{0} {1}", firstName, lastName); }
            set { }
        }

        public void SetPerson(Persons person)
        {
            Id = person.Id;
            UserName = person.UserName;
            FirstName = person.FirstName;
            LastName = person.LastName;
            EMail = person.EMail;
            DateOfBirth = person.DateOfBirth;
            Phone = person.Phone;
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public override string ToString()
        {
            return string.Format("Login: {0}\nName: {1}\ne-mail: {2}\nDate of birth: {3}", UserName, FirstName + LastName, EMail, DateOfBirth.ToString("dd-MM-yyyy"));
        }
    }
}

