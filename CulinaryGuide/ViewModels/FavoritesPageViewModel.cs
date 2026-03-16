using System.Collections.Generic;
using CulinaryGuide.Models;

namespace CulinaryGuide.ViewModels;

public class FavoritesPageViewModel
{
    public List<Restaurant> FavoriteRestaurants { get; set; }

    public FavoritesPageViewModel()
    {
        // 模拟收藏数据
        FavoriteRestaurants = new List<Restaurant>
        {
            new Restaurant
            {
                Name = "McDonald's",
                Rating = 4.2,
                ShortAddress = "120 Xuefu Road"
            }
        };
    }
}