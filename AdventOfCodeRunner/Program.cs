namespace AdventOfCodeRunner;

using System.Reflection;

using AdventOfCodeRunner.IoC;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public static class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).RunConsoleAsync().ConfigureAwait(false);
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddHostedService<AdventOfCodeService>();
                services.AddAutofac(builder => builder.RegisterModule<AdventOfCodeRunnerModule>());
                services.AddLogging(builder =>
                {
                    builder.AddFilter("Microsoft", LogLevel.Warning);
                });
            })
            .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly()))
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}