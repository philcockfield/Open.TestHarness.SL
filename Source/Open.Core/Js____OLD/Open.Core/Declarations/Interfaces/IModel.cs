namespace Open.Core
{
    /// <summary>A logical model.</summary>
    public interface IModel
    {
        /// <summary>Gets whether the model has been disposed..</summary>
        bool IsDisposed { get;  }

        /// <summary>Retrieves the singleton instance of a PropertyRef with the given property name.</summary>
        /// <param name="propertyName">The name of the property to retrieve the instance for.</param>
        /// <remarks>A singleton instance is returned relative to each instance.</remarks>
        PropertyRef GetPropertyRef(string propertyName);

        /// <summary>Serializes the model to JSON.</summary>
        string ToJson();
    }
}
