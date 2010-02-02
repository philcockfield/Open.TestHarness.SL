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
using System.Data.Objects;
using System.Linq.Expressions;

namespace Open.Core.Common
{
    public static partial class LinqExtensions
    {
        /// <summary>Specifies the related property to include in the result from the query.</summary>
        /// <typeparam name="T">The type of object being returned in the query.</typeparam>
        /// <param name="self">The query.</param>
        /// <param name="properties">The property(s) to include in the query (for example 'n => n.PropertyName').</param>
        public static ObjectQuery<T> Include<T>(this ObjectQuery<T> self, params Expression<Func<T, object>>[] properties)
        {
            if (self == null) throw new ArgumentNullException("self");
            if (properties == null) throw new ArgumentNullException("properties");
            foreach (var property in properties)
            {
                self = self.Include(property.GetPropertyName());
            }
            return self;
        }
    }
}
