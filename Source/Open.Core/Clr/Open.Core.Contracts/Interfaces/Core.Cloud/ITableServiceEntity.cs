using System;

namespace Open.Core.Cloud.TableStorage
{
    /// <summary>Represents an entity in a storage table.</summary>
    public interface ITableServiceEntity
    {
        /// <summary>Gets or sets the partition key of a table entity.</summary>
        string PartitionKey { get; set; }

        /// <summary>Gets or sets the row key of a table entity.</summary>
        string RowKey { get; set; }

        /// <summary>Gets or sets the timestamp.</summary>
        DateTime Timestamp { get; set; }
    }
}
