using CulinaryGuide.Models;
using CulinaryGuide.ViewModels;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CulinaryGuide.Pages;

[QueryProperty(nameof(Name), "name")]  // 接收名为 "name" 的参数
public partial class DetailPage : ContentPage
{
    private Restaurant _restaurant;

    public string Name { get; set; }

    public DetailPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        // 根据接收到的餐厅名称查找完整数据
        _restaurant = GetRestaurantByName(Name);
        if (_restaurant == null)
        {
            _restaurant = new Restaurant { Name = "Unknown Restaurant" };
        }

        // 设置 ViewModel 作为 BindingContext
        BindingContext = new DetailPageViewModel(_restaurant);
    }

    // 模拟数据源（应与 MainPage 保持一致）
    private Restaurant GetRestaurantByName(string name)
    {
        var restaurants = new List<Restaurant>
        {
            new Restaurant
            {
                Name = "ChuanWeiXuan",
                Rating = 4.5,
                Distance = "200m",
                ShortAddress = "88 Xuefu Road",
                FullAddress = "88 Xuefu Road, Jiang'an District",
                Phone = "+86 123 4567 8901",
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
                ImageUrl = "restaurant3",
                FirstLetter = "M"
            }
        };
        return restaurants.FirstOrDefault(r => r.Name == name);
    }

    // 收藏按钮点击事件
    private void FavoriteButton_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        btn.Text = btn.Text == "🤍" ? "❤️" : "🤍";
        DisplayAlert("Info", "Favorite status toggled (demo mode)", "OK");
    }
}