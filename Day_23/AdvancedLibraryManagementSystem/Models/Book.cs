using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        public decimal Price { get; set; }

        public int PublishedYear { get; set; }

        public int AuthorId { get; set; }

        public int GenreId { get; set; }

        public Author Author { get; set; }

        public Genre Genre { get; set; }
    }
}