namespace HRManagement.Core.Entities
{
    // Represents leave request
    public class Leave
    {
        public int LeaveId { get; set; }

        // Foreign Key
        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Reason { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        // Navigation Property
        // Leave belongs to one Employee
        public Employee? Employee { get; set; }
    }
}