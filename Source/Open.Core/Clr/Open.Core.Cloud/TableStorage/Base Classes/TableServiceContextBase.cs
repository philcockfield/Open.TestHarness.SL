using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace Open.Core.Cloud.TableStorage
{
    public abstract class TableServiceContextBase<T> : TableServiceContext
    {
        #region Head
        private static CloudStorageAccount storageAccount;
        private readonly CloudTableClient client;

        /// <summary>Constructor.</summary>
        protected TableServiceContextBase() : this(StorageAccount.TableEndpoint.ToString())
        {
        }

        /// <summary>Constructor.</summary>
        protected TableServiceContextBase(string baseAddress) : base(baseAddress, StorageAccount.Credentials)
        {
            client = StorageAccount.CreateCloudTableClient();
            client.CreateTableIfNotExist(TableName);
        }
        #endregion

        #region Properties
        /// <summary>Gets the name of the storage table.</summary>
        public string TableName { get { return typeof(T).Name; } }

        private static CloudStorageAccount StorageAccount
        {
            get { return storageAccount ?? (storageAccount = CloudSettings.Current.GetStorageAccount()); }
        }
        #endregion

        #region Methods
        /// <summary>Adds the specified object to the set of objects that the DataServiceContext is tracking.</summary>
        /// <param name="entity">The entity to add.</param>
        public void AddObject(T entity)
        {
            AddObject(TableName, entity);
        }

        /// <summary>Gets the name for the storage table.</summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        public static string GetTableName<TEntity>()
        {
            return typeof (TEntity).Name;
        }
        #endregion
    }
}
