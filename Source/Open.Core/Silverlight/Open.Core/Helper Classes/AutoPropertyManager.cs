using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Open.Core.Common
{
    /// <summary>Manages the storage of auto-properties.</summary>
    public class AutoPropertyManager : DisposableBase
    {
        #region Head
        private readonly Action<PropertyChangedEventArgs> firePropertyChanged;
        private readonly List<ItemReference> values = new List<ItemReference>();

        /// <summary>Constructor.</summary>
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

        #region Methods
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
                item = new ItemReference {PropertyInfo = property};
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

        private class ItemReference
        {
            public PropertyInfo PropertyInfo { get; set; }
            public object Value { get; set; }
            public bool IsMatch(PropertyInfo property) { return Equals(PropertyInfo, property); }
        }
    }
}
