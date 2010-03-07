using System;

namespace Open.Core
{
    /// <summary>An identifier of an item (use to filter from a MEF [ImportMany]).</summary>
    public interface IIdentifiable
    {
        /// <summary>Gets the key that uniquely identifies the object.</summary>
        Object Key { get; }
    }
}
