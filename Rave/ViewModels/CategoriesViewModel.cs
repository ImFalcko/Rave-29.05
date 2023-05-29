using System;
using Rave.Models;
using Rave.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using MySqlConnector;
using System.Collections.Generic;

namespace Rave.ViewModels
{
 
    class CategoriesViewModel: BaseViewModel
    {
        
        public ObservableCollection<Category> Categories { get; }
        public Command LoadItemsCommand { get; }
        public MockDataStore MockDataStore { get; set; }

        public CategoriesViewModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Categories = new ObservableCollection<Category>();
        }

        async Task ExecuteLoadItemsCommand()
        {
            try
            {
                // Creamos la conexión
                SqlConnection connection = new SqlConnection();
                IsBusy = true;
                // Abrimos la conexión
                connection.Connection.Open();

                // Creamos la consulta
                string query = "SELECT * FROM musicstyles";
                MySqlCommand command = new MySqlCommand(query, connection.Connection);

                // Ejecutamos la consulta
                MySqlDataReader reader = (MySqlDataReader)command.ExecuteReader();
                while (reader.Read())
                {
                    // Creamos un objeto Category con los datos de la consulta
                    Category category = new Category
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        
                    };

                    // Añadimos la categoria a la lista
                    Categories.Add(category);
                }
                connection.Connection.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public void OnAppearing()
        {
            IsBusy = true;
            
        }
    }
}
