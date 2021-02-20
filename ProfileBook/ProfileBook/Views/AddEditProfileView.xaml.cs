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
        public AddEditProfileView()
        {
            InitializeComponent();
            saveItem.IconImageSource = ImageSource.FromFile("save.png");
        }
        async void OnSaveItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainListView());
        }
    }
}