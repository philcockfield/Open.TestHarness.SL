using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Open.Core.Common;

[assembly: InternalsVisibleTo("Open.Core.Cloud.Test.Clr")]


namespace Open.Core.Cloud.TableStorage
{
    /// <summary>Translates storage of properties in a model to it's backing TableEntity.</summary>
    /// <typeparam name="TModel">The type of the model that is reading/writing properties to this store.</typeparam>
    /// <typeparam name="TBackingEntity">The type of the table entity that acts as the backing store for property values.</typeparam>
    public class TableEntityPropertyManager<TModel, TBackingEntity> : AutoPropertyManager<TModel> where TBackingEntity : ITableServiceEntity
    {
        #region Head
        private readonly Type modelType;
        private readonly PersistClassAttribute classAttribute;
        private PropertyMapCache<TBackingEntity> propertyCache;
        private static readonly List<PropertyMapCache<TBackingEntity>> PropertyCaches = new List<PropertyMapCache<TBackingEntity>>();

        /// <summary>Constructor.</summary>
        /// <param name="backingEntity">The backing entity to use.</param>
        public TableEntityPropertyManager(TBackingEntity backingEntity)
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
        #endregion
    }
}
