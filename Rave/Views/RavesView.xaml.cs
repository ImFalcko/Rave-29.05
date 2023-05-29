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
    public partial class RavesView : ContentView
    {
        RavesViewModel _viewModel;
        public RavesView()
        {
            InitializeComponent();
            BindingContext = _viewModel = new RavesViewModel();

            _viewModel.OnAppearing();
        }
    }
}