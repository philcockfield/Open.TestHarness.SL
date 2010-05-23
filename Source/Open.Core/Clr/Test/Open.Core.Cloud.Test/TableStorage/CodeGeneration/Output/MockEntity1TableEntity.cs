// ---------------------------------------------
//   Generated code.  
//   Changes will be overwritten next time this code is generated.
//   Created: 05/22/2010 23:22:38
// ---------------------------------------------
using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsAzure.StorageClient;
using Open.Core.Cloud.TableStorage;

namespace Open.Core.Cloud.Test.TableStorage.CodeGeneration.Generated
{
    /// <summary>Backing entity used to persist values on the model 'MockEntity1' to TableStorage.</summary>
    [Export(typeof(IMockEntity1TableEntity))]
	public class MockEntity1TableEntity : TableServiceEntity, IMockEntity1TableEntity
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

	public interface IMockEntity1TableEntity
	{
        System.String Text { get; set; }
        Int32 Number { get; set; }
	}

	public class MockEntity1Context : TableServiceContextBase<MockEntity1TableEntity>
	{
	}
}