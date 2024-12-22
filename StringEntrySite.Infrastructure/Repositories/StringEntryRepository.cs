using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StringEntrySite.Infrastructure.Repositories.Interfaces;
using StringEntrySite.Infrastructure.Contexts;
using StringEntrySite.Infrastructure.Entities;

namespace StringEntrySite.Infrastructure.Repositories;

public class StringEntryRepository : IStringEntryRepository
{
    private readonly StringEntryDbContext _context;
    private readonly ILogger<StringEntryRepository> _logger;

    public StringEntryRepository(StringEntryDbContext context, ILogger<StringEntryRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IReadOnlyCollection<string>> Contains(string input) =>
        await _context.StringEntries
            .Where(s => s.Value.ToLower().Contains(input.ToLower()))
            .Select(s => s.Value)
            .ToArrayAsync();

    public async Task<(bool IsAdded, bool AlreadyExists)> Add(string input)
    {
        if (await _context.StringEntries.AnyAsync(s => s.Value == input))
        {
            return (false, true);
        }
        
        _logger.LogInformation("No value for {input} exists. Adding string entry.", input);
        _context.StringEntries.Add(new StringEntry()
        {
            Value = input
        });
        int rowsAdded = await _context.SaveChangesAsync();
        if (rowsAdded < 1)
        {
            _logger.LogError("Failed to add string entry for {input}.", input);
            return (false, false);
        }

        if (rowsAdded > 1)
        {
            _logger.LogWarning("Added more than one row for {input}.", input);
        }

        return (true, false);
    }
}