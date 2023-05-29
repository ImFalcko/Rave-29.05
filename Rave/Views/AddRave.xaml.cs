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
    public partial class AddRave : ContentPage
    {
        public AddRave()
        {
            InitializeComponent();
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}