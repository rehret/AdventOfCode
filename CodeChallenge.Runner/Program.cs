namespace CodeChallenge.Runner;

using System.Collections.Immutable;
using System.Reflection;

using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;

using CodeChallenge.Core.Configuration;
using CodeChallenge.Runner.Helpers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var serviceProvider = BuildServiceProvider(args);
        await using var _ = serviceProvider.ConfigureAwait(false);
        var service = serviceProvider.GetRequiredService<ICodeChallengeService>();
        await service.ExecuteAsync(CancellationToken.None).ConfigureAwait(false);
    }

    private static AutofacServiceProvider BuildServiceProvider(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        var serviceCollection = new ServiceCollection()
            .AddOptions()
            .ConfigureOptionsInReferencedAssemblies(config)
            .AddLogging(cfg => cfg.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = false;
            }))
            .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Information);

        var builder = new ContainerBuilder();
        builder.Populate(serviceCollection);
        builder.RegisterModule(new ConfigurationModule(config));
        builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

        var container = builder.Build();
        return new AutofacServiceProvider(container);
    }

    private static IServiceCollection ConfigureOptionsInReferencedAssemblies(this IServiceCollection serviceCollection, IConfiguration config)
    {
        var configurationTypes = AssemblyHelpers.GetReferencedAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsAssignableTo<ICodeChallengeConfiguration>())
            .Where(x => x.IsClass && !x.IsAbstract)
            .ToImmutableList();

        foreach (var type in configurationTypes)
        {
            var configurationSection = config.GetSection(type.Name);

            if (configurationSection.Exists())
            {
                var configureMethod = typeof(OptionsConfigurationServiceCollectionExtensions)
                    .GetMethods()
                    .Where(x => x.Name == "Configure")
                    .Single(method => method.GetParameters().Length == 2)
                    .MakeGenericMethod(type);

                configureMethod.Invoke(null, new object[] { serviceCollection, configurationSection });
            }
        }

        return serviceCollection;
    }
}