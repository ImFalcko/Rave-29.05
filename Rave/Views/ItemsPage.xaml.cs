using Rave.Models;
using Rave.Services;
using Rave.ViewModels;
using Rave.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using Xamarin.Forms.Xaml;

namespace Rave.Views
{
    public partial class ItemsPage : ContentPage
    {
        CategoriesViewModel _viewModel;
        View View { get; set; }
        List<Stablishment> StartStablishments = new List<Stablishment>();
        List<Raves> StartRaves = new List<Raves>();
        SqlConnection SqlConnection = new SqlConnection();
        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CategoriesViewModel();
            stablishbutton_Clicked(null, null);
            View = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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

        private void filterButton_Clicked(object sender, EventArgs e)
        {
            if (FilterFrame.IsVisible)
            {
                FilterFrame.IsVisible = false;
            } else
            {
                FilterFrame.IsVisible = true;
            }
        }

        private void ravesbutton_Clicked(object sender, EventArgs e)
        {
            RavesView raveView = new RavesView();
            tabFrame.Content = raveView;
            ravesbutton.BackgroundColor = Color.FromHex("#480856");
            ravesbutton.TextColor = Color.White;
            CollectionView items = (CollectionView)raveView.FindByName("ItemsListView");
            StartRaves = items.ItemsSource.Cast<Raves>().ToList();
            foreach (var item in StartRaves)
            {
                item.Image = new Xamarin.Forms.Image{
                  Source =  SqlConnection.SelectImage("SELECT Image FROM ravedata WHERE Id = " + item.Id + ";") };
            }
            stablishbutton.BackgroundColor = Color.Silver;
            stablishbutton.TextColor = Color.FromHex("#480856");
        }

        private void stablishbutton_Clicked(object sender, EventArgs e)
        {
            StablishView stablishView= new StablishView();
            tabFrame.Content = stablishView;
            CollectionView items = (CollectionView)stablishView.FindByName("ItemsListView");
            StartStablishments = items.ItemsSource.Cast<Stablishment>().ToList();
            ravesbutton.BackgroundColor = Color.Silver;
            ravesbutton.TextColor = Color.FromHex("#480856");
            foreach (var item in StartStablishments)
            {
                item.ProfileImage = new Xamarin.Forms.Image
                {
                    Source = SqlConnection.SelectImage("SELECT Image FROM stablishmentdata WHERE Id = " + item.Id + ";")
                };
            }
            stablishbutton.TextColor = Color.White;
            stablishbutton.BackgroundColor = Color.FromHex("#480856");
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            string searchTem = e.NewTextValue;
            searchTem = searchTem.ToLower();

            View view = tabFrame.Content;
            View itemsView = null;
            string type = view.GetType().ToString();
            if (view != null)
            {
                if (!string.IsNullOrEmpty(type))
                {
                    ;
                    if (type.Contains("StablishView"))
                    {
                        itemsView = (StablishView)view;
                    }
                    else
                    {
                        itemsView = (RavesView)view;
                    }
                }
            }
                    
            if (string.IsNullOrEmpty(searchTem))
            {
                if (type.Contains("StablishView"))
                {
                    CollectionView items = (CollectionView)itemsView.FindByName("ItemsListView");
                    foreach (var item in StartStablishments)
                    {
                        item.ProfileImage = new Xamarin.Forms.Image
                        {
                            Source = SqlConnection.SelectImage("SELECT Image FROM stablishmentdata WHERE Id = " + item.Id + ";")
                        };
                    }
                    var filteredList = StartStablishments;
                    items.ItemsSource = filteredList;
                }
                else
                {
                    CollectionView items = (CollectionView)itemsView.FindByName("ItemsListView");
                    foreach (var item in StartRaves)
                    {
                        item.Image = new Xamarin.Forms.Image
                        {
                            Source = SqlConnection.SelectImage("SELECT Image FROM ravedata WHERE Id = " + item.Id + ";")
                        };
                    }
                    var filteredList = StartRaves;
                    items.ItemsSource = filteredList;
                }
                    
            } else
            {
                if (type.Contains("StablishView"))
                {
                    CollectionView items = (CollectionView)itemsView.FindByName("ItemsListView");
                    var filteredList = StartStablishments.Where(a => a.Name.ToLower().Contains(searchTem.ToLower()));
                    foreach (var item in filteredList)
                    {
                        item.ProfileImage = new Xamarin.Forms.Image
                        {
                            Source = SqlConnection.SelectImage("SELECT Image FROM stablishmentdata WHERE Id = " + item.Id + ";")
                        };
                    }
                    items.ItemsSource = filteredList;
                }
                else
                {
                    CollectionView items = (CollectionView)itemsView.FindByName("ItemsListView");
                    var filteredList = StartRaves.Where(a => a.Name.ToLower().Contains(searchTem.ToLower()));
                    foreach (var item in filteredList)
                    {
                        item.Image = new Xamarin.Forms.Image
                        {
                            Source = SqlConnection.SelectImage("SELECT Image FROM ravedata WHERE Id = " + item.Id + ";")
                        };
                    }
                    items.ItemsSource = filteredList;
                }
                
            }
            
        }

        private void SfComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            View view = tabFrame.Content;
            View itemsView = null;
            string type = view.GetType().ToString();
            ObservableCollection<Object> selectedCategories = new ObservableCollection<Object>();
            selectedCategories= (ObservableCollection<Object>) FilterComboBox.SelectedValue;
            if (view != null)
            {
                if (!string.IsNullOrEmpty(type))
                {
                    ;
                    if (type.Contains("StablishView"))
                    {
                        itemsView = (StablishView)view;
                    }
                    else
                    {
                        itemsView = (RavesView)view;
                    }
                }
            }

            if (selectedCategories.Count<=0)
            {
                if (type.Contains("StablishView"))
                {
                    CollectionView items = (CollectionView)itemsView.FindByName("ItemsListView");
                    StablishView stablishview = (StablishView)view;
                    foreach (var item in StartStablishments)
                    {
                        item.ProfileImage = new Xamarin.Forms.Image
                        {
                            Source = SqlConnection.SelectImage("SELECT Image FROM stablishmentdata WHERE Id = " + item.Id + ";")
                        };
                    }
                    items.ItemsSource = StartStablishments;
                }
                else
                {
                    CollectionView items = (CollectionView)itemsView.FindByName("ItemsListView");
                    RavesView stablishview = (RavesView)view;
                    foreach (var item in StartRaves)
                    {
                        item.Image = new Xamarin.Forms.Image
                        {
                            Source = SqlConnection.SelectImage("SELECT Image FROM ravedata WHERE Id = " + item.Id + ";")
                        };
                    }
                    items.ItemsSource = StartRaves;
                }
            }
            else
            {
                if (type.Contains("StablishView"))
                {
                    
                    CollectionView items = (CollectionView)itemsView.FindByName("ItemsListView");
                    IEnumerable<Stablishment> categoryList = null;
                    List<Stablishment> filteredList = new List<Stablishment>();
                    foreach (Category selectedcategory in selectedCategories)
                    {

                        categoryList = StartStablishments.Where(a => a.Categories.ToLower().Contains(selectedcategory.Name.ToLower()));
                        foreach (Stablishment item in categoryList)
                        {
                            filteredList.Add(item);
                        }
                    }
                    foreach (var item in filteredList)
                    {
                        item.ProfileImage = new Xamarin.Forms.Image
                        {
                            Source = SqlConnection.SelectImage("SELECT Image FROM stablishmentdata WHERE Id = " + item.Id + ";")
                        };
                    }
                   
                    items.ItemsSource = filteredList;
                }
                else
                {
                    CollectionView items = (CollectionView)itemsView.FindByName("ItemsListView");
                    IEnumerable<Raves> categoryList = null;
                    List<Raves> filteredList = new List<Raves>();
                    foreach (Category selectedcategory in selectedCategories)
                    {

                        categoryList = StartRaves.Where(a => a.Categories.ToLower().Contains(selectedcategory.Name.ToLower()));
                        foreach (Raves item in categoryList)
                        {
                            filteredList.Add(item);
                        }
                    }
                    foreach (var item in filteredList)
                    {
                        item.Image = new Xamarin.Forms.Image
                        {
                            Source = SqlConnection.SelectImage("SELECT Image FROM ravedata WHERE Id = " + item.Id + ";")
                        };
                    }

                    items.ItemsSource = filteredList;
                }
            }
        }
    }
}