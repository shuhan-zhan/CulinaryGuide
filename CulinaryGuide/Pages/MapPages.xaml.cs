using Microsoft.Maui.Controls;

namespace CulinaryGuide.Pages
{
    [QueryProperty(nameof(Lng), "lng")]
    [QueryProperty(nameof(Lat), "lat")]
    public partial class MapPage : ContentPage
    {
        private string _lng;
        private string _lat;

        public string Lng
        {
            get => _lng;
            set
            {
                _lng = value;
                LoadMap();
            }
        }

        public string Lat
        {
            get => _lat;
            set
            {
                _lat = value;
                LoadMap();
            }
        }

        public MapPage()
        {
            InitializeComponent();
        }

        private void LoadMap()
        {
            if (string.IsNullOrEmpty(_lng) || string.IsNullOrEmpty(_lat))
                return;

            // 高德静态地图 URL（经度在前，纬度在后）
            string apiKey = "5e0ff870af7117e34a2a9c18f4835b64"; // 替换为你的有效 Web 服务 Key
            string url = $"https://restapi.amap.com/v3/staticmap?location={_lng},{_lat}&zoom=16&size=800*600&markers=mid,0xFF0000,A:{_lng},{_lat}&key={apiKey}";

            // 将 URL 加载到 WebView
            MapWebView.Source = new UrlWebViewSource { Url = url };
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}