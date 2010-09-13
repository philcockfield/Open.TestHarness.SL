using System;
using System.Collections;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with collections.</summary>
    public class CollectionHelper
    {
        /// <summary>Converts an enumerable to an ArrayList.</summary>
        /// <param name="collection">The collection to convert.</param>
        /// <returns>An empty ArrayList if 'null' is passed as the source collection.</returns>
        public ArrayList ToArrayList(IEnumerable collection)
        {
            ArrayList list = new ArrayList();
            if (Script.IsNullOrUndefined(collection)) return list;
            foreach (object item in collection)
            {
                list.Add(item);
            }
            return list;
        }

        /// <summary>Constructs a subset of the collection based on the response of an include-filter.</summary>
        /// <param name="collection">The collection to filter.</param>
        /// <param name="predicate">The predicate to match.</param>
        public ArrayList Filter(IEnumerable collection, FuncBool predicate)
        {
            ArrayList list = new ArrayList();
            foreach (object item in collection)
            {
                if (predicate(item)) list.Add(item);
            }
            return list;
        }

        /// <summary>Retrieves the first item that matches the given filter (or null if there is no match).</summary>
        /// <param name="collection">The collection to examine.</param>
        /// <param name="predicate">The predicate to match.</param>
        public object First(IEnumerable collection, FuncBool predicate)
        {
            foreach (object item in collection)
            {
                if (predicate(item)) return item;
            }
            return null;
        }

        /// <summary>Gets the total number of items that match the given predicate.</summary>
        /// <param name="collection">The collection to examine.</param>
        /// <param name="predicate">The predicate to match.</param>
        public int Total(IEnumerable collection, FuncBool predicate)
        {
            if (collection == null) return 0;
            int count = 0;
            foreach (object item in collection)
            {
                if (predicate(item)) count++;
            }
            return count;
        }

        /// <summary>Clears the collection, disposing of all disposable children.</summary>
        /// <param name="collection">The collection to clear and dispose.</param>
        public void DisposeAndClear(ArrayList collection)
        {
            if (collection == null) return;
            foreach (object item in collection)
            {
                IDisposable disposable = item as IDisposable;
                if (disposable != null) disposable.Dispose();
            }
            collection.Clear();
        }
    }
}
