using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Sample.MongoDB.Domain.Informational.Server.Requests;

namespace Sample.MongoDB.Domain.Informational.Server.Handlers
{
    /// <summary>
    /// Handler for requesting to get list of databases names only
    /// </summary>
    /// <remarks>
    /// Handler portion of the Mediator pattern
    /// </remarks>
    internal class GetDatabaseNamesHandler : IRequestHandler<GetDatabaseNames, IEnumerable<string>>
    {
        private readonly IServerRepository _repository;

        public GetDatabaseNamesHandler(IServerRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository, nameof(repository));

            _repository = repository;
        }

        public async Task<IEnumerable<string>> Handle(GetDatabaseNames request, CancellationToken cancellationToken)
        {
            return await _repository.GetDatabaseNames();
        }
    }
}
