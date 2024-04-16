using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using Sample.MongoDB.Domain.Configuration;


namespace Sample.MongoDB.Console
{
    internal static class ApplicationSettings
    {
        public static void AddUserSecrets(IConfigurationBuilder builder)
        {
            builder.AddUserSecrets(Assembly.GetEntryAssembly(), true, true);
        }

        public static bool IsLocalOrDevEnvironment(HostBuilderContext context)
        {
            string appEnv = EnvironmentSettings.ApplicationEnvironment();

            // See following reference for incorporating user secrets
            // https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows#register-the-user-secrets-configuration-source
            // https://blog.elmah.io/asp-net-core-not-that-secret-user-secrets-explained/

            return context.HostingEnvironment.IsDevelopment() ||
                   appEnv.Equals("Local", StringComparison.OrdinalIgnoreCase);
        }

        public static void LoadSettingsFile(IConfigurationBuilder builder)
        {
            LoadSettingsFile(builder, null);
        }


        public static void LoadSettingsFile(HostBuilderContext context, IConfigurationBuilder builder)
        {
            LoadSettingsFile(builder);
            if (IsLocalOrDevEnvironment(context))
                AddUserSecrets(builder);
        }

        public static void LoadSettingsFile(IConfigurationBuilder builder, string? additionalSettingsFile)
        {
            string appEnv = EnvironmentSettings.ApplicationEnvironment();

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var file = $"appsettings.json";
            builder.AddJsonFile(Path.Combine(path, file), true, true);

            file = $"appsettings.Logging.json";
            builder.AddJsonFile(Path.Combine(path, file), true, true);

            file = $"appsettings.Database.json";
            builder.AddJsonFile(Path.Combine(path, file), true, true);


            if (!String.IsNullOrWhiteSpace(appEnv))
            {
                file = $"appsettings.{appEnv}.json";
                builder.AddJsonFile(Path.Combine(path, file), true, true);
            }

            if (!String.IsNullOrWhiteSpace(additionalSettingsFile))
            {
                builder.AddJsonFile(Path.Combine(path, additionalSettingsFile.Trim()), true, true);
            }
        }
    }
}
