﻿namespace Api.Model
{
    public class OrderModel
    {
        public int? DriverId { get; set; }

        public int? EmployeeId { get; set; }

        public string DeliveryAddress { get; set; } = default!;

        public string DeliveryLocation { get; set; } = default!;

        public List<OrderProductModel> Products = default!;
    }
}
