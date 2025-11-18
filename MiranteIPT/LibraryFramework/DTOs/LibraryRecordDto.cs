namespace LibraryFramework.DTOs;

public class LibraryRecordDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string CourseYear { get; set; } = string.Empty;
    public string BookTitle { get; set; } = string.Empty;
}
