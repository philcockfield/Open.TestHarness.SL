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
using System.Runtime.Serialization;

namespace Open.Core.Common
{
    /// <summary>Base class that implements the IDisposable pattern.</summary>
    [DataContract]
    public abstract class DisposableBase : INotifyDisposed
    {
        #region Events
        /// <summary>Fires when the object has been disposed of (via the 'Dispose' method.  See 'IDisposable' interface).</summary>
        public event EventHandler Disposed;
        private void FireDisposed() { if (Disposed != null) Disposed(this, new EventArgs()); }
        #endregion

        #region Head
        // NB: Constructor is public so the object can be de-serialized.
        public DisposableBase() : base() { }
        #endregion

        #region Dispose | Finalize
        ~DisposableBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            // Setup initial conditions.
            if (IsDisposed) return;

            // Perform disposal or managed resources.
            if (isDisposing)
            {
                // Dispose of managed resources.
                OnDisposed();

                // Alert listeners.
                FireDisposed();
            }

            // Finish up.
            IsDisposed = true;
        }
        #endregion

        #region Properties
        /// <summary>Gets whether the object has been disposed.</summary>
        public bool IsDisposed { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        ///    Invoked when the Dispose method is called.  
        ///    Use this to dispose of any managed resources or unwire from events etc.
        /// </summary>
        /// <remarks>
        ///    This is a convenience method that is called from the main 'Dispose' virtual method.
        /// </remarks>
        protected virtual void OnDisposed() { }
        #endregion
    }
}
