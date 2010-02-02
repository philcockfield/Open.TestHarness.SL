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

using System.Linq;
using System.Collections.Generic;
using System.Windows.Browser;

namespace Open.Core.Common
{
    /// <summary>Represents the 'hash' portion of the browser's URL (occuring after the # charachter)</summary>
    public class UrlHash
    {
        #region Properties
        /// <summary>Gets whether there is a URL hash value.</summary>
        public static bool HasValue
        {
            get { return Value != null; }
        }

        /// <summary>Gets or sets the value of the URL hash (without the preceeding '#' character).</summary>
        public static string Value
        {
            get
            {
                var value = ((string)HtmlPage.Window.Eval("document.location.hash")).AsNullWhenEmpty();
                return value == null ? null : value.TrimStart("#".ToCharArray()).AsNullWhenEmpty();
            }
            set
            {
                value = value.AsNullWhenEmpty();
                var eval = string.Format("document.location.hash=\"{0}\"", value);
                HtmlPage.Window.Eval(eval);
            }
        }

        /// <summary>Gets the set of hash values split on the '&' character.</summary>
        public static string[] ValueArray
        {
            get
            {
                var value = Value;
                if (value == null) return new string[] {};
                value = value.TrimEnd(" ".ToCharArray());
                value = value.TrimEnd("&".ToCharArray());
                return value.Split("&".ToCharArray());
            }
        }

        /// <summary>Gets a dictionary of values from the hash-url split on the equals sign (eg. 'key=value').</summary>
        /// <remarks>If the item (sepearted by the '&' character) does not have an '=' character, the entire value is inserted within the key, and the value is null.</remarks>
        public static Dictionary<string, string> ValueDictionary
        {
            get { return GetDictionary(false); }
        }
        #endregion

        #region Methods
        /// <summary>Gets the value corresponding to the given key in a case-insensitive manner (eg. from a URL ending in #key=value).</summary>
        /// <param name="key">The key of the value (not case-sensitive).</param>
        /// <returns>The specified value, or null if it doesn't exist.</returns>
        public static string GetValue(string key)
        {
            if (!HasValue) return null;
            key = key.ToLower();
            var dictionary = GetDictionary(true);
            return dictionary.ContainsKey(key) ? dictionary[key] : null;
        }

        /// <summary>Converts the given dictionary to a URL-hash and replaces the current URL-hash with this value.</summary>
        /// <param name="values">A dictionary of values (like the dictionary retreived from the 'ValueDictionary' property).</param>
        /// <remarks>Use this method, in conjuction with the ValueDictionary to perform non-destructive edits to the URL-hash.</remarks>
        public static void SetValues(Dictionary<string, string> values)
        {
            Value = ToUrlHash(values);
        }

        /// <summary>Adds, or updates, the key-value pair to the URL hash in non-destructive manner.</summary>
        /// <param name="key">The key of the value (case-sensitive).</param>
        /// <param name="value">The value (pass null to remove).</param>
        /// <remarks>Non-destructive means that the existing values within the URL hash are retained.</remarks>
        public static void SetValue(string key, string value)
        {
            // Setup initial conditions.
            var dictionary = ValueDictionary;
            value = value.AsNullWhenEmpty();

            // Update dictionary.
            if (value == null)
            {
                if (dictionary.ContainsKey(key)) dictionary.Remove(key);
            }
            else
            {
                dictionary[key] = value;
            }

            // Finish up.
            SetValues(dictionary);
        }
        #endregion

        #region Internal
        private static Dictionary<string, string> GetDictionary(bool keyToLower)
        {
            var list = new Dictionary<string, string>();
            foreach (var item in ValueArray)
            {
                var pair = item.Split("=".ToCharArray());
                var key = keyToLower ? pair[0].ToLower() : pair[0];
                var value = pair.Length == 1 ? null : pair[1].AsNullWhenEmpty();
                list.Add(key, value);
            }
            return list;
        }

        private static string ToUrlHash(Dictionary<string, string> values)
        {
            var urlHash = "";
            foreach (var item in values)
            {
                urlHash += string.Format("{0}={1}&", item.Key, item.Value);
            }
            urlHash = urlHash.TrimEnd("&".ToCharArray());
            return urlHash;
        }
        #endregion
    }
}
