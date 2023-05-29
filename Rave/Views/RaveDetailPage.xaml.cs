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
    public partial class RaveDetailPage : ContentPage
    {
        public RaveDetailPage()
        {
            InitializeComponent();
            BindingContext = new RaveDetailViewModel();
        }

    }
}