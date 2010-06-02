using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Open.Core.Common
{
    /// <summary>Event arguments that accompany the property-changed event.</summary>
    public class AutoPropertySetEventArgs : EventArgs
    {
        public PropertyInfo Property { get; set; }
        public Object Value { get; set; }
    }


    /// <summary>Manages the storage of auto-properties.</summary>
    public class AutoPropertyManager : DisposableBase
    {
        #region Events
        /// <summary>Fires when an auto-property is set.</summary>
        public event EventHandler<AutoPropertySetEventArgs> PropertySet;
        private void FirePropertySet(AutoPropertySetEventArgs e){if (PropertySet != null) PropertySet(this, e);}
        #endregion

        #region Head
        private readonly Action<PropertyChangedEventArgs> firePropertyChanged;
        private readonly List<ItemReference> values = new List<ItemReference>();

        /// <summary>Constructor.</summary>
        /// <param name="firePropertyChanged">Action that will fire the property-changed event on the instance this object is managing.</param>
        public AutoPropertyManager(Action<PropertyChangedEventArgs> firePropertyChanged = null)
        {
            // Store values.
            this.firePropertyChanged = firePropertyChanged;
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            foreach (var item in values)
            {
                item.PropertyInfo = null;
                item.Value = null;
            }
            values.Clear();
        }
        #endregion

        #region Properties
        /// <summary>Gets the number of values that are currently stored in the manager.</summary>
        public int ValueCount { get { return values.Count; } }
        #endregion

        #region Methods - Serialization
        /// <summary>Retrieves the stored values as a serialized JSON string and type.</summary>
        public string GetSerializedValues()
        {
            if (IsDisposed) return null;
            try
            {
                return new SerializedValues(values).ToSerializedJson();
            }
            catch (SerializationException error)
            {
                throw new SerializationException("Failed to convert auto properties to serialized string.  See inner exception for detais.", error);
            }
        }

        /// <summary>Populates the property-manager with values deserialized from the given JSON string.</summary>
        /// <param name="serializedValues">The JSON string to populate values with (see 'GetSerializedValues').</param>
        public void Populate(string serializedValues)
        {
            // Setup initial conditions.
            if (IsDisposed) return;
            if (serializedValues.IsNullOrEmpty(true)) throw new ArgumentNullException("serializedValues");
            var container = serializedValues.FromSerializedJson<SerializedValues>();
            values.Clear();

            // Reconstruct the serialized values.
            foreach (var item in container.Values)
            {
                values.Add(item.ToItemReference(container.TypeLibrary));
            }
        }

        /// <summary>
        ///     Creates a new instance of the property-manager populated with deserialized 
        ///     values from the given JSON string.
        /// </summary>
        /// <param name="serializedValues">The JSON string to populate values with (see 'GetSerializedValues').</param>
        /// <param name="firePropertyChanged">Action that will fire the property-changed event on the instance this object is managing.</param>
        public static AutoPropertyManager Create(string serializedValues, Action<PropertyChangedEventArgs> firePropertyChanged = null)
        {
            if (serializedValues.IsNullOrEmpty(true)) throw new ArgumentNullException("serializedValues");
            var manager = new AutoPropertyManager(firePropertyChanged);
            manager.Populate(serializedValues);
            return manager;
        }
        #endregion

        #region Methods - GetValue | SetValue
        /// <summary>Gets the value for the specified property from the auto-property backing store.</summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <typeparam name="TResult">The type of the property return value.</typeparam>
        /// <param name="property">An expression representing the property (for example 'n => n.PropertyName').</param>
        /// <param name="defaultValue">The default value to use if the property has not already been stored.</param>
        public TResult GetValue<T, TResult>(Expression<Func<T, object>> property, TResult defaultValue = default(TResult))
        {
            if (property == null) throw new ArgumentNullException("property");
            return GetValue(property.GetPropertyInfo(), defaultValue);
        }
        private TResult GetValue<TResult>(PropertyInfo propertyInfo, TResult defaultValue = default(TResult))
        {
            // Setup initial conditions.
            if (IsDisposed) return defaultValue;

            // Attempt to read the value.
            TResult value;
            if (OnReadValue(propertyInfo, out value)) return value;

            // Value not stored already.  Return the default value, and register it in the store (silently, without raising event).
            OnWriteValue(propertyInfo, defaultValue, true);
            return defaultValue;
        }

        /// <summary>
        ///    Sets the value for the specified property, storing it in the auto-property backing store and
        ///    firing the corresponding 'PropertyChanged' event(s) as required.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <typeparam name="TResult">The type of the property return value.</typeparam>
        /// <param name="property">An expression representing the property (for example 'n => n.PropertyName').</param>
        /// <param name="value">The new value of the property.</param>
        /// <param name="fireAlso">
        ///    A collection of expressions that represent the properties 
        ///    that have also changed as a result of this change and should
        ///    therefore also have their PropertyChanged events fired.
        ///    (for example 'n => n.PropertyName'.)
        /// </param>
        /// <returns>True if the given value was different from the current value (and therefore events were fired), otherwise False.</returns>
        public bool SetValue<T, TResult>(Expression<Func<T, object>> property, TResult value, params Expression<Func<T, object>>[] fireAlso)
        {
            return SetValue(property, value, default(TResult), fireAlso);
        }

        /// <summary>
        ///    Sets the value for the specified property, storing it in the auto-property backing store and
        ///    firing the corresponding 'PropertyChanged' event(s) as required.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <typeparam name="TResult">The type of the property return value.</typeparam>
        /// <param name="property">An expression representing the property (for example 'n => n.PropertyName').</param>
        /// <param name="defaultValue">The default value to use if the property has not already been stored.</param>
        /// <param name="value">The new value of the property.</param>
        /// <param name="fireAlso">
        ///    A collection of expressions that represent the properties 
        ///    that have also changed as a result of this change and should
        ///    therefore also have their PropertyChanged events fired.
        ///    (for example 'n => n.PropertyName'.)
        /// </param>
        /// <returns>True if the given value was different from the current value (and therefore events were fired), otherwise False.</returns>
        public bool SetValue<T, TResult>(Expression<Func<T, object>> property, TResult value, TResult defaultValue, params Expression<Func<T, object>>[] fireAlso)
        {
            // Setup initial conditions.
            if (IsDisposed) return false;
            if (property == null) throw new ArgumentNullException("property");
            var propInfo = property.GetPropertyInfo();

            // Retrieve the current value.
            var currentValue = GetValue(propInfo, defaultValue);
            if (Equals(currentValue, value)) return false;

            // Store the value.
            OnWriteValue(propInfo, value, false);

            // Alert listeners.
            FirePropertyChanged(propInfo.Name);
            FirePropertyChangedEvents(propInfo.Name, fireAlso);
            FirePropertySet(new AutoPropertySetEventArgs { Property = propInfo, Value = value });

            // Finish up.
            return true;
        }
        #endregion

        #region Methods (Virtual) - Read/Write (used internally for overriding the data-store).
        /// <summary>Gets a property value in the dictionary (DO NOT USE unless overriding auto-backing-field store).</summary>
        /// <param name="property">The property being read.</param>
        /// <param name="value">The variable to return the value within</param>
        /// <returns>True if the value exists within the store, otherwise False.</returns>
        protected virtual bool OnReadValue<T>(PropertyInfo property, out T value)
        {
            var item = GetItem(property);
            value = (item == null)
                        ? default(T)
                        : (T)item.Value;
            return item != null;
        }

        /// <summary>Stores a property value in the dictionary (DO NOT USE unless overriding auto-backing-field store).</summary>
        /// <param name="property">The property being written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="isDefaultValue">Flag indicating if the given value is the default value.</param>
        protected virtual void OnWriteValue<T>(PropertyInfo property, T value, bool isDefaultValue)
        {
            // Setup initial conditions.
            var item = GetItem(property);

            // Create the new item reference and store it.
            if (item == null)
            {
                item = new ItemReference { PropertyInfo = property };
                values.Add(item);
            }

            // Finish up.
            item.Value = value;
        }
        #endregion

        #region Internal
        private ItemReference GetItem(PropertyInfo propInfo)
        {
            return values.FirstOrDefault(m => m.IsMatch(propInfo));
        }

        private void FirePropertyChangedEvents<T>(string excludeProperty, params Expression<Func<T, object>>[] properties)
        {
            if (firePropertyChanged == null) return;
            foreach (var expression in properties)
            {
                var propertyName = expression.GetPropertyName();
                if (propertyName != excludeProperty) FirePropertyChanged(propertyName);
            }
        }

        private void FirePropertyChanged(string propertyName)
        {
            if (firePropertyChanged == null) return;
            firePropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public class ItemReference
        {
            public PropertyInfo PropertyInfo { get; set; }
            public object Value { get; set; }
            public bool IsMatch(PropertyInfo property) { return Equals(PropertyInfo, property); }
        }
        
        [DataContract]
        public class SerializedItemReference
        {
            #region Head
            public SerializedItemReference() { }
            public SerializedItemReference(ItemReference item, Dictionary<string, int> typeLibrary)
            {
                PropertyName = item.PropertyInfo.Name;
                PropertyType = StoreType(item.PropertyInfo.DeclaringType, typeLibrary);
                if (item.Value != null) ValueType = StoreType(item.Value.GetType(), typeLibrary);
                ValueAsJson = item.Value == null ? null : item.Value.ToSerializedJson();
            }
            #endregion

            #region Properties
            [DataMember] public string PropertyName { get; set; }
            [DataMember] public int PropertyType { get; set; }
            [DataMember] public int? ValueType { get; set; }
            [DataMember] public string ValueAsJson { get; set; }
            #endregion

            #region Methods
            public ItemReference ToItemReference(Dictionary<string, int> typeLibrary)
            {
                var valueType = ValueType == null ? null : GetType(ValueType.Value, typeLibrary);
                var propertyType = GetType(PropertyType, typeLibrary);
                return new ItemReference
                           {
                               PropertyInfo = propertyType.GetProperty(PropertyName),
                               Value = ValueAsJson == null ? null : ValueAsJson.FromSerializedJson(valueType)
                           };
            }
            #endregion

            #region Internal
            private static Type GetType(int index, Dictionary<string, int> typeLibrary)
            {
                return typeLibrary.ContainsValue(index) ? Type.GetType(typeLibrary.FirstOrDefault(m => m.Value == index).Key) : null;
            }

            private static int StoreType(Type type, Dictionary<string, int> typeLibrary)
            {
                var name = type.AssemblyQualifiedName;
                if (typeLibrary.ContainsKey(name))
                {
                    return typeLibrary.FirstOrDefault(m => m.Key == name).Value;
                }
                else
                {
                    var index = typeLibrary.Count + 1;
                    typeLibrary.Add(name, index);
                    return index;
                }
            }
            #endregion
        }

        [DataContract]
        public class SerializedValues
        {
            #region Head
            public SerializedValues(){}
            public SerializedValues(IEnumerable<ItemReference> values)
            {
                TypeLibrary = new Dictionary<string, int>();
                Values = values.Select(m => new SerializedItemReference(m, TypeLibrary)).ToList();
            }
            #endregion

            #region Properties
            [DataMember] public List<SerializedItemReference> Values { get; set; }
            [DataMember] public Dictionary<string, int> TypeLibrary { get; set; }
            #endregion
        }
    }
}
