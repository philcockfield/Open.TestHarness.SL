using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.StorageClient;
using Open.Core.Cloud.TableStorage;
using Open.Core.Cloud.Test;
using Open.Core.Cloud.Test.TableStorage.CodeGeneration.Generated;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.TableStorage
{
    [TestClass]
    public class TableServiceContextBaseTest : CloudTestBase
    {
        #region Head
        private string tableName;
        private CloudTableClient client;

        [TestInitialize]
        public void TestSetup()
        {
            tableName = TestEntityContext.GetDefaultTableName<TestEntity>();
            client = CloudSettings.CreateTableClient();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteTable();
        }

        private void DeleteTable()
        {
            client.DeleteTableIfExist(tableName);
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldHaveDefaultTableName()
        {
            var context = new TestEntityContext();
            context.TableName.ShouldBe("TestEntity");
            TestEntityContext.GetDefaultTableName<TestEntity>().ShouldBe(context.TableName);
        }

        [TestMethod]
        public void ShouldHaveCustomTableName()
        {
            var context = new MockModelAContext();
            context.TableName.ShouldBe("MyCustomTableName");
            context.DeleteTable();
        }


        [TestMethod]
        public void ShouldCreateTableOnConstruction()
        {
            DeleteTable();

            new TestEntityContext();
            var tables = client.ListTables().ToList();
            tables.ShouldContain(tableName);
        }

        [TestMethod]
        public void ShouldDeleteTable()
        {
            var context = new TestEntityContext();
            client.ListTables().ShouldContain(context.TableName);

            context.DeleteTable();
            client.ListTables().ShouldNotContain(context.TableName);
        }

        [TestMethod]
        public void ShouldDeleteCustomeNamedTable()
        {
            var context = new TestEntityCustomNameContext();
            client.ListTables().ShouldContain(context.TableName);

            context.DeleteTable();
            client.ListTables().ShouldNotContain(context.TableName);
        }

        [TestMethod]
        public void ShouldCreateTable()
        {
            var context = new TestEntityContext();
            context.DeleteTable();
            client.ListTables().ShouldNotContain(context.TableName);

            context.CreateTable();
            context.CreateTable();
            context.CreateTable();
            client.ListTables().ShouldContain(context.TableName);
        }

        [TestMethod]
        public void ShouldAddEntityWithDefaultTableName()
        {
            DeleteTable();

            var context = new TestEntityContext();
            var entity = new TestEntity { Text = "Foo.ShouldAdd" };
            context.AddObject(entity);
            context.SaveChanges();

            context.Query.Where(m => m.Text == entity.Text).ToList().Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldSaveAndRetrieveEntityFromCustomNamedTable()
        {
            var context = new TestEntityCustomNameContext();
            var entity = new TestEntity
                             {
                                 Text = "Foo.ShouldAddToCustomTable", 
                                 PartitionKey = "P1", 
                                 RowKey = "R1"
                             };
            context.AddObject(entity);
            context.SaveChanges();

            // ---

            var query1 = context.Query.Where(m => m.PartitionKey == "P1" && m.RowKey == "R1");
            query1.ToList().Count.ShouldBe(1);

            var query2 = context.Query.Where(m => m.Text == "Hello");
            query2.ToList().Count.ShouldBe(0);

            // ---

            context.DeleteTable();
        }

        #endregion

        #region Mocks
        private class TestEntityContext : TableServiceContextBase<TestEntity> { }
        private class TestEntity : TableServiceEntity
        {
            public TestEntity() : base(Guid.NewGuid().ToString(), String.Empty) { }
            public string Text { get; set; }
        }

        private class TestEntityCustomNameContext : TableServiceContextBase<TestEntity>
        {
            protected override string GetCustomTableName()
            {
                return "MyTestEntityCustomTableName";
            }
        }

        #endregion
    }
}
