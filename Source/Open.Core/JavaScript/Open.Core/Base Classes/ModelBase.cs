using System;
using System.Collections;

namespace Open.Core
{
    /// <summary>Base class for data models.</summary>
    public abstract class ModelBase : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedHandler PropertyChanged;
        private Dictionary propertyBag;
        #endregion

        #region Properties
        private Dictionary PropertyBag { get { return propertyBag ?? (propertyBag = new Dictionary()); } }
        #endregion

        #region Methods : Protected
        /// <summary>Fires the 'PropertyChanged' event.</summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected void FirePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(this, propertyName));
        }

        /// <summary>Retrieves a property value from the backing store.</summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="defaultValue">The default value to provide (if the value does not exist).</param>
        protected object Get(string propertyName, object defaultValue)
        {
            return PropertyBag.ContainsKey(propertyName) ? PropertyBag[propertyName] : defaultValue;
        }

        /// <summary>
        ///     Stores the given value for the named property 
        ///     (firing the 'PropertyChanged' event if the value differs from the current value).
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="defaultValue">The default value of the property.</param>
        /// <returns>True if the value was different the the current value, otherwise False.</returns>
        protected bool Set(string propertyName, object value, object defaultValue)
        {
            // Don't continue if the value has not changed.
            object currentValue = Get(propertyName, defaultValue);
            if (value == currentValue) return false;

            // Store value and fire event.
            PropertyBag[propertyName] = value;
            FirePropertyChanged(propertyName);

            // Finish up.
            return true;
        }
        #endregion
    }
}
