using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Sample.MongoDB.Domain.Models.Informational;

namespace Sample.MongoDB.Infrastructure.Configue
{
    /// <summary>
    /// This class is designed to register mapping between the Model and the Bson object.
    /// </summary>
    /// <remarks>
    /// Think of this as the configuration file for Entity Framework
    /// </remarks>
    internal class DatabaseConfig : IRegisterClassMap
    {        
        public void Register()
        {
            BsonClassMap.RegisterClassMap<Database>(map =>
            {
                map.AutoMap();              // Auto Map if property names already match (case sensitive) to the bson property names

                map.MapMember(p => p.Name)
                   .SetElementName("name")
                   .SetIsRequired(true);

                map.MapMember(p => p.Size)
                   .SetElementName("sizeOnDisk")
                   .SetIsRequired(true);

                map.MapMember(p => p.IsEmpty)
                   .SetElementName("empty")
                   .SetIsRequired(true);
            });
        }
    }
}
