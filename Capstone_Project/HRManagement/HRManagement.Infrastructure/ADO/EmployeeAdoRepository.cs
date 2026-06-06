using Microsoft.Data.SqlClient;

namespace HRManagement.Infrastructure.ADO
{
    public class EmployeeAdoRepository
    {
        private readonly string _connectionString;

        public EmployeeAdoRepository(
            string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get Total Employee Count
        public async Task<int> GetEmployeeCountAsync()
        {
            using SqlConnection connection =
                new SqlConnection(_connectionString);

            await connection.OpenAsync();

            SqlCommand command =
                new SqlCommand(
                    "SELECT COUNT(*) FROM Employees",
                    connection);

            var result =
                await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }
    }
}