using ProfileBook.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook
{
    public partial class App : Application
    {
        public static string EndPoint = "https://mmm.com/api/";
        public static string Tocken { get; set; }

        public static bool IsUserLoggedIn { get { return !string.IsNullOrEmpty(Tocken); } }
        public App()
        {
            InitializeComponent();

            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new SignInView());
            }
            else
            {
                MainPage = new NavigationPage(new MainListView());
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
