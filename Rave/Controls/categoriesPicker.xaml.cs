using Rave.ViewModels;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rave.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoriesPicker : ContentView
	{
        List<string> Preferences { get; set; }

		CategoriesViewModel _viewModel;
		public CategoriesPicker()
		{
			InitializeComponent ();
			BindingContext = _viewModel = new CategoriesViewModel();
            Preferences = new List<string>();
            _viewModel.OnAppearing();
        }

        private void SwitchToggled(object sender, PropertyChangingEventArgs e)
        {
            Switch switcher = (Switch)sender;
            if (switcher.IsToggled == true)
            {
                Preferences.Add(switcher.AutomationId);
            }
        }

        public List<string> GetPreferences()
        {
            return Preferences;
        }

    }
}