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

//using DevExpress.Mobile.DataGrid;
//using DevExpress.Mobile.Core.Containers;


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
            //employeesItem.IconImageSource = ImageSource.FromFile("user26.png");
            //departmentItem.IconImageSource = ImageSource.FromFile("Department26.png");
            //reportItem.IconImageSource = ImageSource.FromFile("reports24.png");
            //exitItem.IconImageSource = ImageSource.FromFile("exit_logup2.png");
            logoutItem.IconImageSource = ImageSource.FromFile("logout.png");
            settingsItem.IconImageSource = ImageSource.FromFile("settings.png");
            BindingContext = this;

            IsBusy = false;
        }

        //public async Task RefreshData()
        //{
        //    Employees.Clear();
        //    var dataServ = new DataService(App.EndPoint, App.Tocken);
        //    var empls = await dataServ.getEmployees();
        //    foreach (var empl in empls)
        //    {
        //        Employees.Add(empl);
        //    }

        //    //employeesList.BindingContext = Employees;
        //    //employeesList.SetBinding(BindableProperty..BindingContext = Employees;
        //    BindingContext = null; // только для devexpress
        //    BindingContext = this;
        //    //employeesList.IsPullToRefreshEnabled = true;
        //    //employeesList.Redraw(false);
        //    //employeesList.RefreshData();
        //}

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
        //async void OnEmployeeItemClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new EmployeePage());
        //}
        //async void OnDepartmentItemClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new DepartmentPage());
        //}
        //async void OnReportItemClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new ReportsPage());
        //}
        void OnUserInfoItemClicked(object sender, EventArgs e)
        {

            //await Navigation.PushAsync(new UserInfoPage(null));
        }
        async void OnExitItemClicked(object sender, EventArgs e)
        {
            var diaRes = await DisplayAlert("Exit", "Are you sure you want to close the application?", "Yes", "Cancel");
            if (diaRes) System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }

        //private void employeesList_ItemTapped(object sender, ItemTappedEventArgs e)
        //{

        //}

        //async void reportItem2_Clicked(object sender, EventArgs e)
        //{
        //    //await Navigation.PushAsync(new ReportPage2());
        //}

        //private void MenuItemNew_Clicked(object sender, EventArgs e)
        //{
        //    var menuitem = sender as MenuItem;
        //    if (menuitem != null)
        //    {
        //        //var name = menuitem.BindingContext as Employee;
        //        Employees.Add(new Employee() { Id = Guid.NewGuid(), UserName = "aaaa", DateOfBirth = DateTime.Today, EMail = "aaaa", FirstName = "aaaa", LastName = "aaaa" });
        //    }
        //}
        //private void MenuItemEdit_Clicked(object sender, EventArgs e)
        //{
        //    var menuitem = sender as MenuItem;
        //    if (menuitem != null)
        //    {
        //        var name = menuitem.BindingContext as Employee;
        //        DisplayAlert("Alert", "Item Edit " + name.Name, "Ok");
        //    }
        //}

        //private void MenuItemDelete_Clicked(object sender, EventArgs e)
        //{
        //    var menuitem = sender as MenuItem;
        //    if (menuitem != null)
        //    {
        //        var empl = menuitem.BindingContext as Employee;
        //        Employees.Remove(empl);
        //    }
        //}
    }
}
