using MySqlConnector;
using Rave.Models;
using Rave.Services;
using Rave.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using User = Rave.Models.User;

namespace Rave.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Stablishment Stablishment { get; set; }
        public List<string> Categories { get; set; }
        public Stream stream { get; set; }
        NewItemViewModel _viewModel;
        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewItemViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            ImagePicker imagePicker = new ImagePicker();
            IsBusy = false;
            ImageSource newSource = await imagePicker.GetImage();
            ImageButton newImage = (ImageButton)sender;
            stream = imagePicker.File;
            if (newSource != null)
            {
                newImage.Source = newSource;
            } else
            {
                newImage.Source = "default_img.png";
            }
        }

        private void CreateButton_Clicked(object sender, EventArgs e)
        {
            User user = Application.Current.Properties["user"] as User;
            string Name = nameEntry.Text;
            string Description = descriptionEntry.Text;
            int Owner = Convert.ToInt32(user.Id);
            DateTime StartDate = DatePicker.Date;
            TimeSpan StartTime = TimePicker.Time;
            Categories = CategoriesPicker.GetPreferences();
            byte[] ProfileImage = clsImagen.ConvertImageToByteArray(stream);

            string valueCategories = string.Join(",", Categories);

            SqlConnection con = new SqlConnection();
            string insertQuery = "INSERT INTO ravedata (Name, Description, Owner, StartDate, StartTime, Image, MusicStyles, CreationDate, UpdateDate, Location, Latitude, Longitude) " +
                                 "VALUES (@name, @description, @owner, @startDate, @startTime, @image, @musicstyles, @creationdate, @updatedate, @location, @latitude, @longitude)";
            MySqlCommand cmd = new MySqlCommand(insertQuery);
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@description", Description);
            cmd.Parameters.AddWithValue("@owner", Owner);
            cmd.Parameters.AddWithValue("@startDate", StartDate);
            cmd.Parameters.AddWithValue("@startTime", MySqlDbType.Time).Value = StartTime;
            cmd.Parameters.AddWithValue("@image", ProfileImage);
            cmd.Parameters.AddWithValue("@musicstyles", MySqlDbType.Set).Value = valueCategories;
            cmd.Parameters.AddWithValue("@creationdate", DateTime.Now);
            cmd.Parameters.AddWithValue("@updatedate", DateTime.Now);
            cmd.Parameters.AddWithValue("@location", addressEntry.Text);
            cmd.Parameters.AddWithValue("@latitude", latitudeEntry.Text);
            cmd.Parameters.AddWithValue("@longitude", longitudeEntry.Text);

            con.ExecuteQuery(cmd);
        }

        public void UbicationButton_Clicked(object sender, EventArgs e)
        {
            double latitude = Convert.ToDouble(latitudeEntry.Text);
            double longitude = Convert.ToDouble(longitudeEntry.Text);
            newItemMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude), Distance.FromMiles(1)));
            newItemMap.Pins.Add(new Pin
            {
                Position = new Position(latitude, longitude),
                Label = "Ubicación",
                Address = addressEntry.Text
            });
        }
    }
}