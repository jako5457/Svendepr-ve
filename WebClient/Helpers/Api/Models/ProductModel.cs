namespace WebClient.Helpers.Api.Models
{
    public class ProductModel
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public double BuyPrice { get; set; }

        public string EAN { get; set; } = default!;

        public List<int> Categories { get; set; } = default!;
    }
}
