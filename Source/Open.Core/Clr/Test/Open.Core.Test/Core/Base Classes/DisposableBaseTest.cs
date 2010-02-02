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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Common.Base_Classes
{
    [TestClass]
    public class DisposableBaseTest
    {
        #region Tests

        [TestMethod]
        public void ShouldNotBeDisposedByDefault()
        {
            var stub = new Stub();
            stub.IsDisposed.ShouldBe(false);
        }


        [TestMethod]
        public void ShouldReportIsDisposedAfterDisposal()
        {
            var stub = new Stub();
            stub.Dispose();
            stub.IsDisposed.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldFireDisposedEventOnlyOnce()
        {
            var stub = new Stub();

            var fireCount = 0;
            stub.Disposed += delegate { fireCount++; };

            stub.Dispose();
            stub.Dispose();
            stub.Dispose();

            fireCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldInvokeProtectedOnDisposedMethod()
        {
            var stub = new Stub();
            stub.OnDisposedInvokeCount.ShouldBe(0);

            stub.Dispose();
            stub.Dispose();
            stub.Dispose();

            stub.OnDisposedInvokeCount.ShouldBe(1);
        }
        #endregion

        #region Stubs
        private class Stub : DisposableBase
        {
            public int OnDisposedInvokeCount { get; private set; }

            protected override void OnDisposed()
            {
                base.OnDisposed();
                OnDisposedInvokeCount++;
            }
        }
        #endregion
    }
}
