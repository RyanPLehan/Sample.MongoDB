using System.Threading.Tasks;
using Sample.MongoDB.Domain.Models.Informational;

namespace Sample.MongoDB.Domain.Infrastructure.Informational.Server
{
    public interface IServerRepository
    {
        Task<IEnumerable<Database>> GetDatabases();
        Task<IEnumerable<string>> GetDatabaseNames();
    }
}