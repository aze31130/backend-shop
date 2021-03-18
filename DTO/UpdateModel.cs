using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_shop.DTO
{
    public class UpdateModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string currentpassword { get; set; }
        public string newpassword { get; set; }
        public string confirmednewpassword { get; set; }
    }
}
