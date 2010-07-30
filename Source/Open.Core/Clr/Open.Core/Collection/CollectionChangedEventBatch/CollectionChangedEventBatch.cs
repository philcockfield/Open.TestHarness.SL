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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Open.Core.Common.Collection
{
    /// <summary>Defines the event raised when a batch of changes have been made to an ObservableCollection.</summary>
    /// <param name="sender">The object that fired the event.</param>
    /// <param name="e">The event arguments.</param>
    public delegate void CollectionChangedEventHandler<T>(object sender, CollectionChangedEventBatchArgs<T> e);

    /// <summary>
    ///    Monitors changes in a collection and reports a set of changes rather than each change individually.
    /// </summary>
    /// <typeparam name="T">The type of items within the collection.</typeparam>
    public class CollectionChangedEventBatch<T> : IDisposable
    {
        #region Head
        /// <summary>Fires when a batch of changes to the collection have occured.</summary>
        public event CollectionChangedEventHandler<T> BatchChange;
        protected void OnBatchChange(CollectionChangedEventBatchArgs<T> e) { if (BatchChange != null) BatchChange(this, e); }

        private readonly List<T> addedItems = new List<T>();
        private readonly List<T> removedItems = new List<T>();
        private readonly List<SwappedCollectionItem<T>> swappedItems = new List<SwappedCollectionItem<T>>();
        private readonly List<NotifyCollectionChangedEventArgs> replacedItems = new List<NotifyCollectionChangedEventArgs>();

        public CollectionChangedEventBatch(ObservableCollection<T> collection)
        {
            Collection = collection;
            collection.CollectionChanged += HandleCollectionChanged;
        }
        #endregion

        #region Dispose | Finalize
        ~CollectionChangedEventBatch()
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
                Collection.CollectionChanged -= HandleCollectionChanged;
            }

            // Finish up.
            IsDisposed = true;
        }

        /// <summary>Gets whether the object has been disposed.</summary>
        public bool IsDisposed { get; private set; }
        #endregion

        #region Event Handlers
        void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged = true;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    addedItems.Add((T)e.NewItems[0]);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    removedItems.Add((T)e.OldItems[0]);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    ProcessReplace(e);
                    break;

                case NotifyCollectionChangedAction.Reset: throw new NotSupportedException("Use the 'RemoveAll' extension method to clear the collection.");
                default: throw new ArgumentOutOfRangeException(e.ToString());
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets the collection being monitored.</summary>
        public ObservableCollection<T> Collection { get; private set; }

        /// <summary>Gets whether the collection has changed since the last batch process.</summary>
        public bool CollectionChanged { get; private set; }

        /// <summary>Gets the collection of items that have been added since the last time 'ProcessBatch' was called.</summary>
        public IEnumerable<T> AddedItems { get { return addedItems; } }

        /// <summary>Gets the collection of items that have been removed since the last time 'ProcessBatch' was called.</summary>
        public IEnumerable<T> RemovedItems { get { return removedItems; } }

        /// <summary>Gets the collection of items that have been swapped around within the colection since the last time 'ProcessBatch' was called.</summary>
        public IEnumerable<SwappedCollectionItem<T>> SwappedItems { get { return swappedItems; } }
        #endregion

        #region Methods
        /// <summary>Processes the batch of changes and fires the 'BatchChange' event if changes have occured.</summary>
        /// <remarks>After this method has run the 'AddedItems', 'RemovedItems' and 'ReplacedItems' properties are reset.</remarks>
        public void ProcessBatch()
        {
            // Setup initial conditions.
            if (! CollectionChanged) return;

            // Prepare the event args and fire the event.
            var args = new CollectionChangedEventBatchArgs<T>
                           {
                               AddedItems = addedItems.ToArray(), 
                               RemovedItems = removedItems.ToArray(),
                               SwappedItems = swappedItems.ToArray()
                           };
            OnBatchChange(args);

            // Finish up.
            ClearCache();
            CollectionChanged = false;
        }
        #endregion

        #region Internal
        private void ClearCache()
        {
            addedItems.Clear();
            removedItems.Clear();
            swappedItems.Clear();
            replacedItems.Clear();
        }

        private void ProcessReplace(NotifyCollectionChangedEventArgs e)
        {
            // Determine if a swap has occured.
            var swap = GetSwapItem(e);
            if (swap != null)
            {
                swappedItems.Add(swap);
                return;
            }

            // Was not a swap operation - populate the Add/Remove (and the reference Replace) lists.
            addedItems.Add((T)e.NewItems[0]);
            removedItems.Add((T)e.OldItems[0]);
            replacedItems.Add(e);
        }

        private SwappedCollectionItem<T> GetSwapItem(NotifyCollectionChangedEventArgs e)
        {
            // Setup initial conditions.
            var newItem = (T)e.NewItems[0];
            if (!removedItems.Contains(newItem)) return null;

            // Look for the old replace event-args (the occured during the first half of the swap operation).
            var firstHalfOfSwapArgs = replacedItems.FirstOrDefault(item => ((T) item.OldItems[0]).Equals(newItem));
            if (firstHalfOfSwapArgs == null) return null;

            // Create the Swap object.
            var swap = new SwappedCollectionItem<T>
                           {
                               Item1 = newItem,
                               Item2 = (T)firstHalfOfSwapArgs.NewItems[0],
                               Item1Index = e.NewStartingIndex,
                               Item2Index = firstHalfOfSwapArgs.NewStartingIndex
                           };

            // Remove the items from the Added/Removed lists, as they are not part of a swap operation.
            // Added.
            addedItems.Remove(swap.Item1);
            addedItems.Remove(swap.Item2);

            // Removed.
            removedItems.Remove(swap.Item1);
            removedItems.Remove(swap.Item2);

            // Replaced.
            replacedItems.Remove(firstHalfOfSwapArgs);

            // Finish up.
            return swap;
        }
        #endregion
    }
}
