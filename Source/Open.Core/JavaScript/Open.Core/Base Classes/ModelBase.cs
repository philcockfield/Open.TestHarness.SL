using System;
using System.Collections;

namespace Open.Core
{
    /// <summary>Base class for data models.</summary>
    public abstract class ModelBase : IModel, INotifyPropertyChanged, IDisposable, INotifyDisposed
    {
        #region Events
        public event PropertyChangedHandler PropertyChanged;

        public event EventHandler Disposed;
        private void FireDisposed(){if (Disposed != null) Disposed(this, new EventArgs());}
        #endregion

        #region Head
        private bool isDisposed;
        private Dictionary propertyBag;
        private ArrayList propertRefs;
        #endregion

        #region Properties
        public bool IsDisposed { get { return isDisposed; } }

        private Dictionary PropertyBag { get { return propertyBag ?? (propertyBag = new Dictionary()); } }
        private ArrayList PropertyRefs { get { return propertRefs ?? (propertRefs = new ArrayList()); } }
        #endregion

        #region Methods
        /// <summary>Disposes of the object.</summary>
        public void Dispose()
        {
            // Setup initial conditions.
            if (isDisposed) return;

            // Dispose of property-refs.
            Helper.Collection.DisposeAndClear(PropertyRefs);
            FireDisposed();

            // Finish up.
            OnDisposed();
            isDisposed = true;
        }

        /// <summary>Serializes the model to JSON.</summary>
        public virtual string ToJson() { return Helper.Json.Serialize(this); }
        #endregion

        #region Methods : Protected
        /// <summary>Invoked when the model is disposed.</summary>
        protected virtual void OnDisposed() { }

        /// <summary>Fires the 'PropertyChanged' event.</summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected void FirePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(GetPropertyRef(propertyName)));
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
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(value)) value = null;

            // Don't continue if the value has not changed.
            object currentValue = Get(propertyName, defaultValue);
            if (value == currentValue) return false;

            // Store value and fire event.
            PropertyBag[propertyName] = value;
            FirePropertyChanged(propertyName);

            // Finish up.
            return true;
        }

        /// <summary>Retrieves a singleton instance to the handle to the named property.</summary>
        /// <param name="propertyName">The name of the property to retrieve.</param>
        public PropertyRef GetPropertyRef(string propertyName)
        {
            // Return the the property-ref if an instance already exists.
            PropertyRef propertyRef = GetPropertyRefFromList(propertyName);
            if (propertyRef != null) return propertyRef;

            // Don't continue if the property does not exist.
            if (!Helper.Reflection.HasProperty(this, propertyName)) return null;

            // Create a new property-ref.
            propertyRef = new PropertyRef(this, propertyName);
            PropertyRefs.Add(propertyRef);

            // Finish up.
            return propertyRef;
        }
        #endregion

        #region Internal
        private PropertyRef GetPropertyRefFromList(string propertyName)
        {
            if (propertRefs == null) return null;
            foreach (PropertyRef property in PropertyRefs)
            {
                if (property.Name == propertyName) return property;
            }
            return null;
        }
        #endregion
    }
}
