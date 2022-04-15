using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class ProductCategory
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public Product Product { get; set; } = default!;

        public Category Category { get; set; } = default!;
    }
}
