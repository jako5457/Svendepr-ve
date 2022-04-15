using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double BuyPrice { get; set; }

        public string EAN { get; set; } = string.Empty;

        public List<ProductCategory> Categories { get; set; } = new List<ProductCategory>();
    }
}
