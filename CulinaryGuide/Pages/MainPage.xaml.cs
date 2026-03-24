using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CulinaryGuide.Models;
using CulinaryGuide.Services;

namespace CulinaryGuide.Pages
{
    public partial class MainPage : ContentPage
    {
        private List<Restaurant> _allRestaurants; // 原始数据，用于搜索
        private List<Restaurant> _restaurants;    // 当前显示的数据

        public MainPage()
        {
            InitializeComponent();
            LoadMockData();
            BindingContext = this;
            // 页面加载时不自动获取位置，等用户点击 Refresh
        }

        private void LoadMockData()
        {
            // 创建原始数据
            _allRestaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Id = 1,
                    Name = "DaDong Roast Duck",
                    Rating = 4.8,
                    Distance = "",
                    ShortAddress = "138 Wangfujing Street",
                    FullAddress = "5th Floor, Beijing apm Shopping Mall, 138 Wangfujing Street, Dongcheng District, Beijing",
                    Phone = "+86 10 6511 2323",
                    ImageUrl = "restaurant1",
                    FirstLetter = "D",
                    Latitude = 39.9108,
                    Longitude = 116.4116
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "Starbucks",
                    Rating = 4.5,
                    Distance = "",
                    ShortAddress = "269 Wangfujing Street",
                    FullAddress = "L1, Wangfujing Central, 269 Wangfujing Street, Dongcheng District, Beijing",
                    Phone = "+86 10 6522 8888",
                    ImageUrl = "restaurant2",
                    FirstLetter = "S",
                    Latitude = 39.9078,
                    Longitude = 116.4122
                },
                new Restaurant
                {
                    Id = 3,
                    Name = "McDonald's",
                    Rating = 4.2,
                    Distance = "",
                    ShortAddress = "255 Wangfujing Street",
                    FullAddress = "No.255 Wangfujing Street, Dongcheng District, Beijing",
                    Phone = "+86 10 6525 6666",
                    ImageUrl = "restaurant3",
                    FirstLetter = "M",
                    Latitude = 39.9085,
                    Longitude = 116.4140
                }
            };
            // 显示数据初始为全部
            _restaurants = new List<Restaurant>(_allRestaurants);
            Restaurants = _restaurants;
        }

        public List<Restaurant> Restaurants { get; private set; }

        // 页面出现时加载收藏状态
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadFavoriteStatuses();
        }

        // 从数据库加载每个餐厅的收藏状态，并更新 IsFavorite 属性
        private async Task LoadFavoriteStatuses()
        {
            foreach (var r in _allRestaurants)
            {
                var fav = await App.Database.GetFavoriteByNameAsync(r.Name);
                r.IsFavorite = fav != null;
            }
            // 刷新显示列表，使界面上的 SwipeItem 文本和颜色更新
            RefreshDisplayedList();
        }

        // 刷新按钮点击事件：获取位置并更新标签，然后计算距离
        private async void RefreshLocation_Clicked(object sender, EventArgs e)
        {
            await UpdateLocationAsync();
        }

        private async Task UpdateLocationAsync()
        {
            try
            {
                // 请求定位权限
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Permission Denied", "Location permission is required to show your current position.", "OK");
                    return;
                }

                // 获取当前位置
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(10)
                });

                if (location != null)
                {
                    // 更新界面上的位置标签
                    CurrentLocationText.Text = $"Lat: {location.Latitude:F4}, Lon: {location.Longitude:F4}";
                    // 计算餐厅距离并刷新列表
                    UpdateRestaurantDistances(location);
                }
                else
                {
                    CurrentLocationText.Text = "Unable to get location";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to get location: {ex.Message}", "OK");
            }
        }

        // 根据用户当前位置计算所有餐厅距离并更新
        private void UpdateRestaurantDistances(Location currentLocation)
        {
            foreach (var r in _allRestaurants)
            {
                var restaurantLocation = new Location(r.Latitude, r.Longitude);
                var distanceInMeters = currentLocation.CalculateDistance(restaurantLocation, DistanceUnits.Kilometers) * 1000;
                // 格式化显示：小于1000米显示“XXXm”，否则显示“X.Xkm”
                r.Distance = distanceInMeters < 1000 ? $"{distanceInMeters:F0}m" : $"{distanceInMeters / 1000:F1}km";
            }
            // 刷新当前显示的列表（如果搜索框有文字，需重新过滤）
            RefreshDisplayedList();
        }

        // 刷新显示的列表（根据当前搜索词）
        private void RefreshDisplayedList()
        {
            var searchText = SearchEntry?.Text?.ToLower() ?? "";
            if (string.IsNullOrWhiteSpace(searchText))
            {
                _restaurants = new List<Restaurant>(_allRestaurants);
            }
            else
            {
                _restaurants = _allRestaurants
                    .Where(r => r.Name.ToLower().Contains(searchText) ||
                                r.ShortAddress.ToLower().Contains(searchText))
                    .ToList();
            }
            Restaurants = _restaurants;
            RestaurantsCollectionView.ItemsSource = null;
            RestaurantsCollectionView.ItemsSource = Restaurants;
        }

        // 搜索框文本改变时触发
        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshDisplayedList();
        }

        // 餐厅列表选中事件：导航到详情页
        private async void OnRestaurantSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Restaurant selectedRestaurant)
            {
                await Shell.Current.GoToAsync($"detail?name={selectedRestaurant.Name}");
                ((CollectionView)sender).SelectedItem = null;
            }
        }

        // 右滑事件：根据收藏状态添加或移除收藏
        private async void OnFavoriteSwipeInvoked(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var restaurant = swipeItem?.BindingContext as Restaurant;
            if (restaurant == null) return;

            // 检查当前收藏状态
            var existing = await App.Database.GetFavoriteByNameAsync(restaurant.Name);
            if (existing != null)
            {
                // 已收藏 → 移除
                await App.Database.RemoveFavoriteAsync(existing.Id);
                restaurant.IsFavorite = false;
                await DisplayAlert("Removed", $"{restaurant.Name} removed from favorites.", "OK");
            }
            else
            {
                // 未收藏 → 添加
                var favorite = new FavoriteRestaurant
                {
                    Name = restaurant.Name,
                    ShortAddress = restaurant.ShortAddress,
                    ImageUrl = restaurant.ImageUrl,
                    Latitude = restaurant.Latitude,
                    Longitude = restaurant.Longitude
                };
                await App.Database.AddFavoriteAsync(favorite);
                restaurant.IsFavorite = true;
                await DisplayAlert("Added", $"{restaurant.Name} added to favorites.", "OK");
            }

            // 强制刷新列表，使 SwipeItem 的文本和颜色更新
            RestaurantsCollectionView.ItemsSource = null;
            RestaurantsCollectionView.ItemsSource = Restaurants;
        }
    }
}