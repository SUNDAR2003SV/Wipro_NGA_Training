using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Book>? Books { get; set; }
    }
}