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
using System.ComponentModel;
using System.IO;
using System.Net;

namespace Open.Core.Common.Network
{
    /// <summary>A version of 'AsyncCompletedEventArgs' that can be used by the TestableWebClient.</summary>
    public class TestableOpenReadCompletedEventArgs : AsyncCompletedEventArgs
    {
        #region Head
        private readonly OpenReadCompletedEventArgs baseArgs;
        private Stream result;

        public TestableOpenReadCompletedEventArgs(
            Stream result, Exception error, bool cancelled, object userState)
            : base(error, cancelled, userState)
        {
            this.result = result;
        }

        public TestableOpenReadCompletedEventArgs(OpenReadCompletedEventArgs args)
            : base(args.Error, args.Cancelled, args.UserState)
        {
            baseArgs = args;
        }
        #endregion

        #region Properties
        public Stream Result
        {
            get
            {
                if (result == null && baseArgs != null) result = baseArgs.Result;
                return result;
            }
        }
        #endregion
    }
}
