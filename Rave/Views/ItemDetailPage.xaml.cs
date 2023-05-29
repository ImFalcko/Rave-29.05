using Rave.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Rave.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
            
        }
    }
}