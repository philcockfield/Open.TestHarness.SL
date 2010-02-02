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
using Open.Core.Common;
using Open.Core.Common.Network;

namespace Open.Core.UI.Silverlight.Test.Common.Network
{
    public class SampleWebClient : TestableWebClient
    {
        #region Properties
        public Exception Error { get; set; }
        public bool Cancelled { get; set; }
        public Object UserState { get; set; }
        public bool Async { get; set; }
        public double AsyncDelay { get; set; }

        public SampleWebClient()
        {
            AsyncDelay = 0.01;
        }
        #endregion

        #region Methods
        public override void OpenReadAsync(Uri address)
        {
            var stream = new MemoryStream();
            var args = new TestableOpenReadCompletedEventArgs(stream, Error, Cancelled, UserState);
            if (Async)
            {
                DelayedAction.Invoke(AsyncDelay, () => OnOpenReadCompleted(args));
            }
            else
            {
                OnOpenReadCompleted(args);
            }
        }
        #endregion
    }

}
