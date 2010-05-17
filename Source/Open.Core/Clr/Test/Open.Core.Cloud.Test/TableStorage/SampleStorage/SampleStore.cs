using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Open.Core.Cloud.Test.TableStorage.CodeGeneration;

using MockEntity = Open.Core.Cloud.Test.TableStorage.CodeGeneration.Generated.MockEntity1TableEntity;

namespace Open.Core.Cloud.TableStorage.SampleStorage
{

    [TestClass]
    public class SampleStore
    {

        [TestMethod]
        public void ShouldInsertEntity()
        {
            var dataSource = new MockDataSource();
            var entity = new MockEntity {Number = 1, Text = "Hello"};
            dataSource.Insert(entity);
        }

    }


    public class MockServiceContext : TableServiceContext
    {
        public const string TableName = "MockEntityTable";
        public MockServiceContext(string baseAddress, StorageCredentials credentials) : base(baseAddress, credentials)
        {
        }
        public IQueryable<MockEntity> Table { get { return CreateQuery<MockEntity>(TableName); } }
    }

    public class MockDataSource
    {
        #region Head
        private readonly MockServiceContext context;

        public MockDataSource()
        {
            var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            context = new MockServiceContext(storageAccount.TableEndpoint.ToString(), storageAccount.Credentials);

            // Create the tables
            // In this case, just a single table.  
            storageAccount.CreateCloudTableClient().CreateTableIfNotExist(MockServiceContext.TableName);
        }
        #endregion

        #region Methods
        public IEnumerable<MockEntity> Select()
        {
            var results = from c in context.Table
                          select c;

            var query = results.AsTableServiceQuery();
            var queryResults = query.Execute();

            return queryResults;
        }

        public void Delete(MockEntity itemToDelete)
        {
            context.AttachTo(MockServiceContext.TableName, itemToDelete, "*");
            context.DeleteObject(itemToDelete);
            context.SaveChanges();
        }

        public void Insert(MockEntity newItem)
        {
            context.AddObject(MockServiceContext.TableName, newItem);
            context.SaveChanges();
        }
        #endregion
    }




}
