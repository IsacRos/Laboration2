using Laboration2.Data;
using Laboration2.Entities;
using Laboration2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Laboration2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<Loan>>> GetLoans()
        {
            using var context = new BookDbContext();
            var loan = await context.Loans.AsNoTracking().Select(x => new LoanDto()
            {
                Id = x.Id,
                LoanDate = x.LoanDate,
                ReturnDate = x.ReturnDate,
                Borrower = $"{x.Borrower.FirstName} {x.Borrower.LastName}",
                Book = x.Book.Title
            }).ToListAsync();
            return Ok(loan);
        }

        [HttpPost]
        public async Task<ActionResult> AddLoan(int bookId, int borrowerId)
        {
            using var context = new BookDbContext();
            var book = context.Books.Find(bookId);
            var borrower = context.Borrowers.Find(borrowerId);

            if (book is null) return NotFound("No book found");
            if (borrower is null) return NotFound("No borrower found");
            if (!book.Available) return NotFound("Book not available");

            Loan loan = new()
            {
                LoanDate = DateTime.Now,
                Book = book,
                Borrower = borrower
            };

            book.Available = false;
            context.Add(loan);
            await context.SaveChangesAsync();
            return Ok(loan);
        }

        [HttpPut]
        public async Task<ActionResult> ReturnLoan(int id)
        {
            using var context = new BookDbContext();
            var loan = context.Loans.Find(id);
            if (loan is null) return NotFound();
            
            var book = context.Books.Find(loan.BookId);
            if (book is null) return NotFound();
            
            book.Available = true; 
            loan.ReturnDate = DateTime.Now;
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
