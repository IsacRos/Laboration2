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
    public class BorrowerController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Borrower>>> GetBorrower()
        {
            using var context = new BookDbContext();
            var b = await context.Borrowers.AsNoTracking().Select(x => new BorrowerDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Ssn = x.Ssn
            }).ToListAsync();
            return Ok(b);
        }

        [HttpPost]
        public async Task<ActionResult<Borrower>> AddBorrower (BorrowerRequest request)
        {
            using var context = new BookDbContext();
            var borrower = new Borrower()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Ssn = request.Ssn
            };
            context.Add(borrower);
            await context.SaveChangesAsync();
            return Ok(borrower);
        }
        [HttpDelete]
        public async Task<ActionResult<Borrower>> DeleteBorrower(int id)
        {
            using var context = new BookDbContext();
            var borrower = context.Borrowers.Find(id);
            if (borrower is not null)
            {
                context.Borrowers.Remove(borrower);
                await context.SaveChangesAsync();
                return Ok($"{borrower.FirstName} {borrower.LastName} was deleted from the system.");
            }
            return NotFound();
        }
    }
}
