using System.Data.Services.Client;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Open.Core.Common;

namespace Open.Core.Cloud.TableStorage
{
    /// <summary>The base class for accessing a specific table.</summary>
    /// <typeparam name="TEntity">The type of the table entity.</typeparam>
    public abstract class TableServiceContextBase<TEntity> : TableServiceContext
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
        public DataServiceQuery<TEntity> Query { get { return CreateQuery<TEntity>(TableName); } }
        #endregion

        #region Properties : Internal
        private static CloudStorageAccount StorageAccount
        {
            get { return storageAccount ?? (storageAccount = CloudSettings.Current.GetStorageAccount()); }
        }
        #endregion

        #region Methods
        /// <summary>Adds the specified object to the set of objects that the DataServiceContext is tracking.</summary>
        /// <param name="entity">The entity to add.</param>
        public void AddObject(TEntity entity) { AddObject(TableName, entity); }

        /// <summary>Creates a data service query for the table-service type.</summary>
        public DataServiceQuery<TEntity> CreateQuery() { return CreateQuery<TEntity>(TableName); }

        /// <summary>Deletes the table if it exists.</summary>
        public void DeleteTable() { client.DeleteTableIfExist(TableName); }

        /// <summary>Creates the table if it doesn't exist.</summary>
        public void CreateTable() { client.CreateTableIfNotExist(TableName); }
        #endregion

        #region Methods : Static
        /// <summary>Gets the name for the storage table.</summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        public static string GetDefaultTableName<TEntity>() { return typeof(TEntity).Name; }
        #endregion

        #region Methods : Virtual
        /// <summary>Retrieves the custom table name (if there is one).</summary>
        /// <remarks>
        ///     Used in T4 code generation to override the default table-name based on the custom 
        ///     table name specified in the [PersistClass] attribute.
        /// </remarks>
        protected virtual string GetCustomTableName() { return null; }
        #endregion

        #region Internal
        private string GetTableName()
        {
            var customName = GetCustomTableName();
            return customName.IsNullOrEmpty(true) ? GetDefaultTableName<TEntity>() : customName;
        }
        #endregion
    }
}
