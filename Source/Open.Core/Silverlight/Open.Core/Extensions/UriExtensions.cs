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
using System.Collections.Generic;

namespace Open.Core.Common
{
    /// <summary>Extensions for working with URI's.</summary>
    public static class UriExtensions
    {
        /// <summary>Retrieves the query string (as a set of Key:Value pairs).</summary>
        /// <param name="self">The Uri to examine.</param>
        /// <returns>The collection of key-value pairs in the query-string</returns>
        /// <remarks>
        ///     The query string is assumed to start at a '?' character, and be a set of 'key=value' paris
        ///     seperated by the '&' delimiter character.<BR/>
        ///     For example: http://domain.com?a=b&b=c;
        /// </remarks>
        public static IEnumerable<KeyValuePair<string, string>> GetQueryString(this Uri self)
        {
            // Setup initial conditions.
            var empty = new List<KeyValuePair<string, string>>();
            if (self == null) return empty;
            var uri = self.ToString();

            // Retrieve the query-string portion of the string.
            var parts = uri.Split("?".ToCharArray());
            if (parts.Count() < 2) return empty;
            var left = parts[0];
            var queryString = uri.Substring(left.Length + 1, uri.Length - (left.Length + 1));

            // Finish up.
            return queryString.ToKeyValuePairs("=", "&");
        }
    }
}
