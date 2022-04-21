namespace Api.Model
{
    public class ProductRequestModel
    {

        public string Location { get; set; } = default!;

        public int EmployeeId { get; set; } = default!;

        public List<ProductRequestProductModel> Products { get; set; } = default!;
    }
}
