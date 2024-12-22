namespace StringEntrySite.Application.Handlers.Interfaces;

public interface IRequestHandler<TIn, TOut>
{
    Task<TOut> Handle(TIn input);
}