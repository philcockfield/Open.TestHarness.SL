using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Open.Core.Cloud
{
    /// <summary>Exportable cloud settings.</summary>
    public interface ICloudSettings
    {
        /// <summary>Gets the connection string to the data storage system.</summary>
         string DataConnectionString { get; }
    }
}
