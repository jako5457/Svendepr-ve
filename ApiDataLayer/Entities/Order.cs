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

        public int? DriverId { get; set; }

        public int? EmployeeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid TrackingInfoId { get; set; } = default!;

        public string DeliveryAddress { get; set; } = default!;

        public string DeliveryLocation { get; set; } = default!;

        public Driver? Driver { get; set; } = default!;

        public Employee? Employee { get; set; } = default!;

        public List<OrderProduct> Products { get; set; } = default!;

    }
}
