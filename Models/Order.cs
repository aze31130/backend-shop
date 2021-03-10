using System.ComponentModel.DataAnnotations;

namespace backend_shop.Models
{
    public class Order
    {
        [Key]
        public int id { get; set; }
        public int customerId { get; set; }
        public int productId { get; set; }
    }
}
