using System.Net;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.MongoDB.Domain.Infrastructure.Informational.Server;
using Sample.MongoDB.Domain.Models.Informational;
using Sample.MongoDB.Infrastructure.Options;
using Sample.MongoDB.Infrastructure.Registration;
using Sample.MongoDB.Infrastructure.Repositories.Informational;

namespace Sample.MongoDB.Console
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHostBuilder hostBuilder = CreateHostBuilder(args);
            await hostBuilder.RunConsoleAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ApplicationSettings.LoadSettingsFile)
                // Need to run as Windows Service, not just console app. (Microsoft.Extensions.Hosting.WindowsServices)
                //.UseWindowsService(ConfigureWindowService)
                .ConfigureLogging(ConfigureLogger)
                // Use serilog as the logging provider (Serilog.Extensions.Hosting)
                //.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration))
                .ConfigureServices(ConfigureOptions)
                .ConfigureServices(ConfigureGeneralServices)
                .ConfigureServices(ConfigureServices)
                .UseConsoleLifetime();                          // This is needed to initiate the termination of the app, within the app itself

            return hostBuilder;
        }


        private static void ConfigureLogger(HostBuilderContext context, ILoggingBuilder builder)
        {
            // Remove MS Logger
            builder.ClearProviders();

            builder.AddConsole();
            builder.AddDebug();
        }

        private static void ConfigureOptions(HostBuilderContext context, IServiceCollection services)
        {
            services.Configure<DatabaseSettings>(context.Configuration.GetSection("Database"));
        }

        private static void ConfigureGeneralServices(HostBuilderContext context, IServiceCollection services)
        {
            // Caching
            services.AddMemoryCache();

            // Get list of assemblies that has our classes needed for AutoMapper, FluentValidation, and MediatR
            Assembly[] assemblies = ApplicationAssembly.GetSelectedAssemblies();

            // Automapper
            //services.AddAutoMapper(assemblies);

            // FluentValidation
            // Add all public Validators automatically.  Classes scoped as internal should not be added
            //services.AddValidatorsFromAssemblies(assemblies, ServiceLifetime.Transient);

            // configure MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }



        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // Infrastructure Setup
            services.AddInfrastructure();

            // Workers
            services.AddHostedService<ConsoleWorker>();
        }

    }
}
