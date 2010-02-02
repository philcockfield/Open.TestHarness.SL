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
using System.Net;

namespace Open.Core.Common.Network
{
    /// <summary>An wrapper of the web-client that provides virtual methods so that they may be overridden by unit-tests.</summary>
    /// <remarks>See: http://msmvps.com/blogs/theproblemsolver/archive/2008/12/07/unit-testing-in-silverlight-part-2.aspx</remarks>
    public class TestableWebClient
    {
        #region Head
        public event EventHandler<TestableOpenReadCompletedEventArgs> OpenReadCompleted;

        public virtual void OpenReadAsync(Uri address)
        {
            var client = new WebClient();
            client.OpenReadCompleted += (s, e) => OnOpenReadCompleted(e);
            client.OpenReadAsync(address);
        }
        #endregion

        #region Methods - Protected
        protected void OnOpenReadCompleted(OpenReadCompletedEventArgs e)
        {
            var args = new TestableOpenReadCompletedEventArgs(e);
            OnOpenReadCompleted(args);
        }

        protected void OnOpenReadCompleted(TestableOpenReadCompletedEventArgs e)
        {
            if (OpenReadCompleted != null)
            {
                OpenReadCompleted(this, e);
            }
        }
        #endregion
    }
}
