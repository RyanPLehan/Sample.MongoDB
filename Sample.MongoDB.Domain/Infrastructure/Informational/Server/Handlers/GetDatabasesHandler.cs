using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Sample.MongoDB.Domain.Infrastructure.Informational.Server.Requests;
using Sample.MongoDB.Domain.Models.Informational;

namespace Sample.MongoDB.Domain.Infrastructure.Informational.Server.Handlers
{
    /// <summary>
    /// Handler for requesting to get list of databases and relative information of each database
    /// </summary>
    /// <remarks>
    /// Handler portion of the Mediator pattern
    /// </remarks>
    internal class GetDatabasesHandler : IRequestHandler<GetDatabases, IEnumerable<Database>>
    {
        private readonly IServerRepository _repository;

        public GetDatabasesHandler(IServerRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));

            _repository = repository;
        }

        public async Task<IEnumerable<Database>> Handle(GetDatabases request, CancellationToken cancellationToken)
        {
            return await _repository.GetDatabases();
        }
    }
}
