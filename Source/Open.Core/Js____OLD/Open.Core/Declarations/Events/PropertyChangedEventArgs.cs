using System;

namespace Open.Core
{
    /// <summary>Event arguments accompanying the 'PropertyChanged' event.</summary>
    public class PropertyChangedEventArgs : EventArgs
    {
        #region Head
        private readonly PropertyRef property;

        /// <summary>Constructor.</summary>
        /// <param name="property">The property that has changed.</param>
        public PropertyChangedEventArgs(PropertyRef property)
        {
            this.property = property;
        }
        #endregion

        #region Properties
        /// <summary>Gets the reference to the property that has changed.</summary>
        public PropertyRef Property{get { return property; }}
        #endregion
    }

    /// <summary>Defines the handler for the 'PropertyChanged' event.</summary>
    /// <param name="sender">The object that fired the event.</param>
    /// <param name="e">The event arguments.</param>
    public delegate void PropertyChangedHandler(object sender, PropertyChangedEventArgs e);
}