using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.Models;

namespace MovieCatalogAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Director> Directors { get; set; }
    }
}