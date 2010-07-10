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
        public void ShouldAddEntityWithDefaultTableName()
        {
            DeleteTable();

            var context = new TestEntityContext();
            var entity = new TestEntity { Text = "Foo.ShouldAdd" };
            context.AddObject(entity);
            context.SaveChanges();

            context.Query.Where(m => m.Text == entity.Text).ToList().Count().ShouldBe(1);
        }
        #endregion

        #region Mocks
        private class TestEntityContext : TableServiceContextBase<TestEntity> { }
        private class TestEntity : TableServiceEntity
        {
            public TestEntity() : base(Guid.NewGuid().ToString(), String.Empty) { }
            public string Text { get; set; }
        }
        #endregion
    }
}
