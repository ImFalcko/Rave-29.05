using MySqlConnector;
using Rave.Services;
using Rave.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using Syncfusion;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.Buttons;

namespace Rave.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        CategoriesViewModel _viewModel;
        List<string> preferences;
		public RegisterPage ()
		{
			InitializeComponent ();
            BindingContext = _viewModel = new CategoriesViewModel();
            preferences = new List<string>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            FormatChecker formatChecker = new FormatChecker();

            string username = usernameEntry.Text;
            string password = passwordEntry.Text;
            string email = emailEntry.Text;
            string phone = phoneEntry.Text;
            string country = countryEntry.Text;

            if (!formatChecker.CheckText(username))
            {
                DisplayAlert("Error", "Username is not valid", "OK");
                return;
            } else if(!formatChecker.CheckPassword(password))
            {
                DisplayAlert("Error", "Password is not valid", "OK");
                return;
            } else if (!formatChecker.CheckEmail(email))
            {
                DisplayAlert("Error", "Email is not valid", "OK");
                return;
            } else if (!formatChecker.CheckPhoneNumber(phone))
            {
                DisplayAlert("Error", "Phone is not valid", "OK");
                return;
            } else if (!formatChecker.CheckText(country))
            {
                DisplayAlert("Error", "Country is not valid", "OK");
                return;
            }
            else
            {
                try
                {
                    // Crear un objeto que representa la conexión a la base de datos
                    Services.SqlConnection connection = new Services.SqlConnection();
                
                    // Sentencia SQL que se ejecutará en la base de datos
                    string insertquery = "INSERT INTO userInfo (username, email, country, phone , preferences, creationDate, updateDate) " +
                        "VALUES (@username, @email, @country, @phone, @preferences ,@creationDate, @updateDate)";

                    // Se establecen los datos de los parámetros de la sentencia SQL
                    MySqlCommand commandBuilder = new MySqlCommand(insertquery);
                    commandBuilder.Parameters.Clear();
                    commandBuilder.Parameters.AddWithValue("@username", username);
                    commandBuilder.Parameters.AddWithValue("@password", password);
                    commandBuilder.Parameters.AddWithValue("@email", email);
                    commandBuilder.Parameters.AddWithValue("@phone", phone);
                    commandBuilder.Parameters.AddWithValue("@country", country);
                    commandBuilder.Parameters.AddWithValue("@creationDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
                    commandBuilder.Parameters.AddWithValue("@updateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));

                    // Conversion de lista a string de la lista de preferencias
                    preferences = CategoriesPicker.GetPreferences();
                    string valuePreferences = string.Join(",", preferences);
                    commandBuilder.Parameters.AddWithValue("@preferences", MySqlDbType.Set).Value = valuePreferences;

                    // Insertar un valor en la tabla
                    connection.ExecuteQuery(commandBuilder);

                    insertquery = "INSERT INTO userlogincheck (Username, Password) " +
                        "VALUES (@username, @password)";

                    // Se establecen los datos de los parámetros de la sentencia SQL
                    commandBuilder = new MySqlCommand(insertquery);
                    commandBuilder.Parameters.Clear();
                    commandBuilder.Parameters.AddWithValue("@username", username);
                    commandBuilder.Parameters.AddWithValue("@password", password);

                    // Insertar un valor en la tabla
                    connection.ExecuteQuery(commandBuilder);


                    // Se abre la pagina de login
                    App.Current.MainPage = new LoginPage();
                }
                catch (Exception ex)
                { 
                    DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }

    }
}