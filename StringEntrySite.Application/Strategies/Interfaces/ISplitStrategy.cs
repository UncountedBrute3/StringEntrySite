namespace StringEntrySite.Application.Strategies.Interfaces;

public interface ISplitStrategy<T>
{
    public IReadOnlyCollection<T> Split(T input);
}