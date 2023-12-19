namespace Laboration2.Models;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public long Isbn { get; set; }
    public int ReleaseYear { get; set; }
}
