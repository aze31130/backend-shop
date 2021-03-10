using System.ComponentModel.DataAnnotations;

namespace backend_shop.Models
{
    public class Customer
    {
        [Key]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string adress { get; set; }
    }
}
