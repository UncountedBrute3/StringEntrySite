using Microsoft.EntityFrameworkCore;
using StringEntrySite.Infrastructure.Entities;

namespace StringEntrySite.Infrastructure.Contexts;

public class StringEntryDbContext : DbContext
{
    public DbSet<StringEntry> StringEntries { get; set; }
    
    public StringEntryDbContext(DbContextOptions<StringEntryDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StringEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired();
            entity.Property(e => e.Value).HasMaxLength(500).IsRequired();
        });
    }
}