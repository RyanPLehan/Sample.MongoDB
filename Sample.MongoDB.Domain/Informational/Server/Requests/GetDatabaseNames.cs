using System;
using System.Collections.Generic;
using MediatR;

namespace Sample.MongoDB.Domain.Informational.Server.Requests
{
    /// <summary>
    /// Request to get list of database names only
    /// </summary>
    /// <remarks>
    /// Request  portion of the Mediator pattern
    /// </remarks>
    public class GetDatabaseNames : IRequest<IEnumerable<string>>
    {
    }
}
