using System;
using System.Collections.Concurrent;
using MongoDB.Driver;

namespace Sample.MongoDB.Infrastructure.Repositories
{
    internal static class ClientManagement
    {
        private static ConcurrentDictionary<string, IMongoClient> _clients = new ConcurrentDictionary<string, IMongoClient>();

        public static IMongoClient CreateClient(string connectionString)
        {
            // Ensure that we create a single client per connection string
            // The MongoDB client will internally maintain a list of connections with connection pools
            return _clients.GetOrAdd(connectionString, s => new MongoClient(s));
        }
    }
}
