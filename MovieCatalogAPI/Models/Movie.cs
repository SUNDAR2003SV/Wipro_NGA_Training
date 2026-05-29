using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCatalogAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        // Foreign Key
        public int DirectorId { get; set; }

        [ForeignKey("DirectorId")]
        public Director? Director { get; set; }
    }
}