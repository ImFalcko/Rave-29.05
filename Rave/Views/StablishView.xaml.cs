using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rave.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rave.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StablishView : ContentView
    {
        ItemsViewModel _viewModel;
        public StablishView()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemsViewModel();

            _viewModel.OnAppearing();
        }


        public void FavoriteBton1_Clicked(object sender, EventArgs e)
        {
            ImageButton Favorite = (ImageButton)sender;
            string Source = Favorite.Source.ToString();
            if (Source.Contains("Disabled"))
            {
                Favorite.Source = "favoriteEnabled.png";
            }
            else
            {
                Favorite.Source = "favoriteDisabled.png";
            }
        }
    }
}