using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProfileBook.ViewModels
{
   public class UserLogin : INotifyPropertyChanged, IUserLogin
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public UserLogin()
        {
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

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if(password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
                
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
