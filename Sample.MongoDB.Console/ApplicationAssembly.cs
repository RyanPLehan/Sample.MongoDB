using System;
using System.Linq;
using System.Reflection;



namespace Sample.MongoDB.Console
{
    internal static class ApplicationAssembly
    {
        /// <summary>
        /// Retrieve all assemblies, including assemblies from nuget packages
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        /// <summary>
        /// Retrieve assemblies that are referenced only
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetSelectedAssemblies()
        {
            return new Assembly[]
                    {
                        typeof(Sample.MongoDB.Console.ApplicationAssembly).Assembly,
                        typeof(Sample.MongoDB.Domain.Configuration.EnvironmentSettings).Assembly,
                        typeof(Sample.MongoDB.Infrastructure.Registration.ServiceCollectionExtension).Assembly,
                    };
        }
    }
}
