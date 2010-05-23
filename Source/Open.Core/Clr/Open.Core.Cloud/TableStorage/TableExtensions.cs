using System;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

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
    }
}
