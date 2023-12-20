using Laboration2.Data;
using Laboration2.Entities;
using Laboration2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laboration2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            using var context = new BookDbContext();
            var book = await context.Books.AsNoTracking().Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Isbn = b.Isbn,
                ReleaseYear = b.ReleaseYear,
                Available = b.Available ? "Book available" : "Book not available"
            }).ToListAsync();
            return Ok(book);
        }
        [HttpGet("id")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            using var context = new BookDbContext();
            var book = await context.Books.AsNoTracking().Where(x => x.Id == id).Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Isbn = b.Isbn,
                ReleaseYear = b.ReleaseYear,
                Available = b.Available ? "Book available" : "Book not available"
            }).FirstOrDefaultAsync();
            return book is not null ? Ok(book) : NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(BookRequest request)
        {
            using var context = new BookDbContext();
            Book book = new()
            {
                Title = request.Title,
                Isbn = request.Isbn,
                ReleaseYear = request.ReleaseYear
            };
            context.Books.Add(book);
            await context.SaveChangesAsync();
            return Ok(book);
        }
        [HttpDelete]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            using var context = new BookDbContext();
            var book = context.Books.Find(id);
            if(book is not null)
            {
                context.Books.Remove(book);
                await context.SaveChangesAsync();
                return Ok($"{book.Title} was deleted");
            }
            return NotFound();
        }
    }
}
