using System;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with strings.</summary>
    public class StringHelper
    {
        /// <summary>Converts the given string to camelCase.</summary>
        /// <param name="value">The value to convert.</param>
        public string ToCamelCase(string value)
        {
            if (Script.IsUndefined(value)) return value;
            if (string.IsNullOrEmpty(value)) return value;
            return value.Substr(0, 1).ToLowerCase() + value.Substring(1, value.Length);
        }
    }
}
