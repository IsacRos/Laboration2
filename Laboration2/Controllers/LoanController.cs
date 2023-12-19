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
    public class LoanController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            using var context = new BookDbContext();
            var book = await context.Books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Isbn = b.Isbn,
                ReleaseYear = b.ReleaseYear
            }).ToListAsync();
            return Ok(book);
        }
        [HttpPost]
        public async Task AddBook(BookRequest request)
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
        }
    }
}
