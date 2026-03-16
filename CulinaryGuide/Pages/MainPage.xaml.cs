using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CulinaryGuide.Models;

namespace CulinaryGuide.Pages
{
    public partial class MainPage : ContentPage
    {
        private List<Restaurant> _restaurants;

        public MainPage()
        {
            InitializeComponent();
            LoadMockData();
            BindingContext = this;
            _ = RefreshLocationAsync();
        }

        private void LoadMockData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "ChuanWeiXuan",
                    Rating = 4.5,
                    Distance = "200m",
                    ShortAddress = "88 Xuefu Road",
                    FullAddress = "88 Xuefu Road, Jiang'an District", // 添加完整地址
                    Phone = "+86 123 4567 8901",                     // 添加电话
                    ImageUrl = "restaurant1",
                    FirstLetter = "C"
                },
                new Restaurant
                {
                    Name = "Starbucks",
                    Rating = 4.3,
                    Distance = "350m",
                    ShortAddress = "100 Xuefu Road",
                    FullAddress = "100 Xuefu Road, Jiang'an District",
                    Phone = "+86 123 4567 8902",
                    ImageUrl = "restaurant2",
                    FirstLetter = "S"
                },
                new Restaurant
                {
                    Name = "McDonald's",
                    Rating = 4.0,
                    Distance = "500m",
                    ShortAddress = "120 Xuefu Road",
                    FullAddress = "120 Xuefu Road, Jiang'an District",
                    Phone = "+86 123 4567 8903",
                    ImageUrl = "restaurant3",   // 确保 Resources/Images 中有 restaurant3.jpg/png
                    FirstLetter = "M"
                }
            };
            Restaurants = _restaurants;
        }

        public List<Restaurant> Restaurants { get; private set; }

        private async void RefreshLocation_Clicked(object sender, EventArgs e)
        {
            await RefreshLocationAsync();
        }

        private async Task RefreshLocationAsync()
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Permission Denied", "Location permission is required to show your current position.", "OK");
                    return;
                }

                var location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(10)
                });

                if (location != null)
                {
                    CurrentLocationText.Text = $"Lat: {location.Latitude:F4}, Lon: {location.Longitude:F4}";
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

        private async void OnRestaurantSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Restaurant selectedRestaurant)
            {
                await Shell.Current.GoToAsync($"detail?name={selectedRestaurant.Name}");
                ((CollectionView)sender).SelectedItem = null;
            }
        }

        private async void OnFavoriteSwipeInvoked(object sender, EventArgs e)
        {
            await DisplayAlert("Favorite", "This restaurant will be added to favorites (functionality coming in next assignment).", "OK");
        }
    }
}