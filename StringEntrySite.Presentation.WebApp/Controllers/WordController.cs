using Microsoft.AspNetCore.Mvc;
using StringEntrySite.Application.Handlers.Interfaces;
using StringEntrySite.Application.Models;
using StringEntrySite.Models;

namespace StringEntrySite.Controllers;

[Route("words")]
public class WordController : ControllerBase
{
    private readonly IRequestHandler<string, ProcessResponse<IReadOnlyCollection<string>>> _getWordsHandler;
    private readonly IRequestHandler<string, ProcessResponse<string>> _uploadHandler;
    private readonly ILogger<WordController> _logger;
    
    public WordController(
        IRequestHandler<string, ProcessResponse<IReadOnlyCollection<string>>> getWordsHandler,
        IRequestHandler<string, ProcessResponse<string>> uploadHandler, 
        ILogger<WordController> logger
        )
    {
        _getWordsHandler = getWordsHandler;
        _uploadHandler = uploadHandler;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ProcessResponse<IReadOnlyCollection<string>>> GetWords([FromQuery] string word)
    {
        _logger.LogInformation("Received request to search for words with '{word}'.", word);
        ProcessResponse<IReadOnlyCollection<string>> response = await _getWordsHandler.Handle(word);
        return response;
    }
    
    [HttpPost]
    public async Task<ProcessResponse<string>> UploadWord([FromBody] DataSubmissionRequest request)
    {
        _logger.LogInformation("Received request to check and upload word from sentence '{sentence}'.", request.Sentence);
        ProcessResponse<string> response = await _uploadHandler.Handle(request.Sentence);
        return response;
    }
}