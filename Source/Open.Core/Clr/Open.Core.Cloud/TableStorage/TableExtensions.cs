using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Open.Core.Common;

namespace Open.Core.Cloud.TableStorage
{
    public static class TableExtensions
    {
        /// <summary>Gets the cloud storate acount.</summary>
        /// <param name="settings">The current cloud settings.</param>
        public static CloudStorageAccount GetStorageAccount(this ICloudSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            return CloudStorageAccount.Parse(settings.DataConnectionString);
        }

        /// <summary>Creates an instance of the CloudTableClient using the connection string in the CloudSettings.</summary>
        /// <param name="settings">The current cloud settings.</param>
        public static CloudTableClient CreateTableClient(this ICloudSettings settings)
        {
            return settings.GetStorageAccount().CreateCloudTableClient();
        }

        /// <summary>Retrieves all the TableModelBase types within the given assembly.</summary>
        /// <param name="assembly">The assembly to look within.</param>
        public static IEnumerable<Type> GetTableModelTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            return assembly.GetTypes().Where(m => m.IsA<TableModelBase>());
        }
    }
}
