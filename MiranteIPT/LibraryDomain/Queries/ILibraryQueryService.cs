using LibraryDomain.Models;

namespace LibraryDomain.Queries;

public interface ILibraryQueryService
{
    Task<IEnumerable<LibraryRecord>> GetAllRecordsAsync();
    Task<IEnumerable<LibraryRecord>> SearchRecordsAsync(string searchTerm);
    Task<LibraryRecord?> GetRecordByIdAsync(int id);
}
