using System.Text.Json.Serialization;

namespace StringEntrySite.Models;

public record DataSubmissionRequest(
    [property: JsonPropertyName("sentence")]
    string Sentence
);