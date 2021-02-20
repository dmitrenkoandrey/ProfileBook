using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ProfileBook.Models;
using ProfileBook.Service;
using ProfileBook.TreeView;



namespace ProfileBook.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainListView : ContentPage
    {
        public MainListView()
        {
            InitializeComponent();
            logoutItem.IconImageSource = ImageSource.FromFile("logout.png");
            settingsItem.IconImageSource = ImageSource.FromFile("settings.png");
            BindingContext = this;

            IsBusy = false;
        }

        

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = false;
        }

        async void OnLogoutItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignInView());
        }

        async void OnSettingsItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsView());
        }
        async void OnEditProfileItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditProfileView());
        }
        
        void OnUserInfoItemClicked(object sender, EventArgs e)
        {

            //await Navigation.PushAsync(new UserInfoPage(null));
        }
        async void OnExitItemClicked(object sender, EventArgs e)
        {
            var diaRes = await DisplayAlert("Exit", "Are you sure you want to close the application?", "Yes", "Cancel");
            if (diaRes) System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }

       
    }
}
