namespace HRManagement.Core.Exceptions
{
    public class InvalidLeaveException : Exception
    {
        public InvalidLeaveException(string message)
            : base(message)
        {
        }
    }
}