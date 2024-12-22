using Microsoft.Extensions.Logging;
using StringEntrySite.Application.Handlers.Interfaces;
using StringEntrySite.Application.Models;
using StringEntrySite.Infrastructure.Repositories.Interfaces;

namespace StringEntrySite.Application.Handlers;

public class WordSearchHandler : IRequestHandler<string, ProcessResponse<IReadOnlyCollection<string>>>
{
    private readonly IStringEntryRepository _repository;

    public WordSearchHandler(IStringEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProcessResponse<IReadOnlyCollection<string>>> Handle(string input)
    {
        // If I had more time, I'd look at something like Lucene.net to enable search engine based queries.
        IReadOnlyCollection<string> words = await _repository.Contains(input);
        return ProcessResponse<IReadOnlyCollection<string>>.Success(words);
    }
}