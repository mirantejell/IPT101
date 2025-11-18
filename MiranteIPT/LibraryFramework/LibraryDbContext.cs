using LibraryDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryFramework;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }

    public DbSet<LibraryRecord> LibraryRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LibraryRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CourseYear).IsRequired().HasMaxLength(100);
            entity.Property(e => e.BookTitle).IsRequired().HasMaxLength(300);
        });
    }
}
