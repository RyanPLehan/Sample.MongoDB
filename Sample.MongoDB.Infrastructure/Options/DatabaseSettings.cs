using System;


namespace Sample.MongoDB.Infrastructure.Options
{
    public class DatabaseSettings
    {
        /// <summary>
        /// Command Connection String.  Used for Read / Write operations
        /// </summary>
        /// <remarks>
        /// Taken from CQRS
        /// </remarks>
        public string CommandConnectionString { get; set; }

        /// <summary>
        /// Query Connection String.  Used for Read Only operations
        /// </summary>
        /// <remarks>
        /// Taken from CQRS
        /// </remarks>
        public string QueryConnectionString { get; set; }
    }
}
