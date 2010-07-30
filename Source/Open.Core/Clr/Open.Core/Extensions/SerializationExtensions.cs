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
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace Open.Core.Common
{
    public static partial class SerializationExtensions
    {
        #region XML Serialization (DataContract)
        /// <summary>Serializes an object into an XML string.</summary>
        /// <param name="self">The object to serialize.</param>
        /// <returns>The serialized XML.</returns>
        public static string ToSerializedXml(this object self)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractSerializer(self.GetType());
                serializer.WriteObject(stream, self);
                return Encoding.UTF8.GetString(stream.ToArray(), 0, (int)stream.Length);
            }
        }

        /// <summary>Takes a WCF serialized XML string, and deserializes it into an object.</summary>
        /// <param name="serializedXml">The serialized XML string.</param>
        /// <param name="knownTypes">The collection of known types to use when de-serializing.</param>
        /// <returns>The deserialized object.</returns>
        public static T FromSerializedXml<T>(this string serializedXml, params Type[] knownTypes)
        {
            return (T)serializedXml.FromSerializedXml(typeof(T), knownTypes);
        }

        /// <summary>Takes a WCF serialized XML string, and deserializes it into an object.</summary>
        /// <param name="serializedXml">The serialized XML string.</param>
        /// <param name="type">The type of the object within the string.</param>
        /// <param name="knownTypes">The collection of known types to use when de-serializing.</param>
        /// <returns>The deserialized object.</returns>
        public static object FromSerializedXml(this string serializedXml, Type type, params Type[] knownTypes)
        {
            using (var reader = new StringReader(serializedXml))
            {
                var serializer = new DataContractSerializer(type);
                serializer.KnownTypes.AddRange(knownTypes);
                return serializer.ReadObject(XmlReader.Create(reader));
            }
        }
        #endregion

        #region JSON Serialization (DataContract)
        /// <summary>Serializes an object into a JSON string.</summary>
        /// <param name="self">The object to serialize.</param>
        /// <returns>The serialized JSON.</returns>
        public static string ToSerializedJson(this object self)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(self.GetType());
                serializer.WriteObject(stream, self);
                return Encoding.UTF8.GetString(stream.ToArray(), 0, (int)stream.Length);
            }
        }

        /// <summary>Takes a JSON serialized string, and deserializes it into an object.</summary>
        /// <param name="json">The serialized JSON string.</param>
        /// <param name="knownTypes">The collection of known types to use when de-serializing.</param>
        /// <returns>The deserialized object.</returns>
        public static T FromSerializedJson<T>(this string json, params Type[] knownTypes)
        {
            return (T)json.FromSerializedJson(typeof(T), knownTypes);
        }

        /// <summary>Takes a JSON serialized string, and deserializes it into an object.</summary>
        /// <param name="json">The serialized JSON string.</param>
        /// <param name="type">The type of the object within the string.</param>
        /// <param name="knownTypes">The collection of known types to use when de-serializing.</param>
        /// <returns>The deserialized object.</returns>
        public static object FromSerializedJson(this string json, Type type, params Type[] knownTypes)
        {
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(type);
                serializer.KnownTypes.AddRange(knownTypes);
                return serializer.ReadObject(stream);
            }
        }
        #endregion

        #region Base64 and Stream Encoding
        /// <summary>Converts a Base64 encoded string into a bitmap memory stream.</summary>
        /// <param name="self">The string containing the byte data (typically a string created from the 'Convert.ToBase64' method).</param>
        /// <returns></returns>
        public static MemoryStream FromBase64ToStream(this string self)
        {
            self = self.AsNullWhenEmpty();
            if (self == null) return null;

            var buffer = Convert.FromBase64String(self);
            return new MemoryStream(buffer);
        }

        /// <summary>Opens the given file, reads it, and converts it into a Base64 encoded string.</summary>
        /// <param name="self">The file to open and read.</param>
        /// <returns>A Base64 encloded string.</returns>
        public static string ToBase64String(this FileInfo self)
        {
            return self == null ? null : self.OpenRead().ToBase64String();
        }

        /// <summary>Converts a stream to a Base64 encoded string.</summary>
        /// <param name="self">The stream to read.</param>
        /// <returns>A Base64 encloded string.</returns>
        public static string ToBase64String(this Stream self)
        {
            return ToBase64String(self, false);
        }

        /// <summary>Converts a stream to a Base64 encoded string.</summary>
        /// <param name="self">The stream to read.</param>
        /// <param name="closeStream">Flag indicating if the stream should be closed once read.</param>
        /// <returns>A Base64 encloded string.</returns>
        public static string ToBase64String(this Stream self, bool closeStream)
        {
            var respBuffer = new byte[self.Length];

            try
            {
                if (self.CanSeek) self.Position = 0;
                self.Read(respBuffer, 0, respBuffer.Length);
            }
            finally { if (closeStream) self.Close(); }

            return Convert.ToBase64String(respBuffer);
        }
        #endregion

        #region Simple Value Type Seralization
        /// <summary>Converts a Color value to a comma delimited list of ARGB values.</summary>
        /// <param name="self">The color to convert.</param>
        /// <returns>A string of ARGB values (comma delimited).</returns>
        public static string ToColorString(this Color self)
        {
            return string.Format("{0},{1},{2},{3}", self.A, self.R, self.G, self.B);
        }

        /// <summary>Retrieves a Color from a string that has been serialized using the 'ToColorString' method.</summary>
        /// <param name="self">The string to convert.</param>
        /// <returns>A Color.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the string does not contain all ARGB values.</exception>
        public static Color FromColorString(this string self)
        {
            // Setup initial conditions.
            self = self.Replace(" ", null);
            self = self.AsNullWhenEmpty();
            if (self == null) return default(Color);

            // Split the color string.
            var aValues = self.Split(",".ToCharArray());
            if (aValues.Length < 4) throw new ArgumentOutOfRangeException(
                                string.Format("The specified color string '{0}' does not contain all ARGB values.", self));

            // Construct new color.
            return Color.FromArgb(
                                Convert.ToByte(aValues[0]),
                                Convert.ToByte(aValues[1]),
                                Convert.ToByte(aValues[2]),
                                Convert.ToByte(aValues[3]));
        }

        /// <summary>Converts a Thickness value to a comma delimited list numbers.</summary>
        /// <param name="self">The value to convert.</param>
        /// <returns>A set of Thickness values (eg. "1,5,3,6").</returns>
        public static string ToThicknessString(this Thickness self)
        {
            if (self.Left == self.Top && self.Left == self.Right && self.Left == self.Bottom)
            {
                return self.Left.ToString();
            }
            else
            {
                return string.Format("{0},{1},{2},{3}", self.Left, self.Top, self.Right, self.Bottom);
            }
        }

        /// <summary>Retrieves a Thickness from a string that has been serialized using the 'ToThicknessString' method.</summary>
        /// <param name="self">The string to convert.</param>
        /// <returns>A Thickness.</returns>
        public static Thickness FromThicknessString(this string self)
        {
            // Setup initial conditions.
            self = self.AsNullWhenEmpty();
            if (self == null) return new Thickness();

            self = self.Replace(" ", null);
            self = self.Replace(".", ",");
            self = self.AsNullWhenEmpty();
            if (self == null) return default(Thickness);

            // Split the thickness string.
            var aValues = self.Split(",".ToCharArray());

            // Construct new thickness.
            if (aValues.Length == 1)
            {
                return new Thickness(Convert.ToDouble(aValues[0]));
            }
            else
            {
                return new Thickness(
                                        GetArrayValue(aValues, 0, 0),
                                        GetArrayValue(aValues, 1, 0),
                                        GetArrayValue(aValues, 2, 0),
                                        GetArrayValue(aValues, 3, 0));
            }
        }

        private static double GetArrayValue(string[] array, int index, double defaultValue)
        {
            if (index >= array.Length) return defaultValue;
            var item = array[index];
            return item.Length == 0 ? defaultValue : Convert.ToDouble(item);
        }
        #endregion
    }
}
