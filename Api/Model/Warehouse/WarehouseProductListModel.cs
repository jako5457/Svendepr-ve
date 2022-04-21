namespace Api.Model
{
    public class WarehouseProductListModel : WarehouseProduct
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public double BuyPrice { get; set; }

        public string EAN { get; set; } = default!;
    }
}
