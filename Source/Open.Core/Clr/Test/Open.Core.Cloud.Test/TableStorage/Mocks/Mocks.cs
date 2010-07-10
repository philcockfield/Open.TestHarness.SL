using System;
using Open.Core.Cloud.TableStorage;
using Open.Core.Cloud.Test.TableStorage.CodeGeneration.Generated;

namespace Open.Core.Cloud.Test.TableStorage.CodeGeneration
{
    [PersistClass(TableName = "MyCustomTableName")]
    public class MockModelA
    {
        #region Head
        public MockModelA() { }
        public MockModelA(MockModelATableEntity backingEntity)
        {
            Property = new TablePropertyManager<MockModelA, MockModelATableEntity>(backingEntity);
        }
        #endregion

        #region Properties
        public TablePropertyManager<MockModelA, MockModelATableEntity> Property { get; private set; }

        [PersistProperty(IsRowKey = true)]
        public string Id
        {
            get { return Property.GetValue<string>(m => m.Id); }
            set { Property.SetValue(m => m.Id, value); }
        }

        [PersistProperty(IsPartitonKey = true)]
        public string Partition
        {
            get { return Property.GetValue<string>(m => m.Partition); }
            set { Property.SetValue(m => m.Partition, value); }
        }

        [PersistProperty]
        public string Text
        {
            get { return Property.GetValue<string>(m => m.Text); }
            set { Property.SetValue(m => m.Text, value); }
        }

        [PersistProperty]
        public int Number
        {
            get { return Property.GetValue<int>(m => m.Number); }
            set { Property.SetValue(m => m.Number, value); }
        }

        public string NotPersisted { get; set; }
        #endregion
    }

    [PersistClass]
    public class MockModelB
    {
        [PersistProperty]
        public DateTime Date { get; set; }
    }

    [PersistClass]
    public class MockModelC
    {
        [PersistProperty]
        public string PartitionKey { get; set; }

        [PersistProperty]
        public string RowKey { get; set; }
    }

    [PersistClass]
    public class NoPersistableValues
    {
        public string Text { get; set; }
    }

    public class NonPersistableClass
    {
        [PersistProperty]
        public string Text { get; set; }
    }
}
