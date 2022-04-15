using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Company? Company { get; set; }

        public Driver? Driver { get; set; }

        public List<Order> Orders { get; set; } = default!;

        public List<ProductRequest> ProductRequests { get; set; } = default!;
    }
}
