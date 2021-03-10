using System.ComponentModel.DataAnnotations;

namespace backend_shop.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        public string label { get; set; }
        public string description { get; set; }
        public int price { get; set; }
    }
}
