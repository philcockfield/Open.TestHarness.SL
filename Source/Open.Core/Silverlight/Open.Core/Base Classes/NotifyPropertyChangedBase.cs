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
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading;

namespace Open.Core.Common
{
    /// <summary>Base class that implements INotifyPropertyChanged.</summary>
    public abstract partial class NotifyPropertyChangedBase : DisposableBase, INotifyPropertyChanged
    {
        #region Head
        /// <summary>Fires when a property on the object has changed value.</summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void FirePropertyChangedEvent(PropertyChangedEventArgs e) { if (PropertyChanged != null) PropertyChanged(this, e); }

        private NotifyPropertyChangedInvoker invoker;

        // NB: Constructor is public so the object can be de-serialized.
        public NotifyPropertyChangedBase()
        {
            // Store a static reference to the sync-context.
            if (SynchronizationContext == null) SynchronizationContext = SynchronizationContext.Current;

            // Wire up events.
            Disposed += HandleDisposed;
        }
        #endregion

        #region Event Handlers
        private void HandleDisposed(object sender, EventArgs e)
        {
            // NB: The event is hooked (rather than overiding OnDisposed) to avoid inconsistent
            // behavior if a deriving class does not call base.OnDisposed() when it overrides.
            Disposed -= HandleDisposed;
            DisposeOfAutoProperties();
        }
        #endregion

        #region Properties - Internal
        /// <summary>Gets the threading synchronization-context.</summary>
        protected static SynchronizationContext SynchronizationContext { get; private set; }

        private NotifyPropertyChangedInvoker Invoker
        {
            get
            {
                if (invoker == null) invoker = new NotifyPropertyChangedInvoker(SynchronizationContext, FirePropertyChangedEvent);
                return invoker;
            }
        }
        #endregion

        #region Methods
        /// <summary>Fires the 'PropertyChanged' event for a collection of properties.</summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new [] {propertyName});
        }

        /// <summary>Fires the 'PropertyChanged' event for a collection of properties.</summary>
        /// <param name="propertyNames">Collection of names of the properties that have changed.</param>
        protected void OnPropertyChanged(params string[] propertyNames)
        {
            Invoker.OnPropertyChanged(propertyNames);
        }

        /// <summary>Fires the 'PropertyChanged' event for a collection of properties.</summary>
        /// <param name="properties">
        ///    The collection of expressions that represent the properties 
        ///    that have changed (for example 'n => n.PropertyName'.)
        /// </param>
        protected void OnPropertyChanged<T>(params Expression<Func<T, object>>[] properties)
        {
            if (properties == null) return;
            foreach (var property in properties)
            {
                OnPropertyChanged(property.GetPropertyName());
            }
        }

        /// <summary>Fires the 'PropertyChanged' event.</summary>
        /// <param name="e">The event args.</param>
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            Invoker.OnPropertyChanged(e);
        }
        #endregion
    }
}
