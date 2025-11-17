using MiranteDomain.Models;

namespace MiranteDomain.Commands;

public interface ILibraryCommandService
{
    Task<LibraryRecord> CreateLibraryRecord(LibraryRecord record);
    Task<LibraryRecord> UpdateLibraryRecord(LibraryRecord record);
    Task DeleteLibraryRecord(int id);
}
