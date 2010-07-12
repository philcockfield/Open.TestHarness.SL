using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Cloud.TableStorage;
using Open.Core.Cloud.Test.TableStorage.CodeGeneration;
using Open.Core.Cloud.Test.TableStorage.CodeGeneration.Generated;
using Open.Core.Cloud.Test.TableStorage.Mocks;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.TableStorage.PropertyManager
{
    [TestClass]
    public class TablePropertyManagerTest : CloudTestBase
    {
        #region Head
        private MockModelATableEntity backingEntity;
        private MockModelA model;

        private MyModel myModel;
        private MyTableEntity myBackingEntity;

        private TablePropertyManager<MockModelA, MockModelATableEntity> propertyManager;

        [TestInitialize]
        public void TestSetup()
        {
            backingEntity = new MockModelATableEntity();
            model = new MockModelA(backingEntity);
            propertyManager = model.Property;

            // --

            myBackingEntity = new MyTableEntity();
            myModel = new MyModel(myBackingEntity);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            propertyManager.Dispose();
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldThrowIfTableEntityNotPersistable()
        {
            Should.Throw<ArgumentOutOfRangeException>(() =>
                                    {
                                        new TablePropertyManager<NonPersistableClass, IMockModelATableEntity>(backingEntity);
                                    });
        }

        [TestMethod]
        public void ShouldInjectBackingEntity()
        {
            propertyManager.BackingEntity.ShouldBe(backingEntity);
        }

        [TestMethod]
        public void ShouldThrowIfBackingEntityNotSupplied()
        {
            Should.Throw<ArgumentNullException>(() => new TablePropertyManager<NonPersistableClass, IMockModelATableEntity>(null));
        }

        [TestMethod]
        public void ShouldStoreTextPropertyValueInBackingStore()
        {
            model.Text.ShouldBe(null);

            model.Text = "Foo";
            model.Text.ShouldBe("Foo");
            backingEntity.Text.ShouldBe("Foo");

            model.Property.SetValue(m => m.Text, "Bar");
            model.Text.ShouldBe("Bar");
            backingEntity.Text.ShouldBe("Bar");
        }

        [TestMethod]
        public void ShouldReadFromInjectedBackingStore()
        {
            backingEntity = new MockModelATableEntity { Text = "Hello" };
            model = new MockModelA(backingEntity);
            model.Text.ShouldBe("Hello");
        }

        [TestMethod]
        public void ShouldTranslateIntToEnumWhenReading()
        {
            myModel.EnumValue.ShouldBe(MyEnum.Zero);
            myBackingEntity.EnumValue = 2;
            myModel.EnumValue.ShouldBe(MyEnum.Two);
        }

        [TestMethod]
        public void ShouldTranslateEnumToIntWhenWriting()
        {
            myModel.EnumValue.ShouldBe(MyEnum.Zero);
            myModel.EnumValue = MyEnum.Three;
            myBackingEntity.EnumValue.ShouldBe(3);
        }

        [TestMethod]
        public void ShouldMapToDifferentBackingEntityName()
        {
            myModel.Number.ShouldBe(0);
            myModel.Number = 42;
            myBackingEntity.MyNumber.ShouldBe(42);

            myBackingEntity.MyNumber = 1024;
            myModel.Number.ShouldBe(1024);
        }

        [TestMethod]
        public void ShouldHaveSingletonInstanceOfPropertyCache()
        {
            var model1 = new MockModelA(new MockModelATableEntity());
            var model2 = new MockModelA(new MockModelATableEntity());

            model1.Property.PropertyCache.ShouldNotBe(null);
            model1.Property.PropertyCache.ShouldBe(model2.Property.PropertyCache);
        }

        [TestMethod]
        public void ShouldConvertEnumToStringOverridingDefaultConverter()
        {
            myModel.EnumConversion = MyEnum.Three;
            myBackingEntity.EnumAsString.ShouldBe("Three");

            myBackingEntity.EnumAsString = "Two";
            myModel.EnumConversion.ShouldBe(MyEnum.Two);
        }

        [TestMethod]
        public void ShouldMapIdPropertyToRowKey()
        {
            model.Id = "MyId";
            backingEntity.RowKey.ShouldBe("MyId");

            backingEntity.RowKey = "Foo";
            model.Id.ShouldBe("Foo");
        }

        [TestMethod]
        public void ShouldMapPartitionPropertyToPartitionKey()
        {
            model.Partition = "P1";
            backingEntity.PartitionKey.ShouldBe("P1");

            backingEntity.PartitionKey = "Foo";
            model.Partition.ShouldBe("Foo");
        }

        [TestMethod]
        public void ShouldSaveChangesToTable()
        {
            var client = CloudSettings.CreateTableClient();
            var context = new MockModelAContext();
            client.DeleteTableIfExist(context.TableName);

            // ---

            var entity = new MockModelATableEntity("P1", "R1");
            var mock = new MockModelA(entity) {Text = "FooBar", Number = 42};
            mock.Property.Save(context);

            // ---

            var query = context.CreateQuery().Where(m => m.PartitionKey == "P1" && m.RowKey == "R1");
            query.ToList().Count.ShouldBe(1);

            query = context.CreateQuery().Where(m => m.PartitionKey == "P1" && m.RowKey == "R2");
            query.ToList().Count.ShouldBe(0);

            // ---

            client.DeleteTableIfExist(context.TableName);
        }

        [TestMethod]
        public void ShouldCreateFromLookup()
        {
            var context = new MockModelAContext();
            context.DeleteTable();
            var entity = new MockModelATableEntity("P1", "R1");
            var mock = new MockModelA(entity) { Text = "FooBar", Number = 42 };
            mock.Property.Save(context);

            // ---
            // Lookup
            context = new MockModelAContext();
            var propManager1 = TablePropertyManager<MockModelA, MockModelATableEntity>.Lookup(context, "P1", "R1");
            propManager1.GetValue<string>(m => m.Text).ShouldBe("FooBar");
            propManager1.GetValue<string>(m => m.Partition).ShouldBe("P1");
            propManager1.GetValue<string>(m => m.Id).ShouldBe("R1");

            // ---
            // LookupOrCreate
            var propManager3 = TablePropertyManager<MockModelA, MockModelATableEntity>.LookupOrCreate(context, "P1", "R1");
            propManager3.GetValue<string>(m => m.Text).ShouldBe("FooBar");
            propManager3.GetValue<string>(m => m.Partition).ShouldBe("P1");
            propManager3.GetValue<string>(m => m.Id).ShouldBe("R1");
        }

        [TestMethod]
        public void ShouldReturnNullFromLookup()
        {
            var context = new MockModelAContext();
            context.DeleteTable();
            var entity = new MockModelATableEntity("P1", "R1");
            var mock = new MockModelA(entity) { Text = "FooBar", Number = 42 };
            mock.Property.Save(context);

            // ---

            context = new MockModelAContext();
            var propManager = TablePropertyManager<MockModelA, MockModelATableEntity>.Lookup(context, "P1", "R-2-NEW-ID");
            propManager.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldLookupWithPartialKeys()
        {
            var context = new MockModelAContext();
            context.DeleteTable();
            var entity = new MockModelATableEntity("P1", "R1");
            var mock = new MockModelA(entity) { Text = "FooBar", Number = 42 };
            mock.Property.Save(context);

            // ---

            context = new MockModelAContext();

            var propManager = TablePropertyManager<MockModelA, MockModelATableEntity>.Lookup(context, "Z", "X", KeyQueryType.StartsWith);
            propManager.ShouldBe(null);

            propManager = TablePropertyManager<MockModelA, MockModelATableEntity>.Lookup(context, "P", "R", KeyQueryType.StartsWith);
            propManager.GetValue<string>(m => m.Text).ShouldBe("FooBar");

            propManager = TablePropertyManager<MockModelA, MockModelATableEntity>.Lookup(context, "P1", "R", KeyQueryType.StartsWith);
            propManager.GetValue<string>(m => m.Text).ShouldBe("FooBar");

            propManager = TablePropertyManager<MockModelA, MockModelATableEntity>.Lookup(context, "P", "R1", KeyQueryType.StartsWith);
            propManager.GetValue<string>(m => m.Text).ShouldBe("FooBar");

            propManager = TablePropertyManager<MockModelA, MockModelATableEntity>.Lookup(context, "P1", "R1", KeyQueryType.StartsWith);
            propManager.GetValue<string>(m => m.Text).ShouldBe("FooBar");
        }

        [TestMethod]
        public void ShouldCreateNewInstanceWhenLookupNotFound()
        {
            var context = new MockModelAContext();
            context.DeleteTable();
            context.CreateTable();

            var propManager = TablePropertyManager<MockModelA, MockModelATableEntity>.LookupOrCreate(context, "P1", "R1");
            propManager.GetValue<string>(m => m.Text).ShouldBe(null);
            propManager.GetValue<string>(m => m.Partition).ShouldBe("P1");
            propManager.GetValue<string>(m => m.Id).ShouldBe("R1");
        }
        #endregion
    }
}
