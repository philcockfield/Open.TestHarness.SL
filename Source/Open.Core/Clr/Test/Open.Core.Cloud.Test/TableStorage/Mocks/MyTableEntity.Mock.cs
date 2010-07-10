using System;
using System.Data.Services.Common;
using Microsoft.WindowsAzure.StorageClient;
using Open.Core.Cloud.TableStorage;

namespace Open.Core.Cloud.Test.TableStorage.Mocks
{
    public class EnumToString : IConverter
    {
        public object ToTarget(object source)
        {
            if (source == null) return source;
            return source.ToString();
        }

        public object ToSource(object target)
        {
            if (target == null) return target;
            return (MyEnum)Enum.Parse(typeof(MyEnum), target.ToString(), true);
        }
    }

    public enum MyEnum
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3
    }

    public class MyTableEntityContext : TableServiceContextBase<MyTableEntity>
    {
    }


    [DataServiceEntity]
    public partial class MyTableEntity : IMyTableEntity
    {
        // Constructors.
        public MyTableEntity() : this(Guid.NewGuid().ToString(), String.Empty) { }
        public MyTableEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        // Properties
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTime Timestamp { get; set; }
        public System.String Text { get; set; }
        public Int32 MyNumber { get; set; }
        public Int32 EnumValue { get; set; }
        public string NoSetter { get { return "Foo"; } }
        public string EnumAsString { get; set; }
        public string BadConverterType { get; set; }
    }

    public partial interface IMyTableEntity : ITableServiceEntity
    {
        String Text { get; set; }
        Int32 MyNumber { get; set; }
        Int32 EnumValue { get; set; }
        string NoSetter { get; }
        string EnumAsString { get; set; }
        string BadConverterType { get; set; }
    }

    [PersistClass]
    public class MyModel
    {
        #region Head
        public MyModel(IMyTableEntity backingEntity)
        {
            Property = new TablePropertyManager<MyModel, IMyTableEntity>(backingEntity);
        }
        #endregion

        #region Properties
        public TablePropertyManager<MyModel, IMyTableEntity> Property { get; private set; }

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
        public MyEnum EnumValue
        {
            get { return Property.GetValue<MyEnum>(m => m.EnumValue); }
            set { Property.SetValue(m => m.EnumValue, value); }
        }

        [PersistProperty(MapTo = "MyNumber")]
        public int Number
        {
            get { return Property.GetValue<int>(m => m.Number); }
            set { Property.SetValue(m => m.Number, value); }
        }

        [PersistProperty]
        public string NotOnBackingEntity { get; set; }

        [PersistProperty]
        public string NoSetter { get { return "Foo"; } }

        [PersistProperty(Converter = typeof(string))]
        public string BadConverterType { get; set; }

        public string NotPersistable { get; set; }

        [PersistProperty(MapTo = "EnumAsString", Converter = typeof(EnumToString))]
        public MyEnum EnumConversion
        {
            get { return Property.GetValue<MyEnum>(m => m.EnumConversion); }
            set { Property.SetValue(m => m.EnumConversion, value); }
        }
        #endregion
    }
}