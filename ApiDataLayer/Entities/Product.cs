using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public double BuyPrice { get; set; }

        public string EAN { get; set; } = default!;

        [JsonIgnore]
        public List<ProductCategory> Categories { get; set; } = default!;
    }
}
