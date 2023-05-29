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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserInfo : ContentView
	{
        SqlConnection SqlConnection { get; set; }
		public UserInfo ()
		{
			InitializeComponent ();
            SqlConnection = new SqlConnection();
		}

        private void EditButton_Cliked(object sender, EventArgs e)
        {
            ImageButton editBton = (ImageButton)sender;
            String AutoId = editBton.AutomationId;
            switch (AutoId)
            {
                case "email":
                    editBton1.IsVisible = false;
                    acceptBton1.IsVisible = true;
                    declineBtn1.IsVisible = true;
                    emailEntry.IsEnabled = true;
                    break;
                case "phone":
                    editBton2.IsVisible = false;
                    acceptBton2.IsVisible = true;
                    declineBtn2.IsVisible = true;
                    phoneEntry.IsEnabled = true;
                    break;
                case "country":
                    editBton3.IsVisible = false;
                    acceptBton3.IsVisible = true;
                    declineBtn3.IsVisible = true;
                    countryEntry.IsEnabled = true;
                    break;
            }


        }

        private void AcceptButton_Cliked(object sender, EventArgs e)
        {
            FormatChecker formatChecker = new FormatChecker();
            ImageButton acceptBton = (ImageButton)sender;
            String AutoId = acceptBton.AutomationId;
            switch (AutoId)
            {
                case "acceptEmail":
                   

                   
                    if (formatChecker.CheckEmail(emailEntry.Text))
                    {
                        editBton1.IsVisible = true;
                        acceptBton1.IsVisible = false;
                        declineBtn1.IsVisible = false;
                        emailEntry.IsEnabled = false;
                        
                        SqlConnection.UpdateValueIntoTable("userinfo", "Email", emailEntry.Text, Convert.ToInt32(userId.Text));
                    }
                    else
                    {
                         App.Current.MainPage.DisplayAlert("Error", "Invalid email format", "OK");
                    }
                    
                    break;

                case "acceptPhone":
                    if (formatChecker.CheckPhoneNumber(phoneEntry.Text))
                    {
                        editBton2.IsVisible = true;
                        acceptBton2.IsVisible = false;
                        declineBtn2.IsVisible = false;
                        phoneEntry.IsEnabled = false;
                        
                        SqlConnection.UpdateValueIntoTable("userinfo", "Phone", phoneEntry.Text, Convert.ToInt32(userId.Text));
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Error", "Invalid email format", "OK");
                    }
                    
                    break;

                case "acceptCountry":
                    if (formatChecker.CheckText(countryEntry.Text)) { 
                    editBton3.IsVisible = true;
                    acceptBton3.IsVisible = false;
                    declineBtn3.IsVisible = false;
                    countryEntry.IsEnabled = false;
                    
                    SqlConnection.UpdateValueIntoTable("userinfo", "Country", countryEntry.Text, Convert.ToInt32(userId.Text));
                   
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Error", "Invalid email format", "OK");
                    }
                    break;
            }
        }
        private void DeclineButton_Cliked(object sender, EventArgs e)
        {
            ImageButton declineBton = (ImageButton)sender;
            String AutoId = declineBton.AutomationId;
            switch (AutoId)
            {
                case "declineEmail":
                    editBton1.IsVisible = true;
                    acceptBton1.IsVisible = false;
                    declineBtn1.IsVisible = false;
                    emailEntry.IsEnabled = false;
                    emailEntry.Text = null;
                    break;
                case "declinePhone":
                    editBton2.IsVisible = true;
                    acceptBton2.IsVisible = false;
                    declineBtn2.IsVisible = false;
                    phoneEntry.IsEnabled = false;
                    phoneEntry.Text = null;
                    break;
                case "declineCountry":
                    editBton3.IsVisible = true;
                    acceptBton3.IsVisible = false;
                    declineBtn3.IsVisible = false;
                    countryEntry.IsEnabled = false;
                    countryEntry.Text = null;
                    break;
            }
        }
    }
}