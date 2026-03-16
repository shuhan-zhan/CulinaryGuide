using CulinaryGuide.Models;
using System.Windows.Input;

namespace CulinaryGuide.ViewModels;

public class MainPageViewModel
{
    public List<Restaurant> Restaurants { get; set; }
    public ICommand NavigateToDetailCommand { get; }

    public MainPageViewModel()
    {
        // 模拟数据
        Restaurants = new List<Restaurant>
        {
            new Restaurant
            {
                Name = "Chuan Wei Xuan",
                Rating = 4.5,
                Distance = "200m",
                ShortAddress = "88 Xuefu Road",
                FullAddress = "88 Xuefu Road,Chuan Wei Xuan",
                Phone = "123-4567"
            },
            new Restaurant
            {
                Name = "Starbucks",
                Rating = 4.3,
                Distance = "350m",
                ShortAddress = "100 Xuefu Road",
                FullAddress = "100 Xuefu Road,Starbucks",
                Phone = "234-5678"
            },
            new Restaurant
            {
                Name = "McDonald's",
                Rating = 4.2,
                Distance = "500m",
                ShortAddress = "120 Xuefu Road",
                FullAddress = "120 Xuefu Road,McDonald's",
                Phone = "345-6789"
            }
        };

        NavigateToDetailCommand = new Command<Restaurant>(async (restaurant) =>
        {
            await Shell.Current.GoToAsync("detail", new Dictionary<string, object>
            {
                ["Restaurant"] = restaurant
            });
        });
    }
}