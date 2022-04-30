namespace Api.Model
{
    public class EmployeeModel
    {
        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Phone { get; set; } = default!;

        public int? CompanyId { get; set; }
    }

}
