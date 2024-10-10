namespace HR.LeaveManagement.Application.Models.Identity
{
    public class Employee
    {
        public string Id { get; set; }

        public string Email { get; set; }
        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

    }
}
