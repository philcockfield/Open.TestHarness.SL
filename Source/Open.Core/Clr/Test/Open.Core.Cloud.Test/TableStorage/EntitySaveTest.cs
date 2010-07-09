using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.StorageClient;
using Open.Core.Cloud.TableStorage;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.TableStorage
{
    [TestClass]
    public class EntitySaveTest : CloudTestBase
    {
        #region Head
        private readonly CloudTableClient client;

        public EntitySaveTest()
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
//            DeleteTable();
        }

        private void DeleteTable()
        {
            var tableName = TestEntityContext.GetTableName<TestEntity>();
            client.DeleteTableIfExist(tableName);
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldSave()
        {
            DeleteTable();

            var context = new TestEntityContext();
            var entity = new TestEntity
                             {
                                 PartitionKey = "Partition1",
                                 RowKey = "1",
                                 Text = "Zazen", 
                                 Number = 42,
                                 StringArray = new[] { "One", "Two" },
                             };

            context.AddObject(entity);
            context.SaveChanges();

//            DeleteTable();
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
            public string[] StringArray { get; set; }
        }
        #endregion
    }
}