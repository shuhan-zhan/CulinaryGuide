using System.Windows.Input;
using CulinaryGuide.Models;
using Microsoft.Maui.Controls;

namespace CulinaryGuide.ViewModels
{
    public class DetailPageViewModel
    {
        public Restaurant Restaurant { get; set; }
        public ICommand NavigateToMapCommand { get; }

        public DetailPageViewModel(Restaurant restaurant)
        {
            Restaurant = restaurant;

            NavigateToMapCommand = new Command(async () =>
            {
                // 确保餐厅坐标有效
                if (Restaurant == null || (Restaurant.Latitude == 0 && Restaurant.Longitude == 0))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Restaurant location is not available.", "OK");
                    return;
                }

                // 传递经度、纬度和餐厅名称（参数名与 MapPage 的 QueryProperty 一致）
                await Shell.Current.GoToAsync($"map?lng={Restaurant.Longitude}&lat={Restaurant.Latitude}&name={Restaurant.Name}");
            });
        }
    }
}