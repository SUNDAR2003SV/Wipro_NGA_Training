namespace HRManagement.Core.Entities
{
    // Represents an employee in the company
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public DateTime JoiningDate { get; set; }

        // Navigation Property
        // One Employee can have many leave requests
        public ICollection<Leave> Leaves { get; set; }
            = new List<Leave>();
    }
}