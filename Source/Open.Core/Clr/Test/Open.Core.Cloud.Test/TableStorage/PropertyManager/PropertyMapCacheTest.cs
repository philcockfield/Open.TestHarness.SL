using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Cloud.TableStorage;
using Open.Core.Common;
using Open.Core.Common.Testing;

using TMyModel = Open.Core.Cloud.Test.TableStorage.Mocks.MyModel;
using TBackingEntity = Open.Core.Cloud.Test.TableStorage.Mocks.MyTableEntity;
using TConverter = Open.Core.Cloud.Test.TableStorage.Mocks.EnumToString;

namespace Open.Core.Cloud.Test.TableStorage.PropertyManager
{
    [TestClass]
    public class PropertyMapCacheTest : CloudTestBase
    {
        #region Head

        private TMyModel model;
        private TBackingEntity backingEntity1;
        private AnotherTableEntity backingEntity2;
        private PropertyMapCache<TBackingEntity> cache;

        [TestInitialize]
        public void TestSetup()
        {
            backingEntity1 = new TBackingEntity();
            backingEntity2 = new AnotherTableEntity();
            model = new TMyModel(backingEntity1);
            cache = new PropertyMapCache<TBackingEntity>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldGetCorrespondingEntityProperty()
        {
            var modelProp = typeof (TMyModel).GetProperty("Text");
            modelProp.ShouldNotBe(null);

            var backingProp = typeof (TBackingEntity).GetProperty("Text");
            backingProp.ShouldNotBe(null);

            cache.GetPropertyMetadata(modelProp).BackingProperty.ShouldBe(backingProp);
            cache.GetPropertyMetadata(modelProp).BackingProperty.ShouldBe(backingProp);
        }

        [TestMethod]
        public void ShouldThrowIfModelPropertyNoPersistable()
        {
            var modelProp = typeof(TMyModel).GetProperty("NotPersistable");
            modelProp.ShouldNotBe(null);
            Should.Throw<ArgumentOutOfRangeException>(() => cache.GetPropertyMetadata(modelProp));
        }

        [TestMethod]
        public void ShouldThrowIfModelPropertyNotOnBackingEntity()
        {
            var modelProp = typeof(TMyModel).GetProperty("NotOnBackingEntity");
            modelProp.ShouldNotBe(null);
            Should.Throw<NotFoundException>(() => cache.GetPropertyMetadata(modelProp));
        }

        [TestMethod]
        public void ShouldMapToRowKey()
        {
            var modelProp = typeof(TMyModel).GetProperty("Id");
            var rowKeyProp = typeof(TBackingEntity).GetProperty("RowKey");
            modelProp.ShouldNotBe(null);
            rowKeyProp.ShouldNotBe(null);
            cache.GetPropertyMetadata(modelProp).BackingProperty.ShouldBe(rowKeyProp);
        }

        [TestMethod]
        public void ShouldMapToPartitionKey()
        {
            var modelProp = typeof(TMyModel).GetProperty("Partition");
            var partitionKeyProp = typeof(TBackingEntity).GetProperty("PartitionKey");
            modelProp.ShouldNotBe(null);
            partitionKeyProp.ShouldNotBe(null);
            cache.GetPropertyMetadata(modelProp).BackingProperty.ShouldBe(partitionKeyProp);
        }

        [TestMethod]
        public void ShouldThrowIfMoreThanOneRowKeyIsDeclared()
        {
            var propId1 = typeof(MultipleRowKeys).GetProperty("Id1");
            propId1.ShouldNotBe(null);
            Should.Throw<ArgumentOutOfRangeException>(() => cache.GetPropertyMetadata(propId1));
        }

        [TestMethod]
        public void ShouldThrowIfMoreThanOnePartitionKeyIsDeclared()
        {
            var propId1 = typeof(MultiplePartitionKeys).GetProperty("Partition1");
            propId1.ShouldNotBe(null);
            Should.Throw<ArgumentOutOfRangeException>(() => cache.GetPropertyMetadata(propId1));
        }

        [TestMethod]
        public void ShouldMapToDifferentBackingProperty()
        {
            var modelProp = typeof(TMyModel).GetProperty("Number");
            var backingProp = typeof(TBackingEntity).GetProperty("MyNumber");

            modelProp.ShouldNotBe(null);
            backingProp.ShouldNotBe(null);

            cache.GetPropertyMetadata(modelProp).BackingProperty.ShouldBe(backingProp);
        }

        [TestMethod]
        public void ShouldThrowIfPropertyHasNoSetting()
        {
            var modelProp = typeof(TMyModel).GetProperty("NoSetter");
            modelProp.ShouldNotBe(null);
            Should.Throw<ArgumentOutOfRangeException>(() => cache.GetPropertyMetadata(modelProp));
        }

        [TestMethod]
        public void ShouldNotHaveConverter()
        {
            var modelProp = typeof(TMyModel).GetProperty("Text");
            modelProp.ShouldNotBe(null);
            var metadata = cache.GetPropertyMetadata(modelProp);
            metadata.HasConverter.ShouldBe(false);
            metadata.Converter.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldHaveConverterSingleton()
        {
            var modelProp = typeof(TMyModel).GetProperty("EnumConversion");
            modelProp.ShouldNotBe(null);
            var metadata = cache.GetPropertyMetadata(modelProp);
            metadata.HasConverter.ShouldBe(true);

            var converter = metadata.Converter;
            converter.ShouldBeInstanceOfType<TConverter>();
            metadata.Converter.ShouldBe(converter);
        }

        [TestMethod]
        public void ShouldHaveDefaultConverterForEnum()
        {
            var modelProp = typeof(TMyModel).GetProperty("EnumValue");
            modelProp.ShouldNotBe(null);
            var metadata = cache.GetPropertyMetadata(modelProp);
            
            metadata.HasConverter.ShouldBe(true);
            metadata.Converter.ShouldBeInstanceOfType<EnumToIntConverter>();
        }

        [TestMethod]
        public void ShouldThrowIfConverterIsNotCorrectType()
        {
            var modelProp = typeof(TMyModel).GetProperty("BadConverterType");
            modelProp.ShouldNotBe(null);
            Should.Throw<ArgumentOutOfRangeException>(() => cache.GetPropertyMetadata(modelProp));
        }
        #endregion

        public interface IAnotherTableEntity : ITableServiceEntity { }
        public class AnotherTableEntity : IAnotherTableEntity
        {
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public DateTime Timestamp { get; set; }
        }

        public class MultipleRowKeys : AnotherTableEntity
        {
            [PersistProperty(IsRowKey = true)]
            public string Id1 { get; set; }

            [PersistProperty(IsRowKey = true)]
            public string Id2 { get; set; }
        }

        public class MultiplePartitionKeys : AnotherTableEntity
        {
            [PersistProperty(IsPartitonKey = true)]
            public string Partition1 { get; set; }

            [PersistProperty(IsPartitonKey = true)]
            public string Partition2 { get; set; }
        }

    }
}
