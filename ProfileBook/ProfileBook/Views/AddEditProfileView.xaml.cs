using ProfileBook.TreeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProfileBook.ViewModels;

namespace ProfileBook.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditProfileView : ContentPage
    {
        public static string Imagesource { set; get; }
        public PersonViewModel ViewModel { get; private set; }
        public AddEditProfileView(PersonViewModel vm)
        {
            //image1.Source = "pic_profile1";
            InitializeComponent();
            //var PersListView = new PersonsListViewModel();
            if (PersonsListViewModel.IsBusy1 == false)
            {
                image1.Source = "pic_profile1.png";
                    
            }
            else
            {
                image1.Source = Imagesource;
            }
 
            ViewModel = vm;
            this.BindingContext = ViewModel;
            saveItem.IconImageSource = ImageSource.FromFile("save.png");
            takePhotoItem.IconImageSource = ImageSource.FromFile("ic_camera.png");
            getPhotoItem.IconImageSource = ImageSource.FromFile("ic_camera1.png");
            // выбор фото
            
        }

        public AddEditProfileView()
        {
        }
   
        }
    }
