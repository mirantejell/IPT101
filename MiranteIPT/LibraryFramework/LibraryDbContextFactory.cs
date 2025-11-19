using Microsoft.EntityFrameworkCore;

namespace LibraryFramework;

public class LibraryDbContextFactory
{
    private readonly string _connectionString;

    public LibraryDbContextFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public LibraryDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();
        optionsBuilder.UseSqlServer(_connectionString);
        return new LibraryDbContext(optionsBuilder.Options);
    }
}
