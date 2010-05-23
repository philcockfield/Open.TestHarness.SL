using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.StorageClient;
using Open.Core.Cloud.TableStorage;
using Open.Core.Cloud.Test.TableStorage.CodeGeneration.Generated;

namespace Open.Core.Cloud.Test.TableStorage.SampleStorage
{

    [TestClass]
    public class SampleStore : CloudTestBase
    {
        private CloudTableClient cloudTableClient;
        TableServiceContext cloudTableServiceContext;

        public SampleStore()
        {

//            CloudConfiguration.InitializeCloudStorageAccount();
        }

        [TestMethod]
        public void ShouldInsertEntity()
        {
            //var s = CloudSettings;

            //var cloudStorageAccount = CloudStorageAccount.Parse(TableStorageConstants.DevelopmentConnectionString);
            //cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            //cloudTableClient.CreateTableIfNotExist("MyTable");

            var client = CloudSettings.CreateTableClient();
            client.CreateTableIfNotExist("MyTable");

            var b = new MockEntity1Context();
            var entity = new MockEntity1TableEntity
                             {
                                 Number = 3,
                                 Text = "Hello"
                             };

            b.AddObject("MyTable", entity);
            b.SaveChanges();

//            cloudTableClient.DeleteTableIfExist("MyTable");

            //var storageAccount = CloudStorageAccount.FromConfigurationSetting("DataConnectionString");
            //CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

        }
    }

}
