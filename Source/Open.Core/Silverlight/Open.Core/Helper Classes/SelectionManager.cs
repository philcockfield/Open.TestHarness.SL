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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Open.Core.Common
{
    /// <summary>Flags representing the various types of selection mode the 'SelectionManager' handles.</summary>
    public enum SelectionMode
    {
        /// <summary>Only one item within the collection can be selected at a time.</summary>
        Single
    }

    /// <summary>Manages a collection of ISelectable objects.</summary>
    public class SelectionManager<T> where T : ISelectable, INotifyDisposed
    {
        #region Events
        /// <summary>Fires when the selection changes.</summary>
        public event EventHandler SelectionChanged;
        private void OnSelectionChanged() { if (SelectionChanged != null) SelectionChanged(this, new EventArgs()); }
        #endregion

        #region Head
        public const string PropIsSelected = "IsSelected";

        private bool ignoreSelectionChanged;

        public SelectionManager(ObservableCollection<T> collection)
        {
            // Setup initial conditions.
            if (collection == null) throw new ArgumentNullException("collection");
            Collection = collection;

            // Wire up events.
            WireElements(collection, true);
            Collection.CollectionChanged += Handle_Collection_CollectionChanged;
        }
        #endregion

        #region Dispose | Finalize
        ~SelectionManager()
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
                WireElements(Collection, false);
                Collection.CollectionChanged -= Handle_Collection_CollectionChanged;
            }

            // Finish up.
            IsDisposed = true;
            OnDisposed();
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
                    foreach (T item in e.NewItems) { WireElement(item, true); }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (T item in e.OldItems) { WireElement(item, false); }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (T item in e.OldItems) { WireElement(item, false); } // Unwire the old.
                    foreach (T item in e.NewItems) { WireElement(item, true); } // Wire up the new.
                    break;

                case NotifyCollectionChangedAction.Reset: throw new NotSupportedException("Use the 'RemoveAll' extension method to clear the collection.");
                default: throw new ArgumentOutOfRangeException(e.ToString());
            }
        }

        private void Handle_Element_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Setup initial conditions.
            if (e.PropertyName != PropIsSelected) return;
            if (ignoreSelectionChanged) return;
            ignoreSelectionChanged = true;
            var element = (T)sender;

            // Handle selection behavior based on the current mode.
            switch (SelectionMode)
            {
                case SelectionMode.Single: OnSingleElementSelectionChanged(element); break;
                default: throw new NotSupportedException(SelectionMode.ToString());
            }

            // Finish up.
            ignoreSelectionChanged = false;
            OnSelectionChanged();
        }

        private void OnSingleElementSelectionChanged(T element)
        {
            if (!element.IsSelected) return;
            foreach (var item in SelectedItems)
            {
                if (Equals(item, element)) continue;
                item.IsSelected = false;
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets the collection that is being managed.</summary>
        public ObservableCollection<T> Collection { get; private set; }

        /// <summary>Gets or sets the rule used to determine how selections are managed.</summary>
        public SelectionMode SelectionMode { get; set; }

        /// <summary>Gets the collection of selected items.</summary>
        public IEnumerable<T> SelectedItems { get { return Collection.Where(o => o.IsSelected); } }
        #endregion

        #region Internal
        private static bool IsNull(T element) { return Equals(element, default(T)); }

        private void WireElements(IEnumerable<T> collection, bool addHandler)
        {
            foreach (var item in collection)
            {
                WireElement(item, addHandler);
            }
        }

        private void WireElement(T element, bool addHandler)
        {
            if (IsNull(element)) return;
            if (addHandler)
            {
                element.PropertyChanged += Handle_Element_PropertyChanged;
            }
            else
            {
                element.PropertyChanged -= Handle_Element_PropertyChanged;
            }
        }
        #endregion
    }
}
