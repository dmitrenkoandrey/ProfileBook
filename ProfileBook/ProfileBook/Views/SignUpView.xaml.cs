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
    public partial class SignUpView : ContentPage
    {
        private UserLogin newuserLogin;
        public SignUpView()
        {
            newuserLogin = new UserLogin() { UserName = "" };
            InitializeComponent();
            BindingContext = this;

            entnewLoginName.SetBinding(Entry.TextProperty, new Binding { Source = newuserLogin, Path = "UserName" });
            entnewPassword.SetBinding(Entry.TextProperty, new Binding { Source = newuserLogin, Path = "Password" });
            if (Device.RuntimePlatform == Device.iOS) Padding = new Thickness(0, 20, 0, 0);
        }
        private async void btnLogup_Clicked(object sender, EventArgs e)
        {
          await  DisplayAlert("Регистрация", "Регистрация прошла успешно!", "ОK");
            try
            {
                IsBusy = true;
                //var aServ = new AccountService(App.EndPoint, "");
                //
                //App.Pwd = await aServ.Login(userLogin);
                IsBusy = false;

                Navigation.InsertPageBefore(new SignInView(), this);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                //IsBusy = false;
            }
        }
        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            try
            {
                IsBusy = true;
                //var aServ = new AccountService(App.EndPoint, "");
                //
                //App.Pwd = await aServ.Login(userLogin);
                IsBusy = false;

                Navigation.InsertPageBefore(new SignInView(), this);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                //IsBusy = false;
            }
        }
    }
}