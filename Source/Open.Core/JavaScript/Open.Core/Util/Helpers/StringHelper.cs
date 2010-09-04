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

        /// <summary>Converts the given string to SentenceCase.</summary>
        /// <param name="value">The value to convert.</param>
        public string ToSentenceCase(string value)
        {
            if (Script.IsUndefined(value)) return value;
            if (string.IsNullOrEmpty(value)) return value;
            return value.Substr(0, 1).ToUpperCase() + value.Substring(1, value.Length);
        }

        /// <summary>Removes the specified text from the end of a string if it's present (not case sensitive).</summary>
        /// <param name="text">The string to effect.</param>
        /// <param name="remove">The text to remove.</param>
        public string RemoveEnd(string text, string remove)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(remove)) return text;
            if (!text.ToLowerCase().EndsWith(remove.ToLowerCase())) return text;
            return text.Substr(0, text.Length - remove.Length);
        }

        /// <summary>Removes the preceeding path of a URL returning just the end segment.</summary>
        /// <param name="url">The URL to process.</param>
        /// <returns></returns>
        public string StripPath(string url)
        {
            if (string.IsNullOrEmpty(url)) return url;
            string[] parts = url.Split("/");
            return parts.Length == 0 ? url : parts[parts.Length - 1];
        }
    }
}
