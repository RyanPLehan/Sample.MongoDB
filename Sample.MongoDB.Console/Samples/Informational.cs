using MediatR;
using Sample.MongoDB.Domain.Infrastructure.Informational.Server.Requests;
using Sample.MongoDB.Domain.Models.Informational;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.MongoDB.Console.Samples
{
    internal static class Informational
    {
        public static async Task DisplayServerInformation(IMediator mediator)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("The following will display server and database information.  Please hit the <Enter> key to continue.");

            System.Console.WriteLine();
            System.Console.WriteLine("*** Displaying Server Information ***");

            System.Console.WriteLine();
            System.Console.WriteLine("Database Names:");
            await DisplayDatabaseNames(mediator);

            System.Console.WriteLine();
            System.Console.WriteLine("Database Details:");
            await DisplayDatabases(mediator);
        }

        private static async Task DisplayDatabases(IMediator mediator)
        {
            // Use mediator pattern to request to get databases
            var dbs = await mediator.Send(new GetDatabases());
            foreach (Database db in dbs)
                System.Console.WriteLine("Name: {0}.  Current Size: {1}.  Is Empty: {2}", db.Name, db.Size, db.IsEmpty);
        }

        private static async Task DisplayDatabaseNames(IMediator mediator)
        {
            // Use mediator pattern to request to get databases
            var names = await mediator.Send(new GetDatabaseNames());
            foreach (string name in names)
                System.Console.WriteLine("Name: {0}", name);
        }

    }
}
