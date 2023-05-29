using Rave.Models;
using Rave.Services;
using Rave.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Rave.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Stablishment _selectedItem;

        public Command FavoriteTapped { get; }
        public ObservableCollection<Stablishment> Stablishments { get; }
        public List<string> Tags { get; set; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Stablishment> ItemTapped { get; }
        public List<string> FavoriteSource { get; set; }
        public bool IsFavorite { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Stablishments = new ObservableCollection<Stablishment>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Stablishment>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            FavoriteTapped = new Command<Stablishment>(OnFavoriteClicked);
            FavoriteSource = new List<string>();
            Tags = new List<string>();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                
                Stablishments.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Stablishments.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Stablishment SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private void OnFavoriteClicked(Stablishment stablish)
        {
            SqLiteService sqLiteService = new SqLiteService();
            if (!sqLiteService.AddFavorite(stablish))
            {
                sqLiteService.DelFavorite(stablish);
            }

        }

        async void OnItemSelected(Stablishment stablish)
        {
            if (stablish == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.Id)}={stablish.Id}");
        }

    }
}