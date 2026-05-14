using Microsoft.Data.SqlClient;
using System.Data;
using BookStoreApp.Models;

namespace BookStoreApp.Data
{
    public class BookRepository
    {
        private readonly string _connectionString;

        public BookRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // INSERT USING STORED PROCEDURE
        public void AddBook(Book book)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_AddBook", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@Author", book.Author);
            cmd.Parameters.AddWithValue("@Price", book.Price);
            cmd.Parameters.AddWithValue("@PublishedYear", book.PublishedYear);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // READ USING SQLDATAREADER
        public List<Book> GetBooks()
        {
            List<Book> list = new List<Book>();

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Books", con);

            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Book
                {
                    BookId = Convert.ToInt32(reader["BookId"]),
                    Title = reader["Title"].ToString(),
                    Author = reader["Author"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    PublishedYear = Convert.ToInt32(reader["PublishedYear"])
                });
            }

            return list;
        }
    }
}