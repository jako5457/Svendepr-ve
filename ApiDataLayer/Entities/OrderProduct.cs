using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class OrderProduct
    {
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int Amount { get; set; }

        [JsonIgnore]
        public Order Order { get; set; } = default!;

        [JsonIgnore]
        public Product Product { get; set; } = default!;

    }
}
