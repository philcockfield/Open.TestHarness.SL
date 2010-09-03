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
        public ArrayList Filter(IEnumerable collection, IsMatch predicate)
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
        public object First(IEnumerable collection, IsMatch predicate)
        {
            foreach (object item in collection)
            {
                if (predicate(item)) return item;
            }
            return null;
        }
    }
}
