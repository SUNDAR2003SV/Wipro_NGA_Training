using System.ComponentModel.DataAnnotations;

namespace HRManagement.Core.DTOs.Leave
{
    public class LeaveRequestDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;
    }
}