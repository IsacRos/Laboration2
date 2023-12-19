using Laboration2.Entities;

namespace Laboration2.Models
{
    public class LoanDto
    {
        public int Id { get; set; }
        public DateTime? LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public string Borrower { get; set; } = string.Empty;
        public string Book { get; set; } = string.Empty;

    }
}
