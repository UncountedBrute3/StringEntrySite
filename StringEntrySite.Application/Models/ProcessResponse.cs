namespace StringEntrySite.Application.Models;

public record ProcessResponse<T>(T? Value, IReadOnlyCollection<string>? Errors = null)
{
    public bool IsSuccess => Errors is null || Errors.Count == 0;
    
    public static ProcessResponse<T> Success(T value) => new(value);
    public static ProcessResponse<T> Failure(IReadOnlyCollection<string> errors) => new(default, errors);
}