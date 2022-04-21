namespace Api.Model
{
    public class DriverModel
    {

        public int DriverId { get; set; }

        public int EmployeeId { get; set; }

        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Phone { get; set; } = default!;

    }
}
