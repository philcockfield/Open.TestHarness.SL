using System;

namespace Open.Core
{
    /// <summary>Base class that implements the IDiposable pattern.</summary>
    public abstract class DisposableBase : INotifyDisposed
    {
        #region Head
        public event EventHandler Disposed;
        private void FireDisposed() { if (Disposed != null) Disposed(this, new EventArgs()); }

        private bool isDisposed;
        #endregion

        #region Properties
        /// <summary>Gets whether the object has been disposed of.</summary>
        public bool IsDisposed { get { return isDisposed; } }
        #endregion

        #region Methods
        /// <summary>Disposes of the object.</summary>
        public void Dispose()
        {
            // Setup initial conditions.
            if (isDisposed) return;

            // Pass execution to derived class.
            OnDisposed();

            // Finish up.
            FireDisposed();
            isDisposed = true;
        }

        /// <summary>Invoked when the model is disposed.</summary>
        protected virtual void OnDisposed() { }
        #endregion
    }
}
