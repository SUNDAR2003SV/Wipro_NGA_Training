using System;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        // Connection String
        string connectionString = "Server=localhost;Database=Database_Connectivity_Test;Trusted_Connection=True;TrustServerCertificate=True;";

        // Create connection
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connected Successfully!");

                // SQL Query
                string query = "SELECT * FROM Students";

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Email: {reader["Email"]}");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}