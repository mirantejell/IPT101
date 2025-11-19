using LibraryDomain.Models;

namespace LibraryDomain.Commands;

public interface ILibraryCommandService
{
    Task<LibraryRecord> AddRecordAsync(string fullName, string courseYear, string bookTitle);
    Task UpdateRecordAsync(int id, string fullName, string courseYear, string bookTitle);
    Task DeleteRecordAsync(int id);
}
