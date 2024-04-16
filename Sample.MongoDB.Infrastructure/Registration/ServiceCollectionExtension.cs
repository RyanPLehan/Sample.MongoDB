using System;
using Microsoft.Extensions.DependencyInjection;
using Sample.MongoDB.Domain.Infrastructure;
using IInformational = Sample.MongoDB.Domain.Infrastructure.Informational;
using CInformational = Sample.MongoDB.Infrastructure.Repositories.Informational;
using Sample.MongoDB.Infrastructure.Configue;
using Sample.MongoDB.Domain.Infrastructure.Informational.Server;

namespace Sample.MongoDB.Infrastructure.Registration
{

    public static class ServiceCollectionExtension
    {


        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Caching
            services.AddMemoryCache();

            // Register MongoDB Mapping files
            RegisterMongoDBMapping.Scan();          // Use this as an automated means of registering.  Otherwise manually regsiter below
            // new DatabaseConfig().Register();


            // Informational
            services.AddSingleton<IServerRepository, CInformational.ServerRepository>();

            /*
            // Management
            services.AddSingleton<IManagement.ICypherTextRepository, CManagement.CypherTextRepository>();
            services.AddSingleton<IManagement.IDecypherTextRepository, CManagement.DecypherTextRepository>();
            services.AddSingleton<IManagement.ICategoryTypeRepository, CManagement.CategoryTypeRepository>();
            services.AddSingleton<IManagement.IWordRepository, CManagement.WordRepository>();
            services.AddSingleton<IManagement.IWordPartRepository, CManagement.WordPartRepository>();
            services.AddSingleton<IManagement.IWordPartReverseRepository, CManagement.WordPartReverseRepository>();

            // Search
            services.AddSingleton<ISearch.IWordRepository, CSearch.WordRepository>();
            services.AddSingleton<ISearch.IWordPartRepository, CSearch.WordPartRepository>();
            services.AddSingleton<ISearch.IWordPartReverseRepository, CSearch.WordPartReverseRepository>();

            // Search
            services.AddSingleton<IStatistics.IWordRepository, CStatistics.WordRepository>();
            services.AddSingleton<IStatistics.IWordPartRepository, CStatistics.WordPartRepository>();
            services.AddSingleton<IStatistics.IWordPartReverseRepository, CStatistics.WordPartReverseRepository>();
            */

            return services;
        }
    }
}
