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
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Collections;

namespace Open.Core.Common
{
    public static class CollectionExtensions
    {
        #region Head
        private static readonly Random random = new Random((int)DateTime.Now.Ticks);
        #endregion

        #region Sundry
        /// <summary>Disposes of all items within the collection that implement IDisposable.</summary>
        /// <param name="self">The collection to dispose of.</param>
        public static void DisposeOfChildren(this IEnumerable self)
        {
            if (self == null) return;
            foreach (var item in self)
            {
                if (item == null) continue;
                var disposable = item as IDisposable;
                if (disposable != null) disposable.Dispose();
            }
        }

        /// <summary>
        ///     Syncs two collection, ensuring no items exist in the base-collection that aren't in the given-collection
        ///     and that all items in the given-collection are in the base-collection.
        /// </summary>
        /// <typeparam name="T">The type of items within the collection.</typeparam>
        /// <param name="self">The base collection to update.</param>
        /// <param name="collection">The collection to merge in.</param>
        /// <param name="compare">Predicate used to make comparisons.</param>
        /// <remarks>
        ///     This is useful for syncing an Observable collection that is data-bound with results from a server-call, 
        ///     avoiding clearing and resetting the entire collection, just syncing the specific elements that have changed.
        /// </remarks>
        public static void SyncWith<T>(this ICollection<T> self, IEnumerable<T> collection, Func<T, T, bool> compare)
        {
            // Setup initial conditions.
            if (self == null) throw new ArgumentNullException("self");
            if (collection == null) throw new ArgumentNullException("collection");
            if (compare == null) throw new ArgumentNullException("compare");

            lock(self)
            {
                // Remove items from the base-collection that aren't in the specified collection.
                var removedItems = self.Where(existingItem => !collection.Any(newItem => compare(newItem, existingItem)));
                if (removedItems.Count() > 0)
                {
                    foreach (var item in removedItems.ToArray())
                    {
                        self.Remove(item);
                    }
                }

                // Add items to the base-collection that aren't already there.
                var addedItems = collection.Where(newItem => !self.Any(existingItem => compare(existingItem, newItem)));
                foreach (var item in addedItems)
                {
                    self.Add(item);
                }
            }
        }
        #endregion

        #region IEnumerable
        /// <summary>Determines whether the specified collection is empty.</summary>
        /// <param name="self">The collection to examine.</param>
        public static bool IsEmpty<T>(this IEnumerable<T> self)
        {
            if (Equals(self, default(IEnumerable))) return true;
            return self.Count() == 0;
        }

        /// <summary>Returns the enumerable with any duplicate object instances removed.</summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="self">The collection.</param>
        /// <param name="isMatch">An object comparer.</param>
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> self, Func<T, T, bool> isMatch)
        {
            if (self == null || self.Count() == 0 || isMatch == null) return self;
            var list = new List<T>();
            foreach (var item in self)
            {
                var add = list.All(addedItem => !isMatch(addedItem, item));
                if (add) list.Add(item);
            }
            return list;
        }
        #endregion

        #region ICollection<T>
        /// <summary>Retrieves a random index for the collection.</summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="self">The collection to retreive from.</param>
        /// <returns>A random index within the bounds of the collection.</returns>
        public static int RandomIndex<T>(this ICollection<T> self)
        {
            return random.Next(0, self.Count);
        }

        /// <summary>Retrieves a random item from the collection.</summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="self">The collection to retreive from.</param>
        /// <returns>A random item from the collection.</returns>
        public static T RandomItem<T>(this ICollection<T> self)
        {
            return self.Count == 0 ? default(T) : self.ElementAt(RandomIndex(self));
        }

        /// <summary>Adds a range of items to the end of the collection.</summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection to add to.</param>
        /// <param name="items">The items to add.</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (items ==null) return;
            foreach (var item in items) collection.Add(item);
        }

        /// <summary>Adds a range of items to the end of the collection.</summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection to add to.</param>
        /// <param name="items">The items to add.</param>
        public static void AddRange<T>(this ICollection<T> collection, params T[] items)
        {
            if (items == null) return;
            foreach (var item in items) collection.Add(item);
        }
        #endregion

        #region Observable Collection
        /// <summary>Removes all items from the collection (same effect as calling Clear).</summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection to clear.</param>
        /// <remarks>This method can be used instead of 'Clear' so that listeners to 'CollectionChanged' can react to each individual item removal.</remarks>
        public static void RemoveAll<T>(this ObservableCollection<T> collection)
        {
            if (collection.Count == 0) return;
            do
            {
                collection.Remove(collection[0]);
            } while (collection.Count > 0);
        }
        #endregion

        #region Next | Previous
        /// <summary>Gets the next item in a collection relative to the given object.</summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="relativeTo">The object to retrieve the next item to.</param>
        /// <param name="cycle">Flag indicating if the first item in the collection should be returned if 'relativeTo' is the last item.</param>
        /// <returns>The next item, or null if the last item was given (and 'cycle' was not required).</returns>
        public static T NextItem<T>(this IList<T> collection, T relativeTo, bool cycle)
        {
            return GetRelativeItem(collection, relativeTo, () =>
                                                               {
                                                                   var nextIndex = collection.IndexOf(relativeTo) + 1;
                                                                   if (nextIndex < collection.Count) return nextIndex;
                                                                   return cycle ? 0 : -1;
                                                               });
        }

        /// <summary>Gets the previous item in a collection relative to the given object.</summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="relativeTo">The object to retrieve the previous item to.</param>
        /// <param name="cycle">Flag indicating if the last item in the collection should be returned if 'relativeTo' is the first item.</param>
        /// <returns>The previous item, or null if the first item was given (and 'cycle' was not required).</returns>
        public static T PreviousItem<T>(this IList<T> collection, T relativeTo, bool cycle)
        {
            return GetRelativeItem(collection, relativeTo, () =>
                                                               {
                                                                   var previousIndex = collection.IndexOf(relativeTo) - 1;
                                                                   if (previousIndex >= 0) return previousIndex;
                                                                   return cycle ? collection.Count - 1 : -1;
                                                               });
        }

        private static T GetRelativeItem<T>(IList<T> collection, T relativeTo, Func<int> getRelativeIndex)
        {
            // Setup initial conditions.
            var defaultValue = default(T);
            if (collection == null) return defaultValue;
            if (Equals(relativeTo, defaultValue)) return defaultValue;
            if (!collection.Contains(relativeTo)) return defaultValue;

            // Get the index of the next item.
            var index = getRelativeIndex();

            // Finish up.
            return index < 0 ? defaultValue : collection[index];
        }
        #endregion

        #region Write
        /// <summary>Write the collection to the 'Debug' output log.</summary>
        /// <typeparam name="T">The type of item within the collection.</typeparam>
        /// <param name="collection">The collection to write.</param>
        /// <param name="title">The title to give the collection when writing.</param>
        /// <param name="itemOutput">Output string generater for each item.</param>
        public static void Write<T>(this IEnumerable<T> collection, string title, Func<T, string> itemOutput)
        {
            Debug.WriteLine(title + ": " + collection.Count());
            foreach (var item in collection)
            {
                Debug.WriteLine("> " + itemOutput(item));
            }
            Debug.WriteLine("");
        }
        #endregion
    }
}


