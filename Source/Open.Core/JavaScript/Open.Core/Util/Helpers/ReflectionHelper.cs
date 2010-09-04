using System;
using System.Collections;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with reflection.</summary>
    public class ReflectionHelper
    {
        /// <summary>Determines whether the given object is a string.</summary>
        /// <param name="value">The object to examine.</param>
        public bool IsString(object value)
        {
            return value.GetType().Name == "String";
        }

        /// <summary>Determines whether the specified property exists on the object.</summary>
        /// <param name="instance">The object to examine.</param>
        /// <param name="propertyName">The name of the property.</param>
        public bool HasProperty(object instance, string propertyName)
        {
            if (Script.IsNullOrUndefined(instance)) return false;
            propertyName = "get_" + Helper.String.ToCamelCase(propertyName);
            foreach (DictionaryEntry item in Dictionary.GetDictionary(instance))
            {
                if (item.Key == propertyName) return true;
            }
            return false;
        }


        /// <summary>Retrieves the named function from the specified object.</summary>
        /// <param name="source">The source object containing the function.</param>
        /// <param name="name">The name of the function.</param>
        /// <returns>The function, or null if the function could not be found.</returns>
        /// <remarks>
        ///     This method first looks for a 'public' version of the function name, and if not
        ///     found attempts to retrieve the 'internal' version of the function by prepending
        ///     an underscore to the name, eg '_myMethod'.
        /// </remarks>
        public Function GetFunction(object source, string name)
        {
            // Setup initial conditions.
            Dictionary obj = source as Dictionary;
            if (obj == null) return null;
            name = Helper.String.ToCamelCase(name);

            // Look for public function.
            Function func = obj[name] as Function;
            if (!Script.IsNullOrUndefined(func)) return func;

            // Look for private function.
            func = obj["_" + name] as Function;
            if (!Script.IsNullOrUndefined(func)) return func;

            // Finish up.
            return null;
        }
    }
}
