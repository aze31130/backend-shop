using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_shop.DTO
{
    public class OrderDTO
    {
        public int customerId { get; set; }
        public List<int> productId { get; set; }
    }
}
