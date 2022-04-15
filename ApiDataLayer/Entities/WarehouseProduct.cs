using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class WarehouseProduct
    {
        public int WarehouseId { get; set; }

        public int ProductId { get; set; }

        public int Amount { get; set; }

        public Product Product { get; set; } = default!;

        public Warehouse Warehouse { get; set; } = default!;
    }
}
