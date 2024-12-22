namespace StringEntrySite.Infrastructure.Repositories.Interfaces;

public interface IStringEntryRepository
{
    Task<IReadOnlyCollection<string>> Contains(string input);
    Task<(bool IsAdded, bool AlreadyExists)> Add(string input);
}