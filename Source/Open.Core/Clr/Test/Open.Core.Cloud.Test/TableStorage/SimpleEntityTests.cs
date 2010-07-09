using System;
using System.Linq;
using System.Data.Services.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.StorageClient;
using Open.Core.Cloud.TableStorage;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.TableStorage
{
    [TestClass]
    public class SimpleEntityTests : CloudTestBase
    {
        #region Head
        private readonly CloudTableClient client;

        public SimpleEntityTests()
        {
            client = CloudSettings.CreateTableClient();
        }

        [TestInitialize]
        public void TestSetup()
        {
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteTables();
        }

        private void DeleteTables()
        {
            client.DeleteTableIfExist(TestEntityContext.GetTableName<TestEntity>());
            client.DeleteTableIfExist(TestEntityContext.GetTableName<SuperClass>());
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldSave()
        {
            var context = new TestEntityContext();
            var entity = new TestEntity
                             {
                                 PartitionKey = "Partition1",
                                 RowKey = "1",
                                 Text = "Zazen", 
                                 Number = 42,
                             };

            context.AddObject(entity);
            context.SaveChanges();
        }

        [TestMethod]
        public void ShouldSaveDerviedProperties()
        {
            var context = new SuperClassContext();
            var entity = new SuperClass
                             {
                                 PartitionKey = "Partition1",
                                 RowKey = "1",
                                 Text = "ParentText",
                                 ChildProperty = "ChildText",
                             };
            context.AddObject(entity);
            context.SaveChanges();
        }

        [TestMethod]
        public void ShouldFindPartitionKey()
        {
            CreateAndSave(rowKey: "1");
            CreateAndSave(rowKey: "2");

            var context = new TestEntityContext();
            var query = context
                        .CreateQuery<TestEntity>(TestEntityContext.GetTableName<TestEntity>())
                        .Where(m => m.PartitionKey.CompareTo("Partition1") == 0);

            var items = query.ToList();
            items.Count.ShouldBe(2);
        }

        [TestMethod]
        public void ShouldFindByStartsWithOnRowKey()
        {
            // See: http://www.dotnetsolutions.co.uk/blog/archive/2010/05/28/starts-with-query-pattern-windows-azure-table-design-patterns/

            CreateAndSave(rowKey: "METABOLIFE");
            CreateAndSave(rowKey: "METABOLISE");
            CreateAndSave(rowKey: "METABOLISED");
            CreateAndSave(rowKey: "METABOLISM");
            CreateAndSave(rowKey: "METABOLITE");

            var context = new TestEntityContext();
            var query = context
                    .CreateQuery<TestEntity>(TestEntityContext.GetTableName<TestEntity>())
                    .Where(m => 
                            m.RowKey.CompareTo("METABOLIS") >= 0 &&
                            m.RowKey.CompareTo("METABOLIT") < 0);

            var items = query.ToList();
            items.Count.ShouldBe(3);
        }

        [TestMethod]
        public void ShouldFindByStartsWithOnTextProperty()
        {
            CreateAndSave(text: "METABOLIFE");
            CreateAndSave(text: "METABOLISE");
            CreateAndSave(text: "METABOLISED");
            CreateAndSave(text: "METABOLISM");
            CreateAndSave(text: "METABOLITE");

            var context = new TestEntityContext();
            var query = context
                    .CreateQuery<TestEntity>(TestEntityContext.GetTableName<TestEntity>())
                    .Where(m =>
                            m.Text.CompareTo("METABOLIS") >= 0 &&
                            m.Text.CompareTo("METABOLIT") < 0);

            var items = query.ToList();
            items.Count.ShouldBe(3);
        }

        #endregion

        #region Internal

        private int createCount;
        private void CreateAndSave(string partitionKey = "Partition1", string rowKey = null, string text = null)
        {
            createCount++;

            var context = new TestEntityContext();
            var entity = new TestEntity
                        {
                            PartitionKey = partitionKey,
                            RowKey = rowKey ?? "Row" + createCount,
                            Text = text ?? "Foo" + createCount,
                            Number = createCount,
                        };

            context.AddObject(entity);
            context.SaveChanges();
        }
        #endregion

        #region Mocks
        private class TestEntityContext : TableServiceContextBase<TestEntity> { }

        [DataServiceEntity]
        public class TestEntity
        {
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public DateTime Timestamp { get; set; }

            public string Text { get; set; }
            public int Number { get; set; }
        }

        private class SuperClassContext : TableServiceContextBase<SuperClass> { }
        public class SuperClass : TestEntity
        {
            public string ChildProperty { get; set; }
        }
        #endregion
    }
}