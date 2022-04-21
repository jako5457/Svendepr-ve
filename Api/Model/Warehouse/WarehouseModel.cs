namespace Api.Model
{
    public class WarehouseModel
    {
        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string Zipcode { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Phone { get; set; } = default!;

        public int CompanyId { get; set; }
    }
}
