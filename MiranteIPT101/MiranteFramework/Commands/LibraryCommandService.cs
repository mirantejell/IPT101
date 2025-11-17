using MiranteDomain.Commands;
using MiranteDomain.Models;

namespace MiranteFramework.Commands;

public class LibraryCommandService : ILibraryCommandService
{
    private readonly LibraryDbContext _context;

    public LibraryCommandService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<LibraryRecord> CreateLibraryRecord(LibraryRecord record)
    {
        _context.LibraryRecords.Add(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task<LibraryRecord> UpdateLibraryRecord(LibraryRecord record)
    {
        _context.LibraryRecords.Update(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task DeleteLibraryRecord(int id)
    {
        var record = await _context.LibraryRecords.FindAsync(id);
        if (record != null)
        {
            _context.LibraryRecords.Remove(record);
            await _context.SaveChangesAsync();
        }
    }
}
