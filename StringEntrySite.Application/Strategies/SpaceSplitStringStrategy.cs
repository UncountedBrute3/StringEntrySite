using StringEntrySite.Application.Strategies.Interfaces;

namespace StringEntrySite.Application.Strategies;

public class SpaceSplitStringStrategy : ISplitStrategy<string>
{
    public IReadOnlyCollection<string> Split(string input) => input.Split(' ').AsReadOnly();
}