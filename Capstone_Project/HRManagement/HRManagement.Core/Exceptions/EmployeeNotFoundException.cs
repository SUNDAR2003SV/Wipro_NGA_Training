namespace HRManagement.Core.Exceptions
{
    // Custom exception for employee not found
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException(int id)
            : base($"Employee with Id {id} not found.")
        {
        }
    }
}