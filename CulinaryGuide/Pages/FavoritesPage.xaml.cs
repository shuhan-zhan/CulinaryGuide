using CulinaryGuide.Models;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace CulinaryGuide.Pages
{
    public partial class FavoritesPage : ContentPage
    {
        public List<Restaurant> Favorites { get; set; }

        public FavoritesPage()
        {
            InitializeComponent();

            // 模拟收藏数据（包含图片）
            Favorites = new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "McDonald's",
                    ShortAddress = "120 Xuefu Road",
                    ImageUrl = "restaurant3"   // 确保图片文件存在
                },
                new Restaurant
                {
                    Name = "Starbucks",
                    ShortAddress = "100 Xuefu Road",
                    ImageUrl = "restaurant2"
                }
            };

            // 设置 BindingContext
            BindingContext = this;
        }

        // 右滑“Remove”按钮的事件
        private async void OnRemoveSwipeInvoked(object sender, System.EventArgs e)
        {
            // 获取被滑动的餐厅项（此处简化：实际需要获取具体项）
            await DisplayAlert("Remove", "This item will be removed from favorites (demo).", "OK");
        }
    }
}