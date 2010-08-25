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
    }
}
