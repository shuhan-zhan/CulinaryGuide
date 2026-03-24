using CulinaryGuide.Models;
using CulinaryGuide.Services;
using System.Collections.Generic;
using System.IO;

namespace CulinaryGuide
{
    public partial class App : Application
    {
        public static DatabaseService Database { get; private set; }
        public static List<Restaurant> AllRestaurants { get; private set; }

        public App()
        {
            InitializeComponent();

            // 初始化数据库
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "culinaryguide.db3");
            Database = new DatabaseService(dbPath);

            // 初始化全局餐厅列表（与 MainPage 中的模拟数据一致）
            AllRestaurants = new List<Restaurant>
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

            MainPage = new AppShell();
        }
    }
}