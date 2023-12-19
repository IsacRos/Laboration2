namespace Laboration2.Entities;

public class Loan
{
    public int Id { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public int BookId { get; set; }
    public int BorrowerId { get; set; }

    public Borrower Borrower { get; set; } = null!;
    public Book Book { get; set; } = null!;
}
