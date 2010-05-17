// ---------------------------------------------
//   Generated code.  
//   Changes will be overwritten next time this code is generated.
//   Created: 05/17/2010 13:29:52
// ---------------------------------------------
using System;
using Microsoft.WindowsAzure.StorageClient;

namespace Open.Core.Cloud.Test.TableStorage.CodeGeneration.Generated
{
    /// <summary>Backing entity used to persist values on the model 'MockEntity1' to TableStorage.</summary>
    public class MockEntity1TableEntity : TableServiceEntity
    {
        #region Head
        // Constructors.
        public MockEntity1TableEntity(string partitionKey, string rowKey) : base(partitionKey, rowKey) { }
        public MockEntity1TableEntity() : this(Guid.NewGuid().ToString(), String.Empty) { }
        #endregion

        #region Properties
        public System.String Text { get; set; }
        public Int32 Number { get; set; }
        #endregion
    }
}