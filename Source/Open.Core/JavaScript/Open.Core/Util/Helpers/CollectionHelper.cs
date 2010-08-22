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
    }
}
