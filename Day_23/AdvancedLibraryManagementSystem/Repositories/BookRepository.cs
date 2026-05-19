using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Interfaces;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooksWithDetails()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToListAsync();
        }
    }
}