using Rave.Services;
using Rave.ViewModels;
using System;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rave.Views
{
    public partial class ProfilePage : ContentPage
    {
        SqlConnection Connection { get; set; }
        ProfileViewModel _viewModel;

        public ProfilePage()
        {
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            InitializeComponent();
            BindingContext = _viewModel = new ProfileViewModel();
            Connection = new SqlConnection();
            userinfo_Clicked(null, null);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        public async void ProfileImageUpdate(object sender)
        {
            ImagePicker imagePicker = new ImagePicker();
            IsBusy = false;
            ImageSource newSource = await imagePicker.GetImage();
            Image newImage = new Image();
            newImage.Source = newSource;

            IsBusy = true;
            circleFrame.Content = newImage;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            clsImagen clsImagen = new clsImagen();
            ImagePicker imagePicker = new ImagePicker();
            IsBusy = false;

            ImageSource newSource = await imagePicker.GetImage();
            profileImage.Source = newSource;

            Stream file = imagePicker.File ;
            byte[] bImage = clsImagen.ConvertImageToByteArray(file);
            Connection.UpdateProfileImage(
                bImage,
                Convert.ToInt32(userId.Text));
            IsBusy = true;
        }

        private void yourRaves_Clicked(object sender, EventArgs e)
        {
            tabFrame.Content = new YourItems();
            yourraves.BackgroundColor = Color.FromHex("#480856");
            yourraves.TextColor = Color.White;
            userinfo.BackgroundColor = Color.Silver;
            userinfo.TextColor = Color.FromHex("#480856");
        }

        private void userinfo_Clicked(object sender, EventArgs e)
        {
            tabFrame.Content = new UserInfo();
            yourraves.BackgroundColor = Color.Silver;
            yourraves.TextColor = Color.FromHex("#480856");

            userinfo.TextColor = Color.White;
            userinfo.BackgroundColor = Color.FromHex("#480856");
        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage= new LoginPage();
        }
    }
}