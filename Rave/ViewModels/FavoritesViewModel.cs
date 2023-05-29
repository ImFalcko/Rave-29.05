using Rave.Models;
using Rave.Services;
using Rave.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Rave.ViewModels
{
    public class FavoritesViewModel: BaseViewModel
    {

        
        private Stablishment _selectedItem;
        public ObservableCollection<Stablishment> Stablishments { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command FavoriteTapped { get; }
        public Command<Stablishment> ItemTapped { get; }

        public FavoritesViewModel()
        {
            Title = "Browse";
            // Lista de establecimientos favoritos
            Stablishments = new ObservableCollection<Stablishment>();

            // Comando para cargar los establecimientos favoritos
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            // Comando que se ejecuta cuando se selecciona un establecimiento
            ItemTapped = new Command<Stablishment>(OnItemSelected);

            // Comando que se ejecuta cuando se pulsa el botón de favorito
            FavoriteTapped = new Command<Stablishment>(OnFavoriteClicked);

            // Comando que se ejecuta cuando se añade un establecimiento como favorito
            AddItemCommand = new Command(OnAddItem);
        }

        // Método que se ejecuta al cargar la pagina para cargar los datos
        async Task ExecuteLoadItemsCommand()
        {

            IsBusy = true;

            // Cargamos los establecimientos favoritos
            try
            {
                SqLiteService sqLiteService = new SqLiteService();
                Stablishments.Clear();
                var items =  sqLiteService.GetFavorites();
                foreach (var item in items)
                {
                    Stablishment stablish= await DataStore.GetItemAsync(item.StablishmentId);
                    SqlConnection sqlConnection = new SqlConnection();
                    stablish.ProfileImage = new Image
                    {
                        Source = sqlConnection.SelectImage("SELECT Image FROM stablishmentData WHERE Id = " + stablish.Id + ";")
                    };
                    // Obtenemos la imagen del establecimiento
                        
                


                    Stablishments.Add(stablish);
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
