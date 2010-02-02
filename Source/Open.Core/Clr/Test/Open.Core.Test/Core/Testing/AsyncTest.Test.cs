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
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Testing
{
    [TestClass]
    public class AsyncTestTest
    {


        [TestMethod]
        public void ShouldRunAsyncTest()
        {
            var stub = new Stub();

            var eventCount = 0;
            stub.Done += delegate { eventCount++; };

            AsyncTest.Start(test =>
                                {
                                    stub.Done += delegate
                                                     {
                                                         eventCount.ShouldBe(1);
                                                         test.Complete();
                                                     };
                                    stub.Begin();
                                });
        }

        [TestMethod]
        public void ShouldRunAsyncTest_AlternateUsageStyle()
        {
            Action<AsyncTest> onStart = test =>
                        {
                            var stub = new Stub();
                            var eventCount = 0;
                            stub.Done += delegate { eventCount++; };

                            stub.Done += delegate
                                            {
                                                eventCount.ShouldBe(1);
                                                test.Complete();
                                            };
                            stub.Begin();
                        };
            AsyncTest.Start(onStart);
        }


        private class Stub : ModelBase
        {
            public event EventHandler Done;
            private void OnDone(){if (Done != null) Done(this, new EventArgs());}

            public void Begin()
            {
                // Start an async operation that calls fires 'Done' event.
                BeginInvoke(OnDone);
            }
        }

    }
}
