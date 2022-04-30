using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }

        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string Zipcode { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Phone { get; set; } = default!;

        public int CompanyId { get; set; }

        [JsonIgnore]
        public Company Company { get; set; } = default!;

        [JsonIgnore]
        public List<WarehouseProduct> Products { get; set; } = default!;

    }
}
