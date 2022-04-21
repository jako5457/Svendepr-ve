using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class ProductRequest
    {
        public int ProductRequestId { get; set; }

        public string Location { get; set; } = default!;

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = default!;

        public List<ProductRequestProduct> Products { get; set; } = default!;
    }
}
