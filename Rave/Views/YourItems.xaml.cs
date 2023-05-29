using Rave.ViewModels;
using Syncfusion.XForms.PopupLayout;
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
	public partial class YourItems : ContentView
    { 
        YourItemsViewModel _viewModel;
		public YourItems ()
		{
			InitializeComponent ();
            BindingContext = _viewModel = new YourItemsViewModel();
            _viewModel.OnAppearing();
        }
    }
}