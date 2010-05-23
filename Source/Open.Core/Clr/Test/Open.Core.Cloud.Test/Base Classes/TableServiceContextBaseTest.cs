using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.StorageClient;
using Open.Core.Cloud.TableStorage;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.Base_Classes
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
            tableName = MockContext.GetTableName<TestEntity>();
            client = CloudSettings.CreateTableClient();
        }

        private void DeleteTable()
        {
            client.DeleteTableIfExist(tableName);
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldHaveTableName()
        {
            var context = new MockContext();
            context.TableName.ShouldBe("TestEntity");
            MockContext.GetTableName<TestEntity>().ShouldBe(context.TableName);
        }

        [TestMethod]
        public void ShouldCreateTableOnConstruction()
        {
            DeleteTable();

            new MockContext();
            var tables = client.ListTables().ToList();
            tables.ShouldContain(tableName);
        }

        [TestMethod]
        public void ShouldAddEntityWithDefaultTableName()
        {
            DeleteTable();

            var context = new MockContext();
            var entity = new TestEntity { Text = "Foo.ShouldAdd" };
            context.AddObject(entity);
            context.SaveChanges();

            var query = context.CreateQuery<TestEntity>(context.TableName);
            query.Where(m => m.Text == entity.Text).ToList().Count().ShouldBe(1);

            DeleteTable();
        }
        #endregion

        #region Stubs
        private class MockContext : TableServiceContextBase<TestEntity>
        {
        }

        private class TestEntity : TableServiceEntity
        {
            public TestEntity() : base(Guid.NewGuid().ToString(), String.Empty) { }
            public string Text { get; set; }
        }
        #endregion
    }
}
