using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class ProductRequestProduct
    {
        public int ProductRequestId { get; set; }

        public int ProductId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int Amount { get; set; }

        [JsonIgnore]
        public Product Product { get; set; } = default!;

        [JsonIgnore]
        public ProductRequest ProductRequest { get; set; } = default!;
    }
}
