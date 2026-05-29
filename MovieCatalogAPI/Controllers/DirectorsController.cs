using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.Data;
using MovieCatalogAPI.Models;

namespace MovieCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DirectorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/directors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Director>>> GetDirectors()
        {
            return await _context.Directors.ToListAsync();
        }

        // GET: api/directors/1/movies
        [HttpGet("{directorId}/movies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByDirector(int directorId)
        {
            var director = await _context.Directors.FindAsync(directorId);

            if (director == null)
            {
                return NotFound("Director not found");
            }

            var movies = await _context.Movies
                .Where(m => m.DirectorId == directorId)
                .ToListAsync();

            return movies;
        }
    }
}