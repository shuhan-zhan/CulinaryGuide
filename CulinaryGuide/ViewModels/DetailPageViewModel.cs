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
                await Shell.Current.GoToAsync("map");
            });
        }
    }
}