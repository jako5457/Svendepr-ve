using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class Driver
    {
        public int DriverId { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = new();
    }
}
