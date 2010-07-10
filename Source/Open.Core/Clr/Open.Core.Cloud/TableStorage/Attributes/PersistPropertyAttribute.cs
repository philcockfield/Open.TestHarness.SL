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

namespace Open.Core.Cloud.TableStorage
{
    /// <summary>Declares a property to be persisted to table-storage.</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PersistPropertyAttribute : Attribute
    {
        /// <summary>Gets or sets whether the property maps to the RowKey.</summary>
        public bool IsRowKey { get; set; }

        /// <summary>Gets or sets whether the property maps to the PartitionKey.</summary>
        public bool IsPartitonKey { get; set; }

        /// <summary>Gets or sets the name of the property on the backing entity to map to.</summary>
        public string MapTo { get; set; }

        /// <summary>
        ///     Gets or sets the IConverter to use to convert the value.
        ///     (On the IConverter, TSource is the model property's type, TTarget is the backing-entity property's type.)
        /// </summary>
        public Type Converter { get; set; }
    }
}
