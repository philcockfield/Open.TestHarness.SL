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

namespace Open.Core.Common
{
    /// <summary>A generic response object to pass back from an asynchronous operation.</summary>
    /// <typeparam name="TResult">The type of the Result object.</typeparam>
    public class Callback<TResult> : ICallback<TResult>
    {
        /// <summary>Gets or sets the result value.</summary>
        public TResult Result { get; set; }

        /// <summary>Gets or sets whether the operation was cancelled.</summary>
        public bool Cancelled { get; set; }

        /// <summary>Gets or sets the error that may have occured during the operation.</summary>
        public Exception Error { get; set; }

        /// <summary>Gets whether the callback contains an error.</summary>
        public bool HasError { get { return Error != null; } }

        /// <summary>Gets whether the operation was successful (there was no error and the operation was not cancelled).</summary>
        public bool IsSuccessful { get { return !HasError && !Cancelled; } }
    }
}
