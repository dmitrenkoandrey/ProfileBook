using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProfileBook.Models;
using ProfileBook.Service;
using ProfileBook.TreeView;
using ProfileBook.ViewModels;
using ProfileBook;

namespace ProfileBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInView: ContentPage
    {
        private UserLogin userLogin;
        public SignInView()
        {
            userLogin = new UserLogin() { UserName = "user" };
            InitializeComponent();
            BindingContext = this;

            entLoginName.SetBinding(Entry.TextProperty, new Binding { Source = userLogin, Path = "UserName" });
            entPassword.SetBinding(Entry.TextProperty, new Binding { Source = userLogin, Path = "Password" });
            if (Device.RuntimePlatform == Device.iOS) Padding = new Thickness(0, 20, 0, 0);
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                IsBusy = true;
                //var aServ = new AccountService(App.EndPoint, "");
                //
              //App.Pwd = await aServ.Login(userLogin);
                IsBusy = false;
                
                Navigation.InsertPageBefore(new MainListView(), this);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                //IsBusy = false;
            }
        }
        private async void btnLogup_Clicked(object sender, EventArgs e)
        {
            try
            {
                IsBusy = true;
                //var aServ = new AccountService(App.EndPoint, "");
                //
                //App.Pwd = await aServ.Login(userLogin);
                IsBusy = false;
                Navigation.InsertPageBefore(new SignUpView(), this);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                //IsBusy = false;
            }
        }
    }
}