using SQLite;

namespace CulinaryGuide.Models
{
    public class FavoriteRestaurant
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortAddress { get; set; }
        public string ImageUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        // 可以添加其他需要的字段
    }
}