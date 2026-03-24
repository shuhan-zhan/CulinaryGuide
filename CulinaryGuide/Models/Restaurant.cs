using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CulinaryGuide.Models
{
    public class Restaurant : INotifyPropertyChanged
    {
        private string _distance;
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public string Distance
        {
            get => _distance;
            set
            {
                if (_distance != value)
                {
                    _distance = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ShortAddress { get; set; }
        public string FullAddress { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public string FirstLetter { get; set; }
        public double Latitude { get; set; }       // 餐厅纬度
        public double Longitude { get; set; }      // 餐厅经度

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool _isFavorite;
        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}