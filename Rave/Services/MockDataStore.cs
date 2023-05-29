using Rave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using Xamarin.Forms;
using System.Data.SqlTypes;
using System.Data;

namespace Rave.Services
{
    public class MockDataStore : IDataStore<Stablishment>
    {
        List<Stablishment> stablishments = new List<Stablishment>();
        List<Raves> Raves = new List<Raves>();
        List<Category> categories = new List<Category>();
        MySqlConnection connection;

        public MockDataStore()
        {
           connection = new SqlConnection().Connection;
           SetCategories();
            SetStablishments();
        }

        public void demoData()
        {

        }

        // Metodo que carga los establecimientos de la base de datos
        public List<Stablishment> SetStablishments()
        {
            // Abrimos la conexión
             connection.Open();

            // Creamos la consulta
            string query = "SELECT * FROM stablishmentdata";
            SqLiteService sqLiteService = new SqLiteService();
            List<Favorite> favorites =  sqLiteService.GetFavorites();
            MySqlCommand command = new MySqlCommand(query, connection);

            // Ejecutamos la consulta
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string categories = reader.GetString("MusicStyles");
                List<string> categoriesList = categories.Split(',').ToList();
                Image raveImage = new Image { Source = clsImagen.ConvertByteArrayToImage((byte[])reader["Image"]) };
                int id = reader.GetInt32("Id");
                string name = reader.GetString("Name");
                string description = reader.GetString("Description");
                string favoritesource = "favoriteDisabled.png";
                foreach (Favorite favorite in favorites)
                {
                    if (favorite.StablishmentId == id)
                    {
                        favoritesource = "favoriteEnabled.png";
                    }
                }
                // Creamos un objeto Stablishment con los datos de la consulta
                Stablishment stablishment = new Stablishment
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    ProfileImage = raveImage,
                    Categories = string.Join(", ", categoriesList),
                    FavoriteSource = favoritesource
                    
                };
                stablishments.Add(stablishment);
            }
            connection.Close();

            // Devolvemos la lista de establecimientos
            return stablishments;
            
        }

        // Metodo que carga las categorias de la base de datos
        public List<Category> SetCategories()
        {
            // Abrimos la conexión
            connection.Open();

            // Creamos la consulta
            string query = "SELECT * FROM musicstyles";
            MySqlCommand command = new MySqlCommand(query, connection);

            // Ejecutamos la consulta
            MySqlDataReader reader = (MySqlDataReader)command.ExecuteReader();
            while (reader.Read())
            {
                // Creamos un objeto Category con los datos de la consulta
                Category category = new Category
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };

                // Añadimos la categoria a la lista
                categories.Add(category);
            }
            connection.Close();

            // Devolvemos la lista de categorias
            return categories;
        }

        // Metodo que añade a la lista de establecimientos un nuevo establecimiento
        public async Task<bool> AddItemAsync(Stablishment item)
        {
            stablishments.Add(item);
            return await Task.FromResult(true);
        }

        // Metodo que actualiza un establecimiento de la lista
        public async Task<bool> UpdateItemAsync(Stablishment item)
        {
            var oldItem = stablishments.Where((Stablishment arg) => arg.Id == item.Id).FirstOrDefault();
            stablishments.Remove(oldItem);
            stablishments.Add(item);

            return await Task.FromResult(true);
        }

        // Metodo que elimina un establecimiento de la lista
        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = stablishments.Where((Stablishment arg) => arg.Id == id).FirstOrDefault();
            stablishments.Remove(oldItem);

            return await Task.FromResult(true);
        }

        // Metodo que devuelve un establecimiento de la lista
        public async Task<Stablishment> GetItemAsync(int id)
        {
            return await Task.FromResult(stablishments.FirstOrDefault(s => s.Id == id));
        }

        // Metodo que devuelve la lista de establecimientos
        public async Task<IEnumerable<Stablishment>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(stablishments);
        }
    }
}