namespace HRManagement.Core.Entities
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }

        public string Action { get; set; } = string.Empty;

        public string EntityName { get; set; } = string.Empty;

        public DateTime ActionDate { get; set; }

        public string PerformedBy { get; set; } = string.Empty;
    }
}