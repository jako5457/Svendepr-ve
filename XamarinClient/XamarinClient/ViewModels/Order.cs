using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinClient.ViewModels
{
    public class Order
    {
        public int orderId { get; set; }
        public int? driverId { get; set; }
        public int employeeId { get; set; }
        public string createdDate { get; set; }
        public string trackingCode { get; set; }
        public string deliveryAddress { get; set; }
        public string deliveryLocation { get; set; }
        public bool isDelivered { get; set; }
    }
}
