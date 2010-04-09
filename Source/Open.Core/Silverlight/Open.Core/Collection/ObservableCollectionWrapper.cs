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
    /// <summary>A collection of wrapper objects for an observable collection that keeps itself in sync with it's parent collection.</summary>
    /// <typeparam name="TSource">The type of collection being wrapped.</typeparam>
    /// <typeparam name="TWrapper">The type of wrapper.</typeparam>
    public partial class ObservableCollectionWrapper<TSource, TWrapper> : ObservableCollection<TWrapper>, IDisposable
    {
        #region Events
        /// <summary>Fires when an item is added to the collection.</summary>
        public event EventHandler<ObservableCollectionWrapperEventArgs<TSource, TWrapper>> ItemAdded;
        private void FireItemAdded(TSource source, TWrapper wrapper){if (ItemAdded != null) ItemAdded(this, new ObservableCollectionWrapperEventArgs<TSource, TWrapper>(source, wrapper));}
        
        /// <summary>Fires when an item is removed from the collection.</summary>
        public event EventHandler<ObservableCollectionWrapperEventArgs<TSource, TWrapper>> ItemRemoved;
        private void FireItemRemoved(TSource source, TWrapper wrapper) { if (ItemRemoved != null) ItemRemoved(this, new ObservableCollectionWrapperEventArgs<TSource, TWrapper>(source, wrapper)); }
        #endregion

        #region Head
        private Func<TSource, TWrapper> createWrapper;
        private readonly List<KeyValuePair<TWrapper, TSource>> mapping = new List<KeyValuePair<TWrapper, TSource>>();

        public ObservableCollectionWrapper(ObservableCollection<TSource> source, Func<TSource, TWrapper> createWrapper) : this(source, source, createWrapper)
        {
        }

        protected ObservableCollectionWrapper(IEnumerable<TSource> source, INotifyCollectionChanged collectionChangeNotification, Func<TSource, TWrapper> createWrapper)
        {
            // Setup initial conditions.
            if (source == null) throw new ArgumentNullException("source");
            if (createWrapper == null) throw new ArgumentNullException("source");

            // Store values.
            Source = source;
            CreateWrapper = createWrapper;
            DisposeOfWrapperOnRemoval = true;

            // Add existing items.
            for (var i = 0; i < source.Count(); i++)
            {
                AddWrapperItem(source.ElementAt(i), i);
            }

            // Wire up events.
            collectionChangeNotification.CollectionChanged += OnSourceCollectionChanged;
        }
        #endregion

        #region Dispose | Finalize
        ~ObservableCollectionWrapper()
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
                foreach (var item in this) DisposeOfItem(item);
            }

            // Finish up.
            IsDisposed = true;
        }
        #endregion

        #region Event Handlers
        private void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddWrapperItem((TSource)e.NewItems[0], e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    RemoveWrapperItem(e.OldStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    RemoveWrapperItem(e.NewStartingIndex);
                    AddWrapperItem((TSource)e.NewItems[0], e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Reset: throw new NotSupportedException("Use the 'RemoveAll' extension method to clear the collection.");
                default: throw new ArgumentOutOfRangeException(e.ToString());
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets the source collection that is being wrapped.</summary>
        public IEnumerable<TSource> Source { get; private set; }

        /// <summary>Gets or sets the function used to create a wrapper when the source collection is added to.</summary>
        public Func<TSource, TWrapper> CreateWrapper
        {
            get { return createWrapper; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                createWrapper = value;
            }
        }

        /// <summary>Gets or sets whether the wrapper item should have it's Destroy method invoked when removed from the collection.</summary>
        /// <remarks>For this feature to take effect, the wrapper must implement 'IDestroyable'.</remarks>
        public bool DisposeOfWrapperOnRemoval { get; set; }

        /// <summary>Gets whether the controller has been disposed.</summary>
        public bool IsDisposed { get; private set; }
        #endregion

        #region Methods
        /// <summary>Retrieves the source item corresponding to the given wrapper.</summary>
        /// <param name="wrapper">The wrapper object to look up.</param>
        /// <returns>The corresponding source, otherwise null if the item does not exist within the collection.</returns>
        public TSource GetSource(TWrapper wrapper)
        {
            if (Equals(default(TWrapper), wrapper)) return default(TSource); // Null source passed in.
            var match = mapping.FirstOrDefault(item => item.Key.Equals(wrapper));
            return IsNull(match) ? default(TSource) : match.Value;
        }

        /// <summary>Retrieves the wrapper item corresponding to the given source.</summary>
        /// <param name="source">The source object to look up.</param>
        /// <returns>The corresponding wrapper, otherwise null if the item does not exist within the collection.</returns>
        public TWrapper GetWrapper(TSource source)
        {
            if (Equals(default(TSource), source)) return default(TWrapper); // Null source passed in.
            var match = mapping.FirstOrDefault(item => item.Value.Equals(source));
            return IsNull(match) ? default(TWrapper) : match.Key;
        }

        /// <summary>Determines whether a wrapper for the given source item exists within the collection.</summary>
        /// <param name="source">The source item to look up.</param>
        /// <returns>True if a corresponding wrapper exists, otherwise False.</returns>
        public bool ContainsWrapper(TSource source)
        {
            return ! Equals(default(TWrapper), GetWrapper(source));
        }
        #endregion

        #region Internal
        private void AddWrapperItem(TSource sourceItem, int index)
        {
            var wrapper = CreateWrapper(sourceItem);
            Insert(index, wrapper);
            mapping.Add(new KeyValuePair<TWrapper, TSource>(wrapper, sourceItem));
            FireItemAdded(sourceItem, wrapper);
        }

        private void RemoveWrapperItem(int index)
        {
            var match = mapping[index];

            if (IsNull(match)) return;
            Remove(match.Key);

            mapping.Remove(match);
            if (DisposeOfWrapperOnRemoval) DisposeOfItem(match.Key);

            FireItemRemoved(match.Value, match.Key);
        }

        private static bool IsNull(KeyValuePair<TWrapper, TSource> item)
        {
            return Equals(item, default(KeyValuePair<TWrapper, TSource>));
        }

        private static void DisposeOfItem(object item)
        {
            if (item is IDisposable) ((IDisposable)item).Dispose();
        }
        #endregion
    }
}
