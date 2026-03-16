using Microsoft.Maui.Controls;

namespace CulinaryGuide.Pages
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(".."); // 返回上一页
        }
    }
}