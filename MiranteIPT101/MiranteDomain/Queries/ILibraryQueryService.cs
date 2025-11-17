using MiranteDomain.Models;

namespace MiranteDomain.Queries;

public interface ILibraryQueryService
{
    Task<IEnumerable<LibraryRecord>> GetAllLibraryRecords();
    Task<LibraryRecord?> GetLibraryRecordById(int id);
    Task<IEnumerable<LibraryRecord>> SearchLibraryRecords(string searchTerm);
}
