using System.Data.Services.Client;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Open.Core.Common;

namespace Open.Core.Cloud.TableStorage
{
    public abstract class TableServiceContextBase<T> : TableServiceContext
    {
        #region Head
        private static CloudStorageAccount storageAccount;
        private readonly CloudTableClient client;
        private string tableName;

        /// <summary>Constructor.</summary>
        protected TableServiceContextBase() : this(StorageAccount.TableEndpoint.ToString()) { }

        /// <summary>Constructor.</summary>
        protected TableServiceContextBase(string baseAddress) : base(baseAddress, StorageAccount.Credentials)
        {
            client = StorageAccount.CreateCloudTableClient();
            client.CreateTableIfNotExist(TableName);
        }
        #endregion

        #region Properties
        /// <summary>Gets the name of the storage table.</summary>
        public string TableName { get { return tableName ?? (tableName = GetTableName()); } }

        /// <summary>Gets a typed service query provider for the table.</summary>
        public DataServiceQuery<T> Query { get { return CreateQuery<T>(TableName); } }

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
        public static string GetDefaultTableName<TEntity>() { return typeof(TEntity).Name; }

        /// <summary>Creates a data service query for the table-service type.</summary>
        public DataServiceQuery<T> CreateQuery() { return CreateQuery<T>(TableName); }

        /// <summary>Retrieves the custom table name (if there is one).</summary>
        protected virtual string GetCustomTableName() { return null; }
        #endregion

        #region Internal
        private string GetTableName()
        {
            var customName = GetCustomTableName();
            return customName.IsNullOrEmpty(true) ? GetDefaultTableName<T>() : customName;
        }
        #endregion
    }
}
