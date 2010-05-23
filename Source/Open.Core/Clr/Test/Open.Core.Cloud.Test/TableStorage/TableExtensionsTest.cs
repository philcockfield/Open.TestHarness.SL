using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Moq;
using Open.Core.Cloud.Test;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.TableStorage
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
        #endregion
    }
}
