using System;
using System.Diagnostics;
using Rave.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Rave.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(Id))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string name;
        private string description;
        private Image profileimage;
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
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


        public Image ProfileImage
        {
            get => profileimage;
            set => SetProperty(ref profileimage, value);
        }

        public async void LoadItemId(int itemId)
        {
            SqlConnection sqlConnection = new SqlConnection();
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Name = item.Name;
                Description = item.Description;
                ProfileImage = new Image
                {
                    Source = sqlConnection.SelectImage("SELECT Image FROM stablishmentData WHERE Id = " + item.Id + ";")
                };
                Latitude = item.Latitude;
                Longitude = item.Longitude;


                

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
