using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class Company
    {

        public int CompanyId { get; set; }

        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string Zipcode { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Phone { get; set; } = default!;

        [JsonIgnore]
        public List<Warehouse> Warehouses { get; set; } = default!;

        [JsonIgnore]
        public List<Employee> Employees { get; set; } = default!;

    }
}
