//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Open.Core.Common
{
    /// <summary>The base class for models.</summary>
    /// <remarks>Implements functionality for automatically storing property backing values.</remarks>
    public partial class NotifyPropertyChangedBase
    {
        #region Head
        private Dictionary<string, object> propertyStore;
        #endregion

        #region Properties - Internal
        private Dictionary<string, object> PropertyStore
        {
            get
            {
                if (propertyStore == null) propertyStore = new Dictionary<string, object>();
                return propertyStore;
            }
        }
        #endregion

        #region Method - GetPropertyValue (Protected)
        /// <summary>Gets the value for the specified property from the auto-property backing store.</summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="property">An expression representing the property (for example 'n => n.PropertyName').</param>
        protected TResult GetPropertyValue<T, TResult>(Expression<Func<T, object>> property)
        {
            if (property == null) throw new ArgumentNullException("property");
            return GetPropertyValue(property.GetPropertyName(), default(TResult));
        }

        /// <summary>Gets the value for the specified property from the auto-property backing store.</summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <typeparam name="TResult">The type of the property return value.</typeparam>
        /// <param name="property">An expression representing the property (for example 'n => n.PropertyName').</param>
        /// <param name="defaultValue">The default value to use if the property has not already been stored.</param>
        protected TResult GetPropertyValue<T, TResult>(Expression<Func<T, object>> property, TResult defaultValue)
        {
            if (property == null) throw new ArgumentNullException("property");
            return GetPropertyValue(property.GetPropertyName(), defaultValue);
        }

        private TResult GetPropertyValue<TResult>(string propertyName, TResult defaultValue)
        {
            // Attempt to read the value.
            TResult value;
            if (ReadPropertyValue(propertyName, out value)) return value;

            // Value not stored already.  Return the default value, and register it in the store (silently, without raising event).
            WritePropertyValue(propertyName, defaultValue, true);
            return defaultValue;
        }
        #endregion

        #region Methods - SetPropertyValue (Protected)
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
        protected bool SetPropertyValue<T, TResult>(Expression<Func<T, object>> property, TResult value, params Expression<Func<T, object>>[] fireAlso)
        {
            return SetPropertyValue(property, value, default(TResult), fireAlso);
        }

        /// <summary>
        ///    Sets the value for the specified property, storing it in the auto-property backing store and
        ///    firing the corresponding 'PropertyChanged' event(s) as required.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <typeparam name="TResult">The type of the property return value.</typeparam>
        /// <param name="property">An expression representing the property (for example 'n => n.PropertyName').</param>
        /// <param name="value">The new value of the property.</param>
        /// <param name="defaultValue">The default value to use if the property has not been stored (used for checking the current value).</param>
        /// <param name="fireAlso">
        ///    A collection of expressions that represent the properties 
        ///    that have also changed as a result of this change and should
        ///    therefore also have their PropertyChanged events fired.
        ///    (for example 'n => n.PropertyName'.)
        /// </param>
        /// <returns>True if the given value was different from the current value (and therefore events were fired), otherwise False.</returns>
        protected bool SetPropertyValue<T, TResult>(Expression<Func<T, object>> property, TResult value, TResult defaultValue, params Expression<Func<T, object>>[] fireAlso)
        {
            // Setup initial conditions.
            if (property == null) throw new ArgumentNullException("property");
            var name = property.GetPropertyName();

            // Retrieve the current value.
            var currentValue = GetPropertyValue(name, defaultValue);
            if (Equals(currentValue, value)) return false;

            // Store the value.
            WritePropertyValue(property.GetPropertyName(), value, false);

            // Alert listeners.
            OnPropertyChanged(name);
            FirePropertyChangedEvents(name, fireAlso);

            // Finish up.
            return true;
        }
        #endregion

        #region Methods - Read/Write (used internally for overriding the data-store).
        /// <summary>Gets a property value in the dictionary (DO NOT USE unless overriding auto-backing-field store).</summary>
        /// <param name="key">The unique identifier of the value.</param>
        /// <param name="value">The variable to return the value within</param>
        /// <returns>True if the value exists within the store, otherwise False.</returns>
        protected virtual bool ReadPropertyValue<T>(string key, out T value)
        {
            object storeValue;
            if (PropertyStore.TryGetValue(key, out storeValue))
            {
                value = (T)storeValue;
                return true;
            }
            value = default(T);
            return false;
        }

        /// <summary>Stores a property value in the dictionary (DO NOT USE unless overriding auto-backing-field store).</summary>
        /// <param name="key">The unique identifier of the value.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="isDefaultValue">Flag indicating if</param>
        protected virtual void WritePropertyValue<T>(string key, T value, bool isDefaultValue)
        {
            PropertyStore[key] = value;
        }
        #endregion

        #region Internal
        private void DisposeOfAutoProperties()
        {
            if (propertyStore == null) return;
            propertyStore.Clear();
        }

        private void FirePropertyChangedEvents<T>(string excludeProperty, params Expression<Func<T, object>>[] properties)
        {
            foreach (var expression in properties)
            {
                var propertyName = expression.GetPropertyName();
                if (propertyName != excludeProperty) OnPropertyChanged(propertyName);
            }
        }
        #endregion
    }
}
