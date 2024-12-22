namespace StringEntrySite.Application.Validators.Interfaces;

public interface IValidator<T>
{
    public (bool IsValid, IReadOnlyCollection<string>? Errors) IsValid(T input);
}