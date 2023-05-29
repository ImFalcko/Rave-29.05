using MySqlConnector;
using Rave.Models;
using Rave.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Rave.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public User User { get; set; }
        public Command LoadProfileCommand { get; }

        MySqlConnection connection;
        
            
            
        public ProfileViewModel()
        {
            Title = "Profile";
            
            connection = new SqlConnection().Connection;
            LoadProfile();
        }

        private void LoadProfile()
        {
            try
            {
                SetUserInfo();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void SetUserInfo()
        {
           User = App.Current.Properties["user"] as User;
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public async void ProfileImageUpdate(Object sender)
        {
            ImagePicker imagePicker = new ImagePicker();
            IsBusy = false;
            await imagePicker.GetImage();
            IsBusy = true;
        }
    }
}
