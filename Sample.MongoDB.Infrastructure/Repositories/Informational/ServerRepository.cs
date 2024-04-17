using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Sample.MongoDB.Domain.Models.Informational;
using Sample.MongoDB.Infrastructure.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Core.Clusters;
using Sample.MongoDB.Infrastructure.Options;
using Sample.MongoDB.Domain.Informational.Server;

namespace Sample.MongoDB.Infrastructure.Repositories.Informational
{
    /// <summary>
    /// Implementation of the Repository Pattern
    /// </summary>
    public class ServerRepository : IServerRepository
    {
        private readonly IMongoClient _client = null;
        private readonly DatabaseSettings _settings;

        public ServerRepository(IOptions<DatabaseSettings> settings)
        {
            ArgumentNullException.ThrowIfNull(settings, nameof(settings));

            _settings = settings.Value;
            _client = ClientManagement.CreateClient(_settings.QueryConnectionString);
        }

        public async Task<IEnumerable<Database>> GetDatabases()
        {            
            IEnumerable<BsonDocument> dbs = await _client.ListDatabases().ToListAsync();
            return dbs.Select(x => BsonSerializer.Deserialize<Database>(x))
                      .ToList();
        }


        public async Task<IEnumerable<string>> GetDatabaseNames()
        {
            return await _client.ListDatabaseNamesAsync()
                                .Result
                                .ToListAsync();
        }
    }
}
