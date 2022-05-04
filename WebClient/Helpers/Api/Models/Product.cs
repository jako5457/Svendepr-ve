namespace WebClient.Helpers.Api.Models
{
    public class Product
    {
        public int productId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int buyPrice { get; set; }
        public string ean { get; set; }
    }
}
