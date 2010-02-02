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

namespace Open.Core.Common.Collection
{
    /// <summary>Represents an item within a collection that has either been added or removed.</summary>
    /// <typeparam name="TSource">The type of collection being wrapped.</typeparam>
    /// <typeparam name="TWrapper">The type of wrapper.</typeparam>
    public class ObservableCollectionWrapperEventArgs<TSource, TWrapper> : EventArgs
    {
        #region Head
        public ObservableCollectionWrapperEventArgs(TSource source, TWrapper wrapper)
        {
            Source = source;
            Wrapper = wrapper;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the source collection item.</summary>
        public TSource Source { get; private set; }

        /// <summary>Gets or sets the collection wrapper item.</summary>
        public TWrapper Wrapper { get; private set; }
        #endregion
    }
}
