using MySqlConnector;
using Rave.Models;
using Rave.Services;
using Rave.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rave.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            FormatChecker formatChecker = new FormatChecker();
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            if (username == null || password == null)
            {
                DisplayAlert("Error", "Los campos no pueden estar vacios", "OK");
                return;
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection();
                    string selectQuery = "SELECT * FROM UserLoginCheck WHERE username = '" + username + "' AND password = '" + password + "'";
                    List<string> list= con.SelectFrom(selectQuery);

                    if(list.Count>0)
                    {
                        int id = Convert.ToInt32(list[0]);
                        selectQuery = "SELECT * FROM UserInfo WHERE Id ='" + id + "'";
                        con.Connection.Open();
                        MySqlCommand cmd = new MySqlCommand(selectQuery, con.Connection);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        string email = (string)reader["email"];
                        string phone = (string)reader["phone"];
                        string country = (string) reader["country"];
                        string preferences = reader.GetString("preferences");
                        Image profileImage = new Image()
                        {
                            Source = clsImagen.ConvertByteArrayToImage((byte[])reader["profileimage"])
                        };
                       
                        reader.Close();

                        List<string> preferencesList = preferences.Split(',').ToList();

                        User user = new User
                        {
                            Id = id,
                            UserName = username,
                            Email = email,
                            Phone = phone,
                            Country = country,
                            ProfileImage = profileImage,
                            Preferences = preferencesList
                        };

                        App.Current.Properties["user"] = user;
                        App.Current.MainPage = new AppShell();
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        private void registerButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new RegisterPage();
        }
    }
}