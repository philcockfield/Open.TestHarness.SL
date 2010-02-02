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
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Open.Core.Common.Collection
{
    /// <summary>Monitors an ObservableCollection for specific events applied by delegates.</summary>
    /// <typeparam name="T">The type of item within the collection.</typeparam>
    public class ObservableCollectionMonitor<T> where T : INotifyPropertyChanged
    {
        #region Head
        private readonly Action<ObservableCollectionMonitor<T>, T> addHandler;
        private readonly Action<ObservableCollectionMonitor<T>, T> removeHandler;

        public ObservableCollectionMonitor(ObservableCollection<T> collection, Action<ObservableCollectionMonitor<T>, T> addHandler, Action<ObservableCollectionMonitor<T>, T> removeHandler)
        {
            // Store values.
            Collection = collection;
            this.addHandler = addHandler;
            this.removeHandler = removeHandler;

            // Wire up events.
            Collection.CollectionChanged += Handle_Collection_CollectionChanged;
            WireCollection(true);
        }
        #endregion

        #region Dispose | Finalize
        ~ObservableCollectionMonitor()
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
                Collection.CollectionChanged -= Handle_Collection_CollectionChanged;
                WireCollection(false);

                // Alert listeners.
                OnDisposed();
            }

            // Finish up.
            IsDisposed = true;
        }

        /// <summary>Gets whether the object has been disposed.</summary>
        public bool IsDisposed { get; private set; }

        /// <summary>Fires when the object has been disposed of (via the 'Dispose' method.  See 'IDisposable' interface).</summary>
        public event EventHandler Disposed;

        private void OnDisposed()
        {
            if (Disposed != null) Disposed(this, new EventArgs());
        }
        #endregion

        #region Event Handlers
        private void Handle_Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    WireEvent(e.NewItems, true);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    WireEvent(e.OldItems, false);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    WireEvent(e.NewItems, true);
                    WireEvent(e.OldItems, false);
                    break;

                case NotifyCollectionChangedAction.Reset: throw new NotSupportedException("Use the 'RemoveAll' extension method to clear the collection.");
                default: throw new ArgumentOutOfRangeException(e.ToString());
            }
        }
        #endregion
        
        #region Properties
        /// <summary>Gets the source collection being monitored.</summary>
        public ObservableCollection<T> Collection { get; private set; }
        #endregion

        #region Internal
        private void WireCollection(bool add)
        {
            foreach (var item in Collection)
            {
                WireEvent(item, add);
            }
        }

        private void WireEvent(IList items, bool add)
        {
            foreach (var item in items)
            {
                WireEvent((T)item, add);
            }
        }

        private void WireEvent(T item, bool add)
        {
            if (add) addHandler(this, item);
            else removeHandler(this, item);
        }
        #endregion
    }
}
