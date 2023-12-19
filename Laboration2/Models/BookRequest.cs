namespace Laboration2.Models;

public class BookRequest
{
    public string Title { get; set; } = string.Empty;
    public long Isbn { get; set; }
    public int ReleaseYear { get; set; }
}
