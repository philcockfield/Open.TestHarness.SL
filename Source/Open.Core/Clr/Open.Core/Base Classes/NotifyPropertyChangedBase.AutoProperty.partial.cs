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
using System.Linq.Expressions;

namespace Open.Core.Common
{
    /// <summary>The base class for models.</summary>
    /// <remarks>Implements functionality for automatically storing property backing values.</remarks>
    public partial class NotifyPropertyChangedBase
    {
        #region Head
        private AutoPropertyManager propertyManager;
        #endregion

        #region Properties - Internal
        /// <summary>Gets the manager used to store property values.</summary>
        protected virtual AutoPropertyManager Property
        {
            get { return propertyManager ?? (propertyManager = new AutoPropertyManager(OnPropertyChanged)); }
        }
        #endregion

        #region Method - GetPropertyValue (Protected)
        /// <summary>Gets the value for the specified property from the auto-property backing store.</summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="property">An expression representing the property (for example 'n => n.PropertyName').</param>
        protected TResult GetPropertyValue<T, TResult>(Expression<Func<T, object>> property)
        {
            return Property.GetValue<T, TResult>(property);
        }

        /// <summary>Gets the value for the specified property from the auto-property backing store.</summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <typeparam name="TResult">The type of the property return value.</typeparam>
        /// <param name="property">An expression representing the property (for example 'n => n.PropertyName').</param>
        /// <param name="defaultValue">The default value to use if the property has not already been stored.</param>
        protected TResult GetPropertyValue<T, TResult>(Expression<Func<T, object>> property, TResult defaultValue)
        {
            return Property.GetValue(property, defaultValue);
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
            return Property.SetValue(property, value, default(TResult), fireAlso);
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
            return Property.SetValue(property, value, defaultValue, fireAlso);
        }
        #endregion

        #region Internal
        private void DisposeOfAutoProperties()
        {
            if (propertyManager != null) propertyManager.Dispose();
            propertyManager = null;
        }
        #endregion
    }
}
