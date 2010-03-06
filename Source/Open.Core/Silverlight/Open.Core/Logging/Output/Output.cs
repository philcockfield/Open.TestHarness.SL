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
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Text;


namespace Open.Core.Common
{
    /// <summary>Static access to output writers.</summary>
    public static class Output
    {
        #region Head
        private const string NullText = "<null>";
        private static readonly List<IOutput> writers = new List<IOutput>();

        #endregion

        #region Methods
        /// <summary>Registers a writer to be written to.</summary>
        /// <param name="writer">The writer.</param>
        public static void Register(IOutput writer)
        {
            if (writer == null) return;
            if (!writers.Contains(writer)) writers.Add(writer);
        }

        /// <summary>Unregisters a writer to be written to.</summary>
        /// <param name="writer">The writer.</param>
        public static void Unregister(IOutput writer)
        {
            if (writer == null) return;
            if (writers.Contains(writer)) writers.Remove(writer);
        }
        #endregion

        #region Methods - Write
        /// <summary>Writes the empty line to the log.</summary>
        public static void Write()
        {
            OnAllWriters(writer => writer.Write(""));
        }

        /// <summary>Write the value(s) to a single line, delimited by pipes if a set of values were passed (|).</summary>
        /// <param name="values">The set of values to write.</param>
        public static void Write(params object[] values)
        {
            var msg = ToOutputString(values);
            OnAllWriters(writer => writer.Write(msg));
        }

        /// <summary>Writes the given value to the log.</summary>
        /// <param name="color">The color to associate with the log entry.</param>
        /// <param name="value">The value to write.</param>
        public static void Write(Color color, object value)
        {
            var args = new OutputLine { Value = value, Color = color };
            OnAllWriters(writer => writer.Write(args));
        }

        /// <summary>Writes the given value as a bold title.</summary>
        /// <param name="value">The value to write.</param>
        public static void WriteTitle(object value)
        {
            WriteTitle(Colors.Transparent, value);
        }

        /// <summary>Writes the given value as a bold title.</summary>
        /// <param name="color">The color to associate with the log entry.</param>
        /// <param name="value">The value to write.</param>
        public static void WriteTitle(Color color, object value)
        {
            var args = new OutputLine { Value = value, FontWeight = FontWeights.Bold, Color = color };
            OnAllWriters(writer => writer.Write(args));
        }

        /// <summary>Inserts a line break into the log.</summary>
        public static void Break()
        {
            OnAllWriters(writer => writer.Break());
        }

        /// <summary>Clears the log.</summary>
        public static void Clear()
        {
            OnAllWriters(writer => writer.Clear());
        }
        #endregion

        #region Methods - WriteProperties
        /// <summary>Writes out the properties for the specified object.</summary>
        /// <param name="obj">The object to write the properties of.</param>
        /// <param name="includeHierarchy">Flag indicating whether properties from the entire inheritance tree should be written.</param>
        public static void WriteProperties(object obj, bool includeHierarchy)
        {
            WriteProperties(null, obj, includeHierarchy);
        }

        /// <summary>Writes out the properties for the specified object.</summary>
        /// <param name="title">Title of the object being written.</param>
        /// <param name="obj">The object to write the properties of.</param>
        /// <param name="includeHierarchy">Flag indicating whether properties from the entire inheritance tree should be written.</param>
        public static void WriteProperties(string title, object obj, bool includeHierarchy)
        {
            // Setup initial conditions.
            if (obj == null)
            {
                WriteNull();
                return;
            }

            // Get the properties.
            var flags = BindingFlags.Instance | BindingFlags.Public;
            if (!includeHierarchy) flags = flags | BindingFlags.DeclaredOnly;
            var properties = obj.GetType().GetProperties(flags).OrderBy(m => m.Name);

            // Finish up.
            Write(ToPropertyOutput(title, obj, properties));
        }

        /// <summary>Writes out the specified properties for the given object.</summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to write the properties of.</param>
        /// <param name="properties">
        ///    The collection of expressions that represent the properties 
        ///    that have changed (for example 'n => n.PropertyName'.)
        ///    Passing null writes the entire set of properties (not including the inheritance hierarchy).
        /// </param>
        public static void WriteProperties<T>(T obj, params Expression<Func<T, object>>[] properties)
        {
            WriteProperties(null, obj, properties);
        }

        /// <summary>Writes out the specified properties for the given object.</summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="title">Title of the object being written.</param>
        /// <param name="obj">The object to write the properties of.</param>
        /// <param name="properties">
        ///    The collection of expressions that represent the properties 
        ///    that have changed (for example 'n => n.PropertyName'.)
        ///    Passing null writes the entire set of properties (not including the inheritance hierarchy).
        /// </param>
        public static void WriteProperties<T>(string title, T obj, params Expression<Func<T, object>>[] properties)
        {
            // Setup initial conditions.
            if (Equals(obj, default(T)))
            {
                WriteNull();
                return;
            }
            if (properties == null || properties.Length == 0)
            {
                WriteProperties(title, obj, false);
                return;
            }

            // Get the set of properties.
            var type = obj.GetType();
            var list = new List<PropertyInfo>();
            foreach (var property in properties)
            {
                var info = type.GetProperty(property.GetPropertyName(), BindingFlags.Instance | BindingFlags.Public);
                list.Add(info);
            }

            // Finish up.
            Write(ToPropertyOutput(title, obj, list));
        }
        #endregion

        #region Methods - WriteCollection
        /// <summary>Writes out the given collection items to the log (using ToString to get the output for each line item).</summary>
        /// <typeparam name="T">The type items within the collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="truncateAfter">The maximum number of items to write (output is truncated if exceeds this value).</param>
        public static void WriteCollection<T>(IEnumerable<T> collection, int truncateAfter = int.MaxValue)
        {
            WriteCollection(collection, item => item, truncateAfter: truncateAfter);
        }

        /// <summary>Writes out the given collection items to the log.</summary>
        /// <typeparam name="T">The type items within the collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="formatItem">Function that formats the given line item.</param>
        /// <param name="truncateAfter">The maximum number of items to write (output is truncated if exceeds this value).</param>
        public static void WriteCollection<T>(IEnumerable<T> collection, Func<T, object> formatItem, int truncateAfter = int.MaxValue)
        {
            // Setup initial conditions.
            if (collection == null)
            {
                WriteNull();
                return;
            }
            var count = collection.Count();
            var isTruncated = count > truncateAfter;

            // Prepare the collection list.
            var collectionText = string.Empty;
            if (count > 0)
            {
                var builder = new StringBuilder();
                var index = 0;
                foreach (var item in collection)
                {
                    if (index >= truncateAfter) break;

                    object line = NullText;
                    if (item.GetTypeOrNull() != null)
                    {
                        line = formatItem == null
                                                   ? item.ToString()
                                                   : formatItem(item);
                    }
                    builder.AppendLine(string.Format("   {0}) - {1}", index, line));
                    index++;
                }
                collectionText = Environment.NewLine + builder.ToString().TrimEnd(Environment.NewLine.ToCharArray());
            }

            // Preare the final output.
            var truncatedText = isTruncated ? string.Format(" (showing only {0})", truncateAfter) : null;
            var msg = string.Format("{0} {1}{2}:{3}{4}", 
                            count,
                            "item".ToPlural(count, "items"),
                            isTruncated ? string.Format(" (showing {0} only)", truncateAfter) : null, 
                            collectionText,
                            isTruncated ? Environment.NewLine + "..." : null);

            // Finish up.
            Write(msg);
        }
        #endregion

        #region Internal
        private static void OnAllWriters(Action<IOutput> action)
        {
            foreach (var writer in writers)
            {
                action(writer);
            }
        }

        private static void WriteNull()
        {
            Write(NullText);
        }

        private static string GetValue(PropertyInfo property, object obj)
        {
            var value = property.GetValue(obj, null);
            return value == null ? NullText : value.ToString();
        }

        private static string ToPropertyOutput(string title, object obj, IEnumerable<PropertyInfo> properties)
        {
            // Format the property list.
            var props = "";
            foreach (var property in properties)
            {
                props += string.Format(" - {0}: {1}{2}", property.Name, GetValue(property, obj), Environment.NewLine);
            }
            props = props.TrimEnd(Environment.NewLine.ToCharArray());

            // Finish up.
            return string.Format("{0}{1}",
                                title.AsNullWhenEmpty() == null ? null : title + Environment.NewLine,
                                props);
        }

        private static string ToOutputString(object[] values)
        {
            // Setup initial conditions.
            if (values == null || values.Length == 0) return NullText;
            var msg = "";

            // Build up the pipe delimited output message.
            foreach (var value in values)
            {
                if (value == null) continue;
                msg += value + " | ";
            }
            msg = msg.RemoveEnd(" | ").AsNullWhenEmpty();

            // Finish up.
            return msg;
        }
        #endregion
    }
}
