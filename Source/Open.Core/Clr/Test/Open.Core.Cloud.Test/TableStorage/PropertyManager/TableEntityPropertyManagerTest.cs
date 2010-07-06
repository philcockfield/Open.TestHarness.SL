﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Cloud.TableStorage;
using Open.Core.Cloud.Test.TableStorage.CodeGeneration;
using Open.Core.Cloud.Test.TableStorage.Mocks;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.TableStorage.PropertyManager
{
    [TestClass]
    public class TableEntityPropertyManagerTest : CloudTestBase
    {
        #region Head
        private MyTableEntity backingEntity;
        private MyModel model;

        private TableEntityPropertyManager<MyModel, IMyTableEntity> propertyManager;

        [TestInitialize]
        public void TestSetup()
        {
            backingEntity = new MyTableEntity();
            model = new MyModel(backingEntity);
            propertyManager = model.Property;
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
                                        new TableEntityPropertyManager<NonPersistableClass, IMyTableEntity>(backingEntity);
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
            Should.Throw<ArgumentNullException>(() => new TableEntityPropertyManager<NonPersistableClass, IMyTableEntity>(null));
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
            backingEntity = new MyTableEntity { Text = "Hello" };
            model = new MyModel(backingEntity);
            model.Text.ShouldBe("Hello");
        }

        [TestMethod]
        public void ShouldTranslateIntToEnumWhenReading()
        {
            model.EnumValue.ShouldBe(MyEnum.Zero);
            backingEntity.EnumValue = 2;
            model.EnumValue.ShouldBe(MyEnum.Two);
        }

        [TestMethod]
        public void ShouldTranslateEnumToIntWhenWriting()
        {
            model.EnumValue.ShouldBe(MyEnum.Zero);
            model.EnumValue = MyEnum.Three;
            backingEntity.EnumValue.ShouldBe(3);
        }

        [TestMethod]
        public void ShouldMapToDifferentBackingEntityName()
        {
            model.Number.ShouldBe(0);
            model.Number = 42;
            backingEntity.MyNumber.ShouldBe(42);

            backingEntity.MyNumber = 1024;
            model.Number.ShouldBe(1024);
        }

        [TestMethod]
        public void ShouldHaveSingletonInstanceOfPropertyCache()
        {
            var model1 = new MyModel(new MyTableEntity());
            var model2 = new MyModel(new MyTableEntity());

            model1.Property.PropertyCache.ShouldNotBe(null);
            model1.Property.PropertyCache.ShouldBe(model2.Property.PropertyCache);
        }

        [TestMethod]
        public void ShouldConvertEnumToStringOverridingDefaultConverter()
        {
            model.EnumConversion = MyEnum.Three;
            backingEntity.EnumAsString.ShouldBe("Three");

            backingEntity.EnumAsString = "Two";
            model.EnumConversion.ShouldBe(MyEnum.Two);
        }

//        [TestMethod]
//        public void ShouldSaveChangesToTable()
//        {
//            // Setup initial conditions.
//            var tableName = MyTableEntityContext.GetTableName<MyTableEntity>();
//            var client = CloudSettings.CreateTableClient();

//            model.Text = "FooBar";
//            model.EnumValue = MyEnum.Two;
//            model.Number = 42;

//            var context = new MyTableEntityContext();
//            context.AddObject(backingEntity);
//            context.SaveChanges();

//            // Finish up.
////            client.DeleteTableIfExist(tableName);
//        }
        #endregion
    }
}
