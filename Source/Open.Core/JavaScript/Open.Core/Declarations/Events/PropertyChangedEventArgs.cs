using System;

namespace Open.Core
{
    /// <summary>Event arguments accompanying the 'PropertyChanged' event.</summary>
    public class PropertyChangedEventArgs : EventArgs
    {
        #region Head
        private readonly object instance;
        private readonly string propertyName;
        private string formattedName;

        /// <summary>Constructor.</summary>
        /// <param name="instance">The instance of the object that exposes the property.</param>
        /// <param name="propertyName">The name of the property.</param>
        public PropertyChangedEventArgs(object instance, string propertyName)
        {
            this.instance = instance;
            this.propertyName = propertyName;
        }
        #endregion

        #region Properties
        /// <summary>Gets the instance of the object that exposes the property.</summary>
        public object Instance { get { return instance; } }

        /// <summary>Gets the name of the property.</summary>
        public string PropertyName { get { return propertyName; } }
        private string FormattedName { get { return formattedName ?? (formattedName = Helper.String.ToCamelCase(PropertyName)); } }

        /// <summary>Gets or sets the value of the property.</summary>
        public object PropertyValue
        {
            get { return Type.GetProperty(Instance, FormattedName); }
        }
        #endregion
    }

    /// <summary>Defines the handler for the 'PropertyChanged' event.</summary>
    /// <param name="sender">The object that fired the event.</param>
    /// <param name="e">The event arguments.</param>
    public delegate void PropertyChangedHandler(object sender, PropertyChangedEventArgs e);
}