using ProfileBook.TreeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditProfileView : ContentPage
    {
        public PersonViewModel ViewModel { get; private set; }
        public AddEditProfileView(PersonViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            this.BindingContext = ViewModel;
            saveItem.IconImageSource = ImageSource.FromFile("save.png");
        }

        public AddEditProfileView()
        {
        }

        async void OnSaveItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainListView());
        }
    }
}