using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StringEntrySite.Infrastructure.Contexts;
using StringEntrySite.Infrastructure.Repositories;
using StringEntrySite.Infrastructure.Repositories.Interfaces;
using StringEntrySite.Migrations.Migrations._2024._12;

namespace StringEntrySite.Infrastructure.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? dbConnectionString = configuration.GetRequiredSection("Database").Value;
        services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                // Add SQLite support to FluentMigrator
                .AddSQLite()
                // Set the connection string
                .WithGlobalConnectionString(dbConnectionString)
                // Define the assembly containing the migrations
                .ScanIn(typeof(InitialMigration).Assembly).For.Migrations())
            // Enable logging to console in the FluentMigrator way
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        var serviceProvider = services.BuildServiceProvider(false);
        
        // Instantiate the runner
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        // Execute the migrations
        runner.MigrateUp();
        
        services.AddDbContext<StringEntryDbContext>(options =>
        {
            options.UseSqlite(dbConnectionString);
        });
        
        services.AddScoped<IStringEntryRepository, StringEntryRepository>();
        return services;
    }
}