using LibraryDomain.Commands;
using LibraryDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryFramework.Commands;

public class LibraryCommandService : ILibraryCommandService
{
    private readonly LibraryDbContextFactory _contextFactory;

    public LibraryCommandService(LibraryDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<LibraryRecord> AddRecordAsync(string fullName, string courseYear, string bookTitle)
    {
        using var context = _contextFactory.CreateDbContext();
        
        var record = new LibraryRecord
        {
            FullName = fullName,
            CourseYear = courseYear,
            BookTitle = bookTitle
        };

        context.LibraryRecords.Add(record);
        await context.SaveChangesAsync();

        return record;
    }

    public async Task UpdateRecordAsync(int id, string fullName, string courseYear, string bookTitle)
    {
        using var context = _contextFactory.CreateDbContext();
        
        var record = await context.LibraryRecords.FindAsync(id);
        if (record == null)
            throw new InvalidOperationException($"Record with ID {id} not found.");

        record.FullName = fullName;
        record.CourseYear = courseYear;
        record.BookTitle = bookTitle;

        await context.SaveChangesAsync();
    }

    public async Task DeleteRecordAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        
        var record = await context.LibraryRecords.FindAsync(id);
        if (record == null)
            throw new InvalidOperationException($"Record with ID {id} not found.");

        context.LibraryRecords.Remove(record);
        await context.SaveChangesAsync();
    }
}
