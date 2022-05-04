namespace WebClient.Helpers.Api.Models
{
    public class OrderModel
    {
        public int? driverId { get; set; }
        public int EmployeeId { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryLocation { get; set; }
        public bool isDelivered { get; set; }
        public List<OrderProductModel> Products { get; set; }
    }

    public class OrderProductModel
    {

        public int ProductId { get; set; }

        public int Amount { get; set; }

    }
}
