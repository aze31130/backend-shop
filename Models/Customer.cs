using System.ComponentModel.DataAnnotations;

namespace backend_shop.Models
{
    public class Customer
    {
        [Key]
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
        public string adress { get; set; }
    }
}
