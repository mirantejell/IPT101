namespace LibraryDomain.Models;

public class LibraryRecord
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string CourseYear { get; set; } = string.Empty;
    public string BookTitle { get; set; } = string.Empty;
}
