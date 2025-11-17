using LibraryDomain.Models;
using LibraryDomain.Queries;
using Microsoft.EntityFrameworkCore;

namespace LibraryFramework.Queries;

public class LibraryQueryService : ILibraryQueryService
{
    private readonly LibraryDbContextFactory _contextFactory;

    public LibraryQueryService(LibraryDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<LibraryRecord>> GetAllRecordsAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.LibraryRecords.ToListAsync();
    }

    public async Task<IEnumerable<LibraryRecord>> SearchRecordsAsync(string searchTerm)
    {
        using var context = _contextFactory.CreateDbContext();
        
        var lowerSearchTerm = searchTerm.ToLower();
        
        return await context.LibraryRecords
            .Where(r => r.FullName.ToLower().Contains(lowerSearchTerm) ||
                       r.CourseYear.ToLower().Contains(lowerSearchTerm) ||
                       r.BookTitle.ToLower().Contains(lowerSearchTerm))
            .ToListAsync();
    }

    public async Task<LibraryRecord?> GetRecordByIdAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.LibraryRecords.FindAsync(id);
    }
}
