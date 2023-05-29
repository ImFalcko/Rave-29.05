using MySqlConnector;
using Rave.Models;
using Rave.Services;
using Rave.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Syncfusion.XForms.PopupLayout;

namespace Rave.ViewModels
{
    public class YourItemsViewModel : BaseViewModel
    {
        private Raves _selectedItem;

        public Command FavoriteTapped { get; }
        public ObservableCollection<Raves> Stablishments { get; }
        public ObservableCollection<Raves> Raves { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command SetColumnCommand { get; }
        public Command<Raves> ItemTapped { get; }
        public string FavoriteSource { get; set; }
        public bool IsFavorite { get; set; }
        public int Column { get; set; }

        public YourItemsViewModel()
        {
            Title = "Browse";
            Stablishments = new ObservableCollection<Raves>();
            Raves = new ObservableCollection<Raves>();
            LoadItemsCommand = new Command(() => SetRaves());
            ItemTapped = new Command<Raves>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            FavoriteTapped = new Command<Stablishment>(OnFavoriteClicked);
            SetColumnCommand = new Command(setColumn);
        }

        public void SetRaves()
        {
            IsBusy = true;
            Raves.Clear();
            int userId = (App.Current.Properties["user"] as User).Id;
            SqlConnection sqlConnection = new SqlConnection();
            MySqlConnection connection = sqlConnection.Connection;
            // Abrimos la conexión
            connection.Open();

            // Creamos la consulta
            string query = "SELECT * FROM ravedata";
            MySqlCommand command = new MySqlCommand(query, connection);

            // Ejecutamos la consulta
            MySqlDataReader reader = (MySqlDataReader)command.ExecuteReader();
            while (reader.Read())
            {
                int Id = reader.GetInt32("Id");
                string name = reader.GetString("Name");
                string description = reader.GetString("Description");
                string categories = reader.GetString("MusicStyles");
                Image raveImage;
                try
                {
                    raveImage = new Image()
                    {
                        Source = clsImagen.ConvertByteArrayToImage((byte[])reader["Image"])
                    };
                }
                catch
                {
                    raveImage = new Image();
                }
                 
                // Creamos un objeto Stablishment con los datos de la consulta
                Raves rave = new Raves
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Image = raveImage,
                    Categories = categories
                };
                Raves.Add(rave);
                IsBusy = false;
            }
            connection.Close();

            // Devolvemos la lista de establecimientos
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Raves SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private void OnFavoriteClicked(Stablishment stablish)
        {
            SqLiteService sqLiteService = new SqLiteService();
            if (!sqLiteService.AddFavorite(stablish))
            {
                sqLiteService.DelFavorite(stablish);
            }
        }

        async void OnItemSelected(Raves rave)
        {
            PopupView popupView = new PopupView();
            if (rave == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack

            await Shell.Current.GoToAsync($"{nameof(RaveDetailPage)}?{nameof(RaveDetailViewModel.Id)}={rave.Id}");
        }

        public void setColumn()
        {
            if(Column == 0)
            {
                Column = 1;
            }
            else
            {
                Column = 0;
            }
        }
    }
}