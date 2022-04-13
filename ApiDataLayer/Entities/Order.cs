using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class Order
    {

        public int OrderId { get; set; }

        public int DriverId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string TrackingNumber { get; set; } = string.Empty;

        public string DeliveryAddress { get; set; } = string.Empty;

        public string DeliveryLocation { get; set; } = string.Empty;

        public Driver Driver { get; set; } = new();

        public Employee Employee { get; set; } = new();

    }
}
