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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Open.Core.Common
{
    public static partial class StringExtensions
    {
        #region String - General
        /// <summary>
        ///    Replaces the format items in the <see cref="format"/> string
        ///    with the text equivalents of the corresponding arguments.
        /// </summary>
        public static string FormatWith(this string format, params object[] args)
        {
            return String.Format(format, args);
        }

        /// <summary>Formats the underscores of a name (typically a class/member) into spaces and colons.</summary>
        /// <param name="value">The value to convert.</param>
        public static string FormatUnderscores(this string value)
        {
            if (value.AsNullWhenEmpty() == null) return value;
            value = value.Replace("__", ": ");
            value = value.Replace("_", " ");
            return value.Trim();
        }

        /// <summary>Reverses the character order of the specified string.</summary>
        /// <param name="value">The string to reverse.</param>
        public static string Reverse(this string value)
        {
            if (value.AsNullWhenEmpty() == null) return value;
            var array = value.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        /// <summary>Converts the given object to a string, or returns null if the source object is null.</summary>
        /// <param name="value">The object to convert.</param>
        public static string ToStringOrDefault(this object value)
        {
            return value == null ? null : value.ToString();
        }

        /// <summary>Capitalizes the first letter in the string.</summary>
        /// <param name="value">The string to capitalize.</param>
        public static string ToSentenceCase(this string value)
        {
            // Setup initial conditions.
            if (value == null) return value;
            if (value == string.Empty) return value;

            // Capitalize the first letter.
            var firstChar = value.Substring(0, 1);
            value = firstChar.ToUpper() + value.RemoveStart(firstChar);

            // Finish up.
            return value;
        }

        /// <summary>Converts the given work to it's plural form if the count does not equal 1 (or -1).</summary>
        /// <param name="singular">The singular version of the word.</param>
        /// <param name="count">The number to determine plurality with.</param>
        /// <param name="plural">The plural form of the word</param>
        public static string ToPlural(this string singular, double count, string plural)
        {
            return (count == 1 || count == -1) ? singular : plural;
        }

        /// <summary>Retrieves the trailing file-extension on a file name (including the period.  Eg. 'file.doc' return '.doc').</summary>
        /// <param name="self">The file name.</param>
        public static string FileExtension(this string self)
        {
            // Setup initial conditions.
            if (self.AsNullWhenEmpty() == null) return null;

            // Split around period (.).
            var parts = self.Split(".".ToCharArray());
            if (parts.Count() <= 1) return null;

            // Return the last part, with the period.
            var extension = parts[parts.Length - 1].AsNullWhenEmpty();
            return extension == null ? null : "." + extension;
        }

        /// <summary>Parses the given string into a set of key:value pairs.</summary>
        /// <param name="self">The string to parse.</param>
        /// <param name="keyValueDelimiter">The delimiter between the key and value (eg. key=value, or key:value).</param>
        /// <param name="pairDelimiter">The delimiter between each key:value pair (eg. key1=value&key2=value)</param>
        public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs(
            this string self, 
            string keyValueDelimiter = "=", 
            string pairDelimiter = "&" )
        {
            // Setup initial conditions.
            if (keyValueDelimiter.IsNullOrEmpty(true)) throw new ArgumentNullException("keyValueDelimiter");
            if (pairDelimiter.IsNullOrEmpty(true)) throw new ArgumentNullException("pairDelimiter");

            // Check for empty or null string.
            var list = new List<KeyValuePair<string, string>>();
            if (self.IsNullOrEmpty(true)) return list;

            // Split around the pair delimiter.
            foreach (var phrase in self.Split(pairDelimiter.ToCharArray()))
            {
                // Ensure the phrase is not empty.
                if (phrase.IsNullOrEmpty(true)) continue;

                // Spit to the key and value.
                var parts = phrase.Split(keyValueDelimiter.ToCharArray());
                if (parts.Length == 0) continue;

                // Construct the key-value pair.
                string key = "";
                string value = null;
                if (parts.Length > 0) key = parts[0];
                if (parts.Length > 1)
                {
                    value = phrase.Substring(key.Length + 1, phrase.Length - (key.Length + 1));
                }
                list.Add(new KeyValuePair<string, string>(key.AsNullWhenEmpty(), value));
            }

            // Finish up.
            return list;
        }

        /// <summary>Retrieves the end of a string after the last occurance of the given character.</summary>
        /// <param name="self">The string to examine.</param>
        /// <param name="character">The delimiter character.</param>
        /// <example>
        ///     For example:<br/>
        ///     "Namespace.ClassName".SubstringAfterLast(".")<br/>
        ///     Would return "ClassName".
        /// </example>
        public static string SubstringAfterLast(this string self, string character)
        {
            // Setup initial conditions.
            if (self.IsNullOrEmpty(true)) return self;
            if (character.IsNullOrEmpty(false)) return self;
            if (!self.Contains(character)) return self;

            // Split on the character.
            var parts = self.Split(character.ToCharArray());
            return parts[parts.Length - 1];
        }

        /// <summary>Retrieves the beginning of a string up to the last occurance of the given character.</summary>
        /// <param name="self">The string to examine.</param>
        /// <param name="character">The delimiter character.</param>
        /// <example>
        ///     For example:<br/>
        ///     "Namespace.ClassName".SubstringBeforeLast(".")<br/>
        ///     Would return "Namespace".
        /// </example>
        public static string SubstringBeforeLast(this string self, string character)
        {
            // Setup initial conditions.
            if (self.IsNullOrEmpty(true)) return self;
            if (character.IsNullOrEmpty(false)) return self;
            if (!self.Contains(character)) return self;

            // Get last.
            var last = self.SubstringAfterLast(character);
            return self.RemoveEnd(character + last);
        }

        /// <summary>Repeats the character the specified number of times.</summary>
        /// <param name="self">The character to repeat.</param>
        /// <param name="total">The total number of times to repeat the character.</param>
        public static string Repeat(this string self, int total)
        {
            if (string.IsNullOrEmpty(self)) return self;
            if (total <= 0) return self;

            var builder = new StringBuilder(total);
            for (var i = 0; i < total; i++)
            {
                builder.Append(self);
            }
            return builder.ToString();
        }

        #endregion

        #region String - Null Strings
        /// <summary>Converts a string to null if it's empty, or is only white space.</summary>
        /// <param name="self">The string to convert.</param>
        /// <returns>Null if the string was empty (or contained only white space), otherwise the original string.</returns>
        public static string AsNullWhenEmpty(this string self)
        {
            if (self == null) return null;
            var copy = self.Trim(" ".ToCharArray());
            return copy == "" ? null : self;
        }

        /// <summary>Determines whether the string is null or empty.</summary>
        /// <param name="self">The string to examine.</param>
        public static bool IsNullOrEmpty(this string self)
        {
            return IsNullOrEmpty(self, false);
        }

        /// <summary>Determines whether the string is null or empty.</summary>
        /// <param name="self">The string to examine.</param>
        /// <param name="stripWhiteSpace">Flag indicating if white space should be stripped from the string.</param>
        public static bool IsNullOrEmpty(this string self, bool stripWhiteSpace)
        {
            if (stripWhiteSpace) self = self.AsNullWhenEmpty();
            return String.IsNullOrEmpty(self);
        }
        #endregion

        #region String - Stripping
        /// <summary>Strips the given file extension from the string.</summary>
        /// <param name="self">The source string.</param>
        /// <param name="extension">The extension to remove (may or may not have preceeding period (.), case insensitive).</param>
        /// <returns>The stripped string.</returns>
        public static string StripExtension(this string self, string extension)
        {
            // Setup initial conditions.
            if (self == null) return self;
            extension = extension.AsNullWhenEmpty();
            if (extension == null) return self;

            // Ensure it has exactly one period.
            extension = extension.TrimStart(".".ToCharArray());
            extension = ("." + extension).ToLower();

            // Determine if the extension exists.
            var lower = self.ToLower();
            if (!lower.EndsWith(extension)) return self;

            // Remove the extension.
            return self.Substring(0, self.Length - extension.Length);
        }

        /// <summary>Strips the given set of file extension from the string if they are present.</summary>
        /// <param name="self">The source string.</param>
        /// <param name="extensions">The set of extensions to remove (may or may not have preceeding period (.), case insensitive).</param>
        /// <returns>The stripped string.</returns>
        public static string StripExtension(this string self, params string[] extensions)
        {
            if (self == null) return self;
            foreach (var extension in extensions) self = self.StripExtension(extension);
            return self;
        }
        #endregion

        #region String - Contains
        /// <summary>Looks for the occurance of any of the values within the specified filter (not case-sensitive).</summary>
        /// <param name="self">The string to examine.</param>
        /// <param name="filter">The string containing the set of delimited filter values (eg. 'one two three').</param>
        /// <param name="delimiter">The delimiter to split the 'filter' string on (typically a space character).</param>
        /// <returns>True if the string contains at least one occurance of any of the values within the given filter, otherwise False.</returns>
        public static bool ContainsAny(this string self, string filter, string delimiter)
        {
            // Setup initial conditions.
            self = self.AsNullWhenEmpty();
            filter = filter.AsNullWhenEmpty();
            if (self == null || filter == null) return false;
            self = self.ToLower();

            // Enumerate the set of filter items looking for a match.
            foreach (var item in filter.ToLower().Split(delimiter.ToCharArray()))
            {
                if (self.Contains(item)) return true;
            }

            // Finish up.
            return false;
        }

        /// <summary>Looks for the occurance of all values within the specified filter (not case-sensitive).</summary>
        /// <param name="self">The string to examine.</param>
        /// <param name="filter">The string containing the set of delimited filter values (eg. 'one two three').</param>
        /// <param name="delimiter">The delimiter to split the 'filter' string on (typically a space character).</param>
        /// <returns>True if the string contains all occurances of the values within the given filter, otherwise False.</returns>
        public static bool ContainsAll(this string self, string filter, string delimiter)
        {
            // Setup initial conditions.
            self = self.AsNullWhenEmpty();
            filter = filter.AsNullWhenEmpty();
            if (self == null || filter == null) return false;
            self = self.ToLower();

            // Enumerate the set of filter items looking for a match.
            foreach (var item in filter.ToLower().Split(delimiter.ToCharArray()))
            {
                if (!self.Contains(item)) return false;
            }

            // Finish up.
            return true;
        }        
        #endregion

        #region String - Remove Start | End
        /// <summary>Removes the given start value from the string if it exists (not case sensitive).</summary>
        /// <param name="self">The string to remove from.</param>
        /// <param name="start">The starting value to remove, if present.</param>
        /// <returns>The effected string.</returns>
        public static string RemoveStart(this string self, string start) { return RemoveStart(self, start, false); }

        /// <summary>Removes the given start value from the string if it exists.</summary>
        /// <param name="self">The string to remove from.</param>
        /// <param name="start">The starting value to remove, if present.</param>
        /// <param name="caseSensitive">Flag indicating whether the 'start' comparison should be case sensitive.</param>
        /// <returns>The effected string.</returns>
        public static string RemoveStart(this string self, string start, bool caseSensitive)
        {
            if (self == null || start == null) return self;

            string compareSelf;
            string compareStart;
            PrepareRemovePartParams(self, start, caseSensitive, out compareSelf, out compareStart);

            return compareSelf.StartsWith(compareStart)
                       ? self.Remove(0, start.Length)
                       : self;
        }

        /// <summary>Removes the given end value from the string if it exists (not case sensitive).</summary>
        /// <param name="self">The string to remove from.</param>
        /// <param name="end">The ending value to remove, if present.</param>
        /// <returns>The effected string.</returns>
        public static string RemoveEnd(this string self, string end) { return RemoveEnd(self, end, false); }

        /// <summary>Removes the given end value from the string if it exists.</summary>
        /// <param name="self">The string to remove from.</param>
        /// <param name="end">The ending value to remove, if present.</param>
        /// <param name="caseSensitive">Flag indicating whether the 'end' comparison should be case sensitive.</param>
        /// <returns>The effected string.</returns>
        public static string RemoveEnd(this string self, string end, bool caseSensitive)
        {
            if (self == null || end == null) return self;

            string compareSelf; 
            string compareEnd;
            PrepareRemovePartParams(self, end, caseSensitive, out compareSelf, out compareEnd);

            return compareSelf.EndsWith(compareEnd)
                       ? self.Remove(self.Length - end.Length, end.Length)
                       : self;
        }

        private static void PrepareRemovePartParams(string self, string removeValue, bool caseSensitive, out string selfOut, out string removeValueOut)
        {
            selfOut = caseSensitive ? self : self.ToLower();
            removeValueOut = caseSensitive ? removeValue : removeValue.ToLower();
        }
        #endregion

        #region String - IEnumerable
        /// <summary>Converts the collection to a comma seperated strings.</summary>
        /// <typeparam name="T">The type of object within the collection.</typeparam>
        /// <param name="self">The collection.</param>
        /// <param name="convert">Function used to convert each item to a string.</param>
        public static string ToString<T>(this IEnumerable<T> self, Func<T, string> convert)
        {
            return ToString(self, ", ", convert);
        }

        /// <summary>Converts the collection to a delimiter seperated strings.</summary>
        /// <typeparam name="T">The type of object within the collection.</typeparam>
        /// <param name="self">The collection.</param>
        /// <param name="delimiter">The delimiter to use.</param>
        /// <param name="convert">Function used to convert each item to a string.</param>
        public static string ToString<T>(this IEnumerable<T> self, string delimiter, Func<T, string> convert)
        {
            // Setup initial conditions.
            if (self == null) return null;
            if (convert == null) throw new ArgumentNullException("convert");

            // Build the list.
            var sb = new StringBuilder();
            foreach (var item in self)
            {
                sb.Append(convert(item) + delimiter);
            }

            // Finish up.
            return sb.ToString().RemoveEnd(delimiter);
        }

        /// <summary>
        ///     Converts the collection of strings into a string with each item in the collection 
        ///     seperated by a new-line character.
        /// </summary>
        /// <param name="self">The collection to convert.</param>
        /// <param name="newLineCharacter">Option. The new line character to use. Leave null to use default Environment.NewLine</param>
        public static string ToLines(this IEnumerable<string> self, string newLineCharacter = null)
        {
            // Setup initial conditions.
            if (self == null || self.Count() == 0) return null;
            if (newLineCharacter == null) newLineCharacter = Environment.NewLine;

            // Build the string.
            var builder = new StringBuilder();
            foreach (var item in self)
            {
                builder.Append(item + newLineCharacter);
            }

            // Finish up.
            return builder.ToString().RemoveEnd(newLineCharacter);
        }
        #endregion

        #region String - Load Resource
        /// <summary>Loads the file at the given path.</summary>
        /// <param name="path">
        ///    The path from the root of the project to the file to load (must have it's 'Build Action' marked as 'Resource').
        /// </param>
        public static Stream LoadEmbeddedResource(this string path)
        {
            return LoadEmbeddedResource(path, Assembly.GetCallingAssembly());
        }

        /// <summary>Loads the file at the given path.</summary>
        /// <param name="path">
        ///    The path from the root of the project to the file to load (must have it's 'Build Action' marked as 'EmbeddedResource').<BR/>
        ///    For example: /Folder/File.png
        /// </param>
        /// <param name="assembly">The assembly containing the file.</param>
        public static Stream LoadEmbeddedResource(this string path, Assembly assembly)
        {
            // Setup initial conditions.
            if (path.AsNullWhenEmpty() == null) return null;

            // Prepare path.
            path = path.Replace("/", ".");
            path = path.Replace("\\", ".");
            path = path.Replace(" ", "_");
            path = path.RemoveStart(".");

            // Find matching path.
            var fullPath = (from n in assembly.GetManifestResourceNames()
                            where n.EndsWith(path)
                            select n).FirstOrDefault();

            // Retrieve the resource from within the assembly.
            return fullPath == null ? null : assembly.GetManifestResourceStream(fullPath);
        }
        #endregion
    }
}
