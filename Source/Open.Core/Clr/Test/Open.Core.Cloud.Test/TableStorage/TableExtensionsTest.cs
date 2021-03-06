﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Moq;
using Open.Core.Cloud.TableStorage;
using Open.Core.Cloud.Test;
using Open.Core.Cloud.Test.TableStorage.CodeGeneration;
using Open.Core.Cloud.Test.TableStorage.CodeGeneration.Generated;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.TableStorage
{
    [TestClass]
    public class TableExtensionsTest : CloudTestBase
    {
        #region Head
        private MockFactory mockFactory; 

        [TestInitialize]
        public void TestSetup()
        {
            mockFactory = new MockFactory(MockBehavior.Strict);
        }

         [TestCleanup]
         public void TestCleanup()
         {
             mockFactory.VerifyAll();
         }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldCreateCloudTableClientUsingConnectionString()
        {
            var mockCloudSettings = mockFactory.Create<ICloudSettings>();
            mockCloudSettings.Setup(p => p.DataConnectionString)
                .Returns(TableStorageConstants.DevelopmentConnectionString);

            // ---

            var client = mockCloudSettings.Object.CreateTableClient();
            client.ShouldBeInstanceOfType<CloudTableClient>();
        }

        [TestMethod]
        public void ShouldCreateAccountUsingConnectionString()
        {
            var mockCloudSettings = mockFactory.Create<ICloudSettings>();
            mockCloudSettings.Setup(p => p.DataConnectionString)
                .Returns(TableStorageConstants.DevelopmentConnectionString);

            var client = mockCloudSettings.Object.GetStorageAccount();
            client.ShouldBeInstanceOfType<CloudStorageAccount>();
        }

        [TestMethod]
        public void ShouldGetTableModelsFromAssembly()
        {
            var types = GetType().Assembly.GetTableModelTypes();
            types.ShouldContain(typeof(MockModelA));
            types.ShouldContain(typeof(MockModelB));
            types.ShouldContain(typeof(NoPersistableValues));
            types.ShouldNotContain(typeof(NonPersistableClass));
        }

        [TestMethod]
        public void ShouldGetPersistClassAttribute()
        {
            typeof(MockModelA).GetPersistAttribute().ShouldBeInstanceOfType<PersistClassAttribute>();
        }

        [TestMethod]
        public void ShouldNotGetPersistClassAttribute()
        {
            typeof(NonPersistableClass).GetPersistAttribute().ShouldBe(null);
        }

        [TestMethod]
        public void ShouldGetPersistPropertyAttribute()
        {
            var property = typeof (MockModelA).GetProperty("Text");
            property.ShouldNotBe(null);
            property.GetPersistAttribute().ShouldBeInstanceOfType<PersistPropertyAttribute>();
        }

        [TestMethod]
        public void ShouldNotGetPersistPropertyAttribute()
        {
            var property = typeof(MockModelA).GetProperty("NotPersisted");
            property.ShouldNotBe(null);
            property.GetPersistAttribute().ShouldBe(null);
        }

        [TestMethod]
        public void ShouldHaveQueryForEachKeyComparisonType()
        {
            var context = new MockModelAContext();
            var query = context.Query;

            foreach (KeyQueryType keyQueryType in typeof(KeyQueryType).GetEnumValues())
            {
                query.WhereKeysMatch(keyQueryType, "P1", "R1").ShouldNotBe(null);
            }
        }
        #endregion
    }
}
