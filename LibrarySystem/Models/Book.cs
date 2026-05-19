namespace LibrarySystem.Models
{
    public class Book
    {
        public int BookID { get; set; }

        public string Title { get; set; }

        public int AuthorID { get; set; }

        public Author? Author { get; set; }

        public ICollection<Genre>? Genres { get; set; }
    }
}