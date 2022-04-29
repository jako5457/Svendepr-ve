using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class ProductCategory
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; } = default!;

        [JsonIgnore]
        public Category Category { get; set; } = default!;
    }
}
