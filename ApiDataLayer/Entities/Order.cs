using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class Order
    {

        public int OrderId { get; set; }

        public int? DriverId { get; set; }

        public int? EmployeeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid TrackingCode { get; set; } = default!;

        public string DeliveryAddress { get; set; } = default!;

        public string DeliveryLocation { get; set; } = default!;

        public bool IsDelivered { get; set; } = default!;

        [JsonIgnore]
        public Driver? Driver { get; set; } = default!;

        [JsonIgnore]
        public Employee? Employee { get; set; } = default!;

        [JsonIgnore]
        public List<OrderProduct> Products { get; set; } = default!;

    }
}
