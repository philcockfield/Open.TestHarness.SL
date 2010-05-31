using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Open.Core.Common.Test.Clr")]

namespace Open.Core.Common
{
    /// <summary>Manages the storage of auto-properties.</summary>
    public class AutoPropertyManager : DisposableBase
    {
        #region Head
        private readonly object instance;
        private readonly Action<PropertyChangedEventArgs> firePropertyChanged;
        private readonly List<ItemReference> values = new List<ItemReference>();

        /// <summary>Constructor.</summary>
        public AutoPropertyManager(object instance, Action<PropertyChangedEventArgs> firePropertyChanged = null)
        {
            if (instance == null) throw new ArgumentNullException("instance");
            this.instance = instance;
            this.firePropertyChanged = firePropertyChanged;
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            foreach (var item in values)
            {
                item.Dispose();
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
            if (IsDisposed) return defaultValue;
            return GetOrCreate(property, defaultValue).GetValue<TResult>();
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
            var item = GetOrCreate<T, TResult>(propInfo, defaultValue);

            // Don't continue if there is not change in value.
            var currentValue = item.GetValue<TResult>();
            if (Equals(currentValue, value)) return false;

            // Store value.
            item.SetValue<TResult>(value);

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
            value = default(T);
            return false;
        }

        /// <summary>Stores a property value in the dictionary (DO NOT USE unless overriding auto-backing-field store).</summary>
        /// <param name="property">The property being written.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="isDefaultValue">Flag indicating if the given value is the default value.</param>
        protected virtual void OnSetValue<T>(PropertyInfo property, T value, bool isDefaultValue)
        {
        }
        #endregion

        #region Internal
        private ItemReference GetOrCreate<T, TResult>(Expression<Func<T, object>> property, TResult defaultValue)
        {
            return GetOrCreate<T, TResult>(property.GetPropertyInfo(), defaultValue);
        }

        private ItemReference GetOrCreate<T, TResult>(PropertyInfo propInfo, TResult defaultValue)
        {
            // Check if an entry already exists.
            var item = values.FirstOrDefault(m => m.IsMatch(propInfo));
            if (item != null) return item;

            // Create the new entry and store value.
            item = new ItemReference(this, propInfo);
            item.SetValue<TResult>(defaultValue);
            values.Add(item);

            // Finish up.
            return item;
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

        private class ItemReference : DisposableBase
        {
            #region Head
            private AutoPropertyManager parent;
            private PropertyInfo propertyInfo;
            private object value;

            public ItemReference(AutoPropertyManager parent, PropertyInfo propertyInfo)
            {
                this.parent = parent;
                this.propertyInfo = propertyInfo;
            }

            protected override void OnDisposed()
            {
                base.OnDisposed();
                parent = null;
                propertyInfo = null;
                value = null;
            }
            #endregion

            #region Methods
            public T GetValue<T>()
            {
                var defaultValue = default(T);
                return value == null || Equals(value, defaultValue) 
                           ? defaultValue 
                           : (T)value;
            }

            public void SetValue<T>(object newValue)
            {
                // - Call Set Value virtual method on parent.

                var defaultValue = default(T);
                value = newValue == null || Equals(newValue, defaultValue)
                           ? defaultValue
                           : (T)newValue;
            }

            public bool IsMatch(PropertyInfo property)
            {
                return Equals(propertyInfo, property);
            }
            #endregion
        }
    }
}
