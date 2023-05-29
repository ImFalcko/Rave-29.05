﻿using MySqlConnector;
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

namespace Rave.ViewModels
{
    public class RavesViewModel : BaseViewModel
    {
        private Raves _selectedItem;

        public Command FavoriteTapped { get; }
        public ObservableCollection<Raves> Raves { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Raves> ItemTapped { get; }
        public string FavoriteSource { get; set; }
        public bool IsFavorite { get; set; }
        public int Column { get; set; }

        public RavesViewModel()
        {
            Title = "Browse";
            Raves = new ObservableCollection<Raves>();
            LoadItemsCommand = new Command(() => SetRaves());
            ItemTapped = new Command<Raves>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            FavoriteTapped = new Command<Stablishment>(OnFavoriteClicked);
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
                int id = reader.GetInt32("Id");
                string name = reader.GetString("Name");
                string description = reader.GetString("Description");
                string categories = reader.GetString("MusicStyles");
                Image raveImage;
                try
                {
                    raveImage = new Image()
                    {
                        Source = clsImagen.ConvertByteArrayToImage((byte[])reader["Image"]),
                        HorizontalOptions= LayoutOptions.CenterAndExpand
                    };
                }
                catch
                {
                    raveImage = new Image();
                }
                
                // Creamos un objeto Stablishment con los datos de la consulta
                Raves rave = new Raves
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    Image = raveImage,
                    Categories = categories,
                };
                Raves.Add(rave);
            }
            connection.Close();
            IsBusy = false;
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
            if (rave == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(RaveDetailPage)}?{nameof(RaveDetailViewModel.Id)}={rave.Id}");
        }
    }
}