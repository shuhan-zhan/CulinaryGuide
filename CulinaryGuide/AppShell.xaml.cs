using CulinaryGuide.Pages;

namespace CulinaryGuide;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // 注册详情页和地图页的路由
        Routing.RegisterRoute("detail", typeof(DetailPage));
        Routing.RegisterRoute("map", typeof(MapPage));
    }
}