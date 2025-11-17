using Microsoft.EntityFrameworkCore;
using MiranteDomain.Models;
using MiranteDomain.Queries;

namespace MiranteFramework.Queries;

public class LibraryQueryService : ILibraryQueryService
{
    private readonly LibraryDbContext _context;

    public LibraryQueryService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LibraryRecord>> GetAllLibraryRecords()
    {
        return await _context.LibraryRecords.ToListAsync();
    }

    public async Task<LibraryRecord?> GetLibraryRecordById(int id)
    {
        return await _context.LibraryRecords.FindAsync(id);
    }

    public async Task<IEnumerable<LibraryRecord>> SearchLibraryRecords(string searchTerm)
    {
        return await _context.LibraryRecords
            .Where(r => r.FullName.Contains(searchTerm) || 
                       r.CourseYear.Contains(searchTerm) || 
                       r.BookTitle.Contains(searchTerm))
            .ToListAsync();
    }
}
