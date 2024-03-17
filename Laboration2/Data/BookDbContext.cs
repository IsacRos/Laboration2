using Laboration2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Laboration2.Data;

public class BookDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Borrower> Borrowers { get; set; }
    public DbSet<Loan> Loans { get; set; }

    public BookDbContext()
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*        optionsBuilder.UseInMemoryDatabase("DbNameNotEmpty");*/
        optionsBuilder.UseSqlServer();
    }
}
