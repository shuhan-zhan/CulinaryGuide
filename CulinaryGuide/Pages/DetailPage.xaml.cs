using CulinaryGuide.Models;
using CulinaryGuide.ViewModels;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace CulinaryGuide.Pages;

[QueryProperty(nameof(Name), "name")]
public partial class DetailPage : ContentPage
{
    private Restaurant _restaurant;
    private bool _isFavorite;

    public string Name { get; set; }

    public DetailPage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        // 根据接收到的餐厅名称从全局数据源查找
        _restaurant = await GetRestaurantByNameAsync(Name);
        if (_restaurant == null)
        {
            _restaurant = new Restaurant { Name = "Unknown Restaurant" };
        }

        // 检查是否已收藏
        var existing = await App.Database.GetFavoriteByNameAsync(_restaurant.Name);
        _isFavorite = existing != null;

        // 设置 ViewModel 并更新 UI
        BindingContext = new DetailPageViewModel(_restaurant);
        UpdateFavoriteButton();

        // 为按钮绑定事件（如果 XAML 已绑定则无需此步，但确保方法存在）
        if (FavoriteButton != null)
        {
            FavoriteButton.Clicked -= FavoriteButton_Clicked; // 避免重复订阅
            FavoriteButton.Clicked += FavoriteButton_Clicked;
        }
    }

    private async Task<Restaurant> GetRestaurantByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;
        // 从 App 的全局列表中查找（忽略大小写）
        return App.AllRestaurants?.Find(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    private void UpdateFavoriteButton()
    {
        if (FavoriteButton == null) return;
        // 根据收藏状态设置按钮文本（❤️ 已收藏，🤍 未收藏）
        FavoriteButton.Text = _isFavorite ? "❤️" : "🤍";
    }

    private async void FavoriteButton_Clicked(object sender, EventArgs e)
    {
        if (_restaurant == null) return;

        if (_isFavorite)
        {
            // 取消收藏
            var favorite = await App.Database.GetFavoriteByNameAsync(_restaurant.Name);
            if (favorite != null)
            {
                await App.Database.RemoveFavoriteAsync(favorite.Id);
                _isFavorite = false;
                await DisplayAlert("Removed", $"{_restaurant.Name} has been removed from favorites.", "OK");
            }
        }
        else
        {
            // 添加收藏
            var favorite = new FavoriteRestaurant
            {
                Name = _restaurant.Name,
                ShortAddress = _restaurant.ShortAddress,
                ImageUrl = _restaurant.ImageUrl,
                Latitude = _restaurant.Latitude,
                Longitude = _restaurant.Longitude
            };
            await App.Database.AddFavoriteAsync(favorite);
            _isFavorite = true;
            await DisplayAlert("Added", $"{_restaurant.Name} has been added to favorites.", "OK");
        }

        UpdateFavoriteButton(); // 更新按钮图标
    }
}