namespace Api.Model
{
    public class OrderUpdateModel
    {
        public int? DriverId { get; set; }

        public int? EmployeeId { get; set; }

        public string DeliveryAddress { get; set; } = default!;

        public string DeliveryLocation { get; set; } = default!;

        public bool IsDelivered { get; set; } = default!;
    }
}
