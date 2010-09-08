using System;

namespace Open.Core
{
    /// <summary>Provides notification of when an object is disposed.</summary>
    public interface INotifyDisposed
    {
        /// <summary>Fires when the object is disposed.</summary>
        event EventHandler Disposed;

        /// <summary>Gets whether the view has been disposed of.</summary>
        bool IsDisposed { get; }
    }
}
