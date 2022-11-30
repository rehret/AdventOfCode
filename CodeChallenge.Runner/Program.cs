namespace CodeChallenge.Runner;

using System.Collections.Immutable;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Reflection;

using Autofac;
using Autofac.Configuration;
using Autofac.Core.Registration;
using Autofac.Extensions.DependencyInjection;

using CodeChallenge.Core;
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
        using var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger(typeof(Program).Namespace!);
        var parser = BuildCommandLineParser(serviceProvider, logger);
        await parser.InvokeAsync(args).ConfigureAwait(false);
    }

    private static AutofacServiceProvider BuildServiceProvider(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true)
            .AddJsonFile("appsettings.user.json", true)
            .AddEnvironmentVariables()
            .AddCommandLine(args, new Dictionary<string, string>
            {
                { "--logLevel", "Logging:LogLevel:Default" }
            })
            .Build();

        var serviceCollection = new ServiceCollection()
            .AddOptions()
            .ConfigureOptionsInReferencedAssemblies(config)
            .AddLogging(loggingBuilder => loggingBuilder.AddConfiguration(config.GetSection("Logging")).AddSimpleConsole());

        var builder = new ContainerBuilder();
        builder.Populate(serviceCollection);
        builder.RegisterModule(new ConfigurationModule(config));
        builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

        var container = builder.Build();
        return new AutofacServiceProvider(container);
    }

    private static Parser BuildCommandLineParser(IServiceProvider serviceProvider, ILogger logger)
    {
        return new CommandLineBuilder(serviceProvider.GetRequiredService<RootCommand>())
            .UseDefaults()
            .AddMiddleware(async (context, next) =>
            {
                try
                {
                    await next(context).ConfigureAwait(false);
                }
                catch (ComponentNotRegisteredException componentNotRegisteredException) when (componentNotRegisteredException.Message.Contains(nameof(ISolution)))
                {
                    logger.LogError("Solution has not been registered: {SolutionErrorMessage}", componentNotRegisteredException.Message);
                }
                catch (Exception ex) when (ex is FileNotFoundException or DirectoryNotFoundException)
                {
                    logger.LogError("Could not find input file: '{InputFile}'",
                        ex switch
                        {
                            FileNotFoundException fileNotFoundException           => fileNotFoundException.FileName,
                            DirectoryNotFoundException directoryNotFoundException => directoryNotFoundException.Message,
                            _                                                     => "Unknown error"
                        });
                }
            })
            .Build();
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
            var configurationSection = config.GetSection(type.Name.Replace("Configuration", string.Empty));

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