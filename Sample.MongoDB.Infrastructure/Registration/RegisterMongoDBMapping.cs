using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Sample.MongoDB.Infrastructure.Configue;


namespace Sample.MongoDB.Infrastructure.Registration
{
    /// <summary>
    /// This is used to scan for classes that implement the IRegisterClassMap and to execute the registration process
    /// </summary>
    /// <remarks>
    /// This would be used to assist with using class mapping approach as a means to map models to mongodb entities
    /// </remarks>
    internal static class RegisterMongoDBMapping
    {
        private readonly static ConcurrentDictionary<Assembly, bool> ScannedAssemblies = new();

        public static void Scan()
        {
            Scan(Assembly.GetExecutingAssembly());
        }

        public static void Scan(Assembly assembly, params Assembly[] additionalAssemblies)
        {
            ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));
            Assembly[] allAssemblies = (additionalAssemblies ?? Array.Empty<Assembly>()).Append(assembly).ToArray();
            foreach (var currentAssembly in allAssemblies)
            {
                ScannedAssemblies.GetOrAdd(
                    key: assembly,
                    valueFactory: assembly =>
                    {
                        ScanAssembly(assembly);
                        return true;
                    });
            }
        }

        private static void ScanAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes()
                                          .Where(t => t.GetInterfaces()
                                          .Any(x => x == typeof(IRegisterClassMap))))
            {
                IRegisterClassMap mapInstance = (IRegisterClassMap) Activator.CreateInstance(type);
                mapInstance.Register();
            }
        }
    }
}
