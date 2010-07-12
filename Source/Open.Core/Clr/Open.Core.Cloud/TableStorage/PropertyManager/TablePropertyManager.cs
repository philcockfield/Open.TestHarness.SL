using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.WindowsAzure.StorageClient;
using Open.Core.Common;

[assembly: InternalsVisibleTo("Open.Core.Cloud.Test.Clr")]

namespace Open.Core.Cloud.TableStorage
{
    /// <summary>Translates storage of properties in a model to it's backing TableEntity.</summary>
    /// <typeparam name="TModel">The type of the model that is reading/writing properties to this store.</typeparam>
    /// <typeparam name="TBackingEntity">The type of the table entity that acts as the backing store for property values.</typeparam>
    public class TablePropertyManager<TModel, TBackingEntity> : AutoPropertyManager<TModel> where TBackingEntity : ITableServiceEntity
    {
        #region Head
        private static CloudTableClient cloudTableClient;
        private readonly Type modelType;
        private readonly PersistClassAttribute classAttribute;
        private PropertyMapCache<TBackingEntity> propertyCache;
        private static readonly List<PropertyMapCache<TBackingEntity>> PropertyCaches = new List<PropertyMapCache<TBackingEntity>>();

        /// <summary>Constructor.</summary>
        /// <param name="backingEntity">The backing entity to use.</param>
        public TablePropertyManager(TBackingEntity backingEntity)
        {
            // Setup initial conditions.
            if (Equals(backingEntity, default(TBackingEntity))) throw new ArgumentNullException("backingEntity");
            BackingEntity = backingEntity;

            // Store model type.
            modelType = typeof (TModel);
            classAttribute = modelType.GetPersistAttribute();
            if (classAttribute == null) throw new ArgumentOutOfRangeException(
                                    string.Format("The model type '{0}' is not decorated with [{1}].", 
                                    modelType.Name, 
                                    typeof(PersistClassAttribute).Name));
        }
        #endregion

        #region Properties
        /// <summary>Gets the table entity that acts as the backing store for property values.</summary>
        public TBackingEntity BackingEntity { get; private set; }

        /// <summary>Gets the property cache for the manager.</summary>
        internal PropertyMapCache<TBackingEntity> PropertyCache
        {
            get { return propertyCache ?? (propertyCache = GetOrCreatePropertyCache()); }
        }
        #endregion

        #region Methods
        /// <summary>Saves the backing entity to the table store.</summary>
        public void Save(TableServiceContextBase<TBackingEntity> context)
        {
            if (context == null) throw new ArgumentNullException("context");
            GetTableClient().CreateTableIfNotExist(context.TableName);
            context.AddObject(BackingEntity);
            context.SaveChanges();
        }
        #endregion

        #region Methods : Override
        protected override bool OnReadValue<T>(PropertyInfo modelProperty, out T value)
        {
            // Setup initial conditions.
            var metadata = PropertyCache.GetPropertyMetadata(modelProperty);
            var readValue = metadata.BackingProperty.GetValue(BackingEntity, null);

            // Perform value translations.
            if (metadata.HasConverter) readValue = metadata.Converter.ToSource(readValue);

            // Preform casting.
            value = Equals(readValue, null) ? default(T) : (T)readValue;
            return true;
        }

        protected override void OnWriteValue<T>(PropertyInfo modelProperty, T value, bool isDefaultValue)
        {
            // Setup initial conditions.
            var metadata = PropertyCache.GetPropertyMetadata(modelProperty);
            object writeValue = value;

            // Perform value translations.
            if (metadata.HasConverter) writeValue = metadata.Converter.ToTarget(writeValue);

            // Finish up.
            metadata.BackingProperty.SetValue(BackingEntity, writeValue, null);
        }
        #endregion

        #region Methods : Static

        /// <summary>Creates a new property-manager by looking up the backing entity that matches the given keys.</summary>
        /// <param name="context">The table-service context to use.</param>
        /// <param name="partitionKey">The partition key of the backing entity.</param>
        /// <param name="rowKey">The row key of the backing entity.</param>
        /// <param name="keyQueryType">Flag indicating how to structure the partition/row key query.</param>
        /// <returns>The property-manager, or Null if no corresponding entity could be found.</returns>
        public static TablePropertyManager<TModel, TBackingEntity> Lookup(
                                TableServiceContextBase<TBackingEntity> context, 
                                string partitionKey, 
                                string rowKey,
                                KeyQueryType keyQueryType = KeyQueryType.Literal) 
        {
            // Setup initial conditions.
            if (context == null) throw new ArgumentNullException("context");
            partitionKey = partitionKey.IsNullOrEmpty(true) ? String.Empty : partitionKey;
            rowKey = rowKey.IsNullOrEmpty(true) ? String.Empty : rowKey;

            // Construct the query.
            var query = context.Query.WhereKeysMatch(keyQueryType, partitionKey, rowKey);

            // Attempt to retrieve from table-storage.
            var entity = query.FirstOrDefault();

            // Construct the new property-manager.
            return Equals(entity, default(TBackingEntity)) 
                            ? null 
                            : new TablePropertyManager<TModel, TBackingEntity>(entity);
        }

        /// <summary>
        ///     Creates a new property-manager by looking up the backing entity that matches the given keys,
        ///     and if the backing entity is not found initializes a new property-manager with a virgin entity.
        /// </summary>
        /// <param name="context">The table-service context to use.</param>
        /// <param name="partitionKey">The partition key of the backing entity.</param>
        /// <param name="rowKey">The row key of the backing entity.</param>
        public static TablePropertyManager<TModel, TBackingEntity> LookupOrCreate(
                                TableServiceContextBase<TBackingEntity> context,
                                string partitionKey,
                                string rowKey)
        {
            var propManager = Lookup(context, partitionKey, rowKey);
            return propManager ?? Create(partitionKey, rowKey);
        }

        /// <summary>Creates a new instance of the property manager with the given keys.</summary>
        /// <param name="partitionKey">The partition key of the backing entity.</param>
        /// <param name="rowKey">The row key of the backing entity.</param>
        /// <returns></returns>
        public static TablePropertyManager<TModel, TBackingEntity> Create(string partitionKey = null, string rowKey = null)
        {
            // Create the entity.
            var entity = (TBackingEntity)Activator.CreateInstance(typeof(TBackingEntity));
            entity.PartitionKey = partitionKey.IsNullOrEmpty(true) ? String.Empty : partitionKey;
            entity.RowKey = rowKey.IsNullOrEmpty(true) ? String.Empty : rowKey;

            // Construct the new property-manager.
            return new TablePropertyManager<TModel, TBackingEntity>(entity);
        }
        #endregion

        #region Internal
        private static PropertyMapCache<TBackingEntity> GetOrCreatePropertyCache()
        {
            // Attempt to retrieve the cache (if it already exists).
            var backingType = typeof (TBackingEntity);
            var cache = PropertyCaches.FirstOrDefault(m => m.BackingType == backingType);
            if (cache != null) return cache;
            
            // First call for this type of backing-entity.  Create a new cache.
            cache = new PropertyMapCache<TBackingEntity>();
            PropertyCaches.Add(cache);

            // Finish up.
            return cache;
        }

        private static CloudTableClient GetTableClient() 
        {
            return cloudTableClient ?? (cloudTableClient = CloudSettings.Current.CreateTableClient()); 
        }
        #endregion
    }
}
