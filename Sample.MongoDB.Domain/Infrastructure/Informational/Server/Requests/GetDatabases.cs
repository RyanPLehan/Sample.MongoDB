using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Sample.MongoDB.Domain.Models.Informational;

namespace Sample.MongoDB.Domain.Infrastructure.Informational.Server.Requests
{
    /// <summary>
    /// Request to get list of databases and their specific information
    /// </summary>
    /// <remarks>
    /// Request  portion of the Mediator pattern
    /// </remarks>
    public class GetDatabases : IRequest<IEnumerable<Database>>
    {
    }
}
