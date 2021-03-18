using System.ComponentModel.DataAnnotations;

namespace backend_shop.DTO
{
    public class RegisterModel
    {
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
