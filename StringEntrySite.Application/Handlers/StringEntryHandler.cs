using Microsoft.Extensions.Logging;
using StringEntrySite.Application.Handlers.Interfaces;
using StringEntrySite.Application.Models;
using StringEntrySite.Application.Strategies.Interfaces;
using StringEntrySite.Application.Validators.Interfaces;
using StringEntrySite.Infrastructure.Repositories.Interfaces;

namespace StringEntrySite.Application.Handlers;

public class StringEntryHandler : IRequestHandler<string, ProcessResponse<string>>
{
    private readonly IValidator<string> _validator;
    private readonly ISplitStrategy<string> _splitStrategy;
    private readonly IStringEntryRepository _repository;
    private readonly ILogger<StringEntryHandler> _logger;

    public StringEntryHandler(IValidator<string> validator, ISplitStrategy<string> splitStrategy, IStringEntryRepository repository, ILogger<StringEntryHandler> logger)
    {
        _validator = validator;
        _splitStrategy = splitStrategy;
        _repository = repository;
        _logger = logger;
    }

    public async Task<ProcessResponse<string>> Handle(string input)
    {
        // Split string into words.
        IReadOnlyCollection<string> words = _splitStrategy.Split(input);
        
        // Validate each word.
        HashSet<string> errors = [];
        List<string> validWords = [];
        foreach (var word in words)
        {
            (bool isValid, IReadOnlyCollection<string>? loopErrors) = _validator.IsValid(word);
            if (!isValid)
            {
                if (loopErrors is not null)
                {
                    foreach (var error in loopErrors)
                    {
                        errors.Add(error);
                    }
                }
            }
            else
            {
                validWords.Add(word);
            }
        }

        if (validWords.Count == 0)
        {
            return ProcessResponse<string>.Failure(["No valid words found.", ..errors]);
        }
        
        // From valid words, get longest.
        int maxLength = validWords.Max(word => word.Length);
        IReadOnlyCollection<string> longestWords = validWords.Where(word => word.Length == maxLength).ToArray().AsReadOnly();

        // Add input to the repository.
        foreach (string word in longestWords)
        {
            (bool isAdded, bool alreadyExists) = await _repository.Add(word);
            if (alreadyExists)
            {
                _logger.LogInformation("Value {word} already exists in the repository, skipping.", word);
            }

            if (isAdded)
            {
                return ProcessResponse<string>.Success(word);
            }
        }
        return ProcessResponse<string>.Failure(["Failed to add a valid word."]);
    }
}