using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.MongoDB.Domain.Models.Informational
{
    /// <summary>
    /// Model for information about a MongoDB database
    /// </summary>

    public class Database
    {
        public string Name { get; set; }
        public long Size{ get; set; }
        public bool IsEmpty { get; set; }
    }
}
