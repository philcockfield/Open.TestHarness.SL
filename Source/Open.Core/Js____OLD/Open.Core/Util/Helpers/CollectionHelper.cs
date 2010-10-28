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
            if (Script.IsNullOrUndefined(collection)) return null;
            foreach (object item in collection)
            {
                if (predicate(item)) return item;
            }
            return null;
        }

        /// <summary>Retrieves the last item in the given collection (or null if there are no items).</summary>
        /// <param name="collection">The collection to examine.</param>
        public object Last(IEnumerable collection)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(collection)) return null;
            int count = Count(collection);
            if (count == 0) return null;

            // Retrieve the last item.
            ArrayList list = collection as ArrayList;
            if (list != null) return list[count - 1];

            Array array = collection as Array;
            if (array != null) return array[count - 1];

            int currentIndex = 0;
            foreach (object item in collection)
            {
                if (currentIndex == (count - 1)) return item;
                currentIndex++;
            }
            return null;
        }


        /// <summary>Gets the total number of items that match the given predicate.</summary>
        /// <param name="collection">The collection to examine.</param>
        /// <param name="predicate">The predicate to match.</param>
        public int Total(IEnumerable collection, FuncBool predicate)
        {
            if (Script.IsNullOrUndefined(collection)) return 0;
            int count = 0;
            foreach (object item in collection)
            {
                if (predicate(item)) count++;
            }
            return count;
        }

        /// <summary>Gets the total number of items within the given collection.</summary>
        /// <param name="collection">The collection to count.</param>
        public int Count(IEnumerable collection)
        {
            ArrayList list = collection as ArrayList;
            if (list != null) return list.Count;

            Array array = collection as Array;
            if (array != null) return array.Length;

            return Total(collection, delegate(object o) { return true; });
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
