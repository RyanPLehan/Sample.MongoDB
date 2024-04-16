using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Reflection;


namespace Sample.MongoDB.Infrastructure.Registration
{
    /// <summary>
    /// This is used to scan for the MetadataTypeAttribute and add the class to the provider transport
    /// </summary>
    /// <remarks>
    /// This would be used to assist with using Bson attributes as a means to map models to mongodb entities
    /// </remarks>
    internal static class MetadataScan
    {
        private readonly static ConcurrentDictionary<Assembly, bool> ScannedAssemblies = new();

        public static void Scan()
        {
            Scan(Assembly.GetExecutingAssembly());
        }

        public static void Scan(Assembly assembly, params Assembly[] additionalAssemblies)
        {
            ArgumentNullException.ThrowIfNull(assembly);
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
            foreach (Type currentClass in assembly.GetTypes().Where(x => x.IsClass))
            {
                var metaAttributes = currentClass.GetCustomAttributes<MetadataTypeAttribute>(inherit: false);
                foreach (var metaAttribute in metaAttributes)
                    RegisterMetadataClass(currentClass, metaAttribute);
            }
        }

        private static void RegisterMetadataClass(Type currentClass, MetadataTypeAttribute metaAttribute)
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(currentClass, metaAttribute.MetadataClassType), currentClass);
        }
    }
}
