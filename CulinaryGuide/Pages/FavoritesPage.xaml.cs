using CulinaryGuide.Models;
using CulinaryGuide.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace CulinaryGuide.Pages
{
    public partial class FavoritesPage : ContentPage
    {
        public ObservableCollection<FavoriteRestaurant> Favorites { get; set; } = new();

        public FavoritesPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadFavorites();
        }

        private async Task LoadFavorites()
        {
            var favorites = await App.Database.GetFavoritesAsync();
            Favorites.Clear();
            foreach (var fav in favorites)
                Favorites.Add(fav);
        }

        private async void OnRemoveSwipeInvoked(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var favorite = swipeItem.BindingContext as FavoriteRestaurant;
            if (favorite != null)
            {
                await App.Database.RemoveFavoriteAsync(favorite.Id);
                Favorites.Remove(favorite);
            }
        }
        private async void OnFavoriteSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is FavoriteRestaurant selectedFavorite)
            {
                // 传递餐厅名称到详情页
                await Shell.Current.GoToAsync($"detail?name={selectedFavorite.Name}");
                // 清除选中状态
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}