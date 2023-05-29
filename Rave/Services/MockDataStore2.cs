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
    public class MockDataStore2 : IDataStore<Raves>
    {
        List<Raves> raves = new List<Raves>();
        List<Category> categories = new List<Category>();
        
        MySqlConnection connection;

        public MockDataStore2()
        {
           connection = new SqlConnection().Connection;
           SetCategories();
           SetStablishments();
        }

        public void demoData()
        {

        }

        // Metodo que carga los establecimientos de la base de datos
        
        public List<Raves> SetStablishments()
        {
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
                string name  = reader.GetString("Name");
                string description = reader.GetString("Description");
                string categories = reader.GetString("MusicStyles");
                Image profileImage = new Image()
                {
                    Source = clsImagen.ConvertByteArrayToImage((byte[])reader["Image"])
                }; 
                string location = reader.GetString("Location");
                double latitude = reader.GetDouble("Latitude");
                double longitude = reader.GetDouble("Longitude");
                DateTime date = reader.GetDateTime("StartDate");
                TimeSpan time = reader.GetTimeSpan("StartTime");

                // Creamos un objeto Stablishment con los datos de la consulta
                Raves rave = new Raves
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Categories = categories,
                    Image = profileImage,
                    Location = location,
                    Latitude = latitude,
                    Longitude = longitude,
                    StartDate = date,
                    StartTime = time
                };
                raves.Add(rave);
            }
            connection.Close();

            // Devolvemos la lista de establecimientos
            return raves;
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
        public async Task<bool> AddItemAsync(Raves rave)
        {
            raves.Add(rave);
            return await Task.FromResult(true);
        }

        // Metodo que actualiza un establecimiento de la lista
        public async Task<bool> UpdateItemAsync(Raves rave)
        {
            var oldItem = raves.Where((Raves arg) => arg.Id == rave.Id).FirstOrDefault();
            raves.Remove(oldItem);
            raves.Add(rave);

            return await Task.FromResult(true);
        }

        // Metodo que elimina un establecimiento de la lista
        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = raves.Where((Raves arg) => arg.Id == id).FirstOrDefault();
            raves.Remove(oldItem);
            return await Task.FromResult(true);
        }

        // Metodo que devuelve un establecimiento de la lista
        public async Task<Raves> GetItemAsync(int id)
        {
            return await Task.FromResult(raves.FirstOrDefault(s => s.Id == id));
        }

        // Metodo que devuelve la lista de establecimientos
        public async Task<IEnumerable<Raves>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(raves);
        }
    }
}