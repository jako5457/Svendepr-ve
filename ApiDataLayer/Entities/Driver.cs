using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class Driver
    {
        public int DriverId { get; set; }

        public int EmployeeId { get; set; }

        [JsonIgnore]
        public Employee Employee { get; set; } = default!;

        [JsonIgnore]
        public List<Order> Orders { get; set; } = default!;
    }
}
