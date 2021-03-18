using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_shop.Models
{
    public class Authenticate
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
