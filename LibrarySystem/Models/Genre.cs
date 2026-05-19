namespace LibrarySystem.Models
{
    public class Genre
    {
        public int GenreID { get; set; }

        public string GenreName { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}