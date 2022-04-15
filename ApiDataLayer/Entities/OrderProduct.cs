using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class OrderProduct
    {
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int Amount { get; set; }

        public Order Order { get; set; } = new();

        public Product Product { get; set; } = new();

    }
}
