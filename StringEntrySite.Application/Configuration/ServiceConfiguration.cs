using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StringEntrySite.Application.Handlers;
using StringEntrySite.Application.Handlers.Interfaces;
using StringEntrySite.Application.Models;
using StringEntrySite.Application.Strategies;
using StringEntrySite.Application.Strategies.Interfaces;
using StringEntrySite.Application.Validators;
using StringEntrySite.Application.Validators.Interfaces;
using StringEntrySite.Infrastructure.Configuration;

namespace StringEntrySite.Application.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddScoped<IRequestHandler<string, ProcessResponse<string>>, StringEntryHandler>();
        services.AddScoped<IRequestHandler<string, ProcessResponse<IReadOnlyCollection<string>>>, WordSearchHandler>();
        services.AddSingleton<ISplitStrategy<string>, SpaceSplitStringStrategy>();
        services.AddSingleton<IValidator<string>, SubmittedStringValidator>();
        return services;
    }
}