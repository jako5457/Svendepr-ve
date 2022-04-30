using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Phone { get; set; } = default!;

        public int? CompanyId { get; set; }

        [JsonIgnore]
        public Company? Company { get; set; }

        [JsonIgnore]
        public Driver? Driver { get; set; }

        [JsonIgnore]
        public List<Order> Orders { get; set; } = default!;

        [JsonIgnore]
        public List<ProductRequest> ProductRequests { get; set; } = default!;
    }
}
