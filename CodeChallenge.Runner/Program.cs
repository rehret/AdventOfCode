namespace CodeChallenge.Runner;

using System.Reflection;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class Program
{
    public static async Task Main()
    {
        var serviceProvider = BuildServiceProvider();
        await using var _ = serviceProvider.ConfigureAwait(false);
        var service = serviceProvider.GetRequiredService<ICodeChallengeService>();
        await service.ExecuteAsync(CancellationToken.None).ConfigureAwait(false);
    }

    private static AutofacServiceProvider BuildServiceProvider()
    {
        var serviceCollection = new ServiceCollection()
            .AddLogging(cfg => cfg.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = false;
            }))
            .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Information);

        var builder = new ContainerBuilder();
        builder.Populate(serviceCollection);
        builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

        var container = builder.Build();
        return new AutofacServiceProvider(container);
    }
}