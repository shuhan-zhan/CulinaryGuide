namespace CulinaryGuide.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public double Rating { get; set; }
        public string Distance { get; set; }
        public string ShortAddress { get; set; }
        public string FullAddress { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }   // 新增：图片文件名（不带扩展名）
        public string FirstLetter { get; set; } // 可选，如果不使用首字母色块可删除
    }
}