using System;
using System.Diagnostics;
using Rave.Models;
using Rave.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Rave.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(Id))]
    public class RaveDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string name;
        private string description;
        private ImageSource profileimagesource;
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime StartDate { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public string Location { get; set; }
        public Map map { get; set; }
        public ContentView MapView { get; set; }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }


        public ImageSource ProfileImageSource
        {
            get => profileimagesource;
            set => SetProperty(ref profileimagesource, value);
        }

        public async void LoadItemId(int itemId)
        {
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                Raves item = await DataStore2.GetItemAsync(itemId);
                Id = item.Id;
                Name = item.Name;
                Description = item.Description;
                ProfileImageSource = sqlConnection.SelectImage("SELECT Image FROM ravedata WHERE Id = " + item.Id + ";");
                Location = item.Location;
                Latitude = item.Latitude;
                Longitude = item.Longitude;
                StartDate = item.StartDate;
                StartTime = item.StartTime;
                Map raveLocation = new Map(MapSpan.FromCenterAndRadius(new Position(Latitude, Longitude), Distance.FromMiles(0.1)));
                raveLocation.Pins.Add(new Pin
                {
                    Type = PinType.Place,
                    Label = Name,
                    Position = new Position(Latitude, Longitude)
                });
                map = raveLocation;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
