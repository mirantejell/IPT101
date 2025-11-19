using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MiranteFramework;

public class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
{
    public LibraryDbContext CreateDbContext(string[]? args = null)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MiranteLibraryDb;Trusted_Connection=True;");
        
        return new LibraryDbContext(optionsBuilder.Options);
    }
}
