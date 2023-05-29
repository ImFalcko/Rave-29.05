using MySqlConnector;
using Rave.Models;
using Rave.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rave.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string text;
        private string description;

        public ObservableCollection<Category> Categories { get; }
        public Command LoadItemsCommand { get; }
        public MockDataStore MockDataStore { get; set; }

        public NewItemViewModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Categories = new ObservableCollection<Category>();
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Stablishment newItem = new Stablishment()
            {
                Id =Convert.ToInt32( Guid.NewGuid()),
                Description = Description
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
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
