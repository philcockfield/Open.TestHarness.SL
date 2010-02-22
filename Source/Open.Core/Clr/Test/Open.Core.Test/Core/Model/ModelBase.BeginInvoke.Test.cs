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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using System.Threading;

namespace Open.Core.Common.Test.Core.Common.Model
{
    [TestClass]
    public class ModelBaseBeginInvokeTest
    {
        #region Head
        private Stub stub;

        [TestInitialize]
        public void TestSetup()
        {
            stub = new Stub();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldInvokeSynchronously()
        {
            stub.Value.ShouldBe(null);

            ModelBase.IsAsyncEnabled = false;
            stub.BeginInvoke("Value");

            stub.Value.ShouldBe("Value");
        }

        [TestMethod]
        public void ShouldInvokeAsynchronously()
        {
            ModelBase.IsAsyncEnabled = true;

            stub.Value.ShouldBe(null);
            stub.BeginInvoke("Value");
            stub.Value.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldChangeValueAfterDelay()
        {
            stub.Value.ShouldBe(null);
            AsyncTest.Start(test =>
                    {
                        new PropertyObserver<Stub>(stub)
                            .RegisterHandler(m => m.Value, m =>
                                                               {
                                                                   stub.Value.ShouldBe("Value");
                                                                   test.Complete();
                                                               });
                        stub.BeginInvoke("Value");
                    });
        }

        [TestMethod]
        public void ShouldThrowError()
        {
            stub.Error = new Exception("My Error");
            ModelBase.IsAsyncEnabled = false;
            Should.Throw<Exception>(() => stub.BeginInvoke("Value"));
        }

        [TestMethod]
        public void ShouldSuppressErrorSynchronous()
        {
            stub.Error = new Exception("My Error");
            ModelBase.IsAsyncEnabled = false;
            stub.BeginInvoke("Value", args => args.IsHandled = true);
        }

        [TestMethod]
        public void ShouldSuppressErrorAsynchronous()
        {
            stub.Error = new Exception("My Error");

            stub.Value.ShouldBe(null);
            AsyncTest.Start(test => stub.BeginInvoke("Value", args =>
                                                                  {
                                                                      args.IsHandled = true;
                                                                      stub.ErrorThrowCount.ShouldBe(1);
                                                                      test.Complete();
                                                                  }));
        }

        [TestMethod]
        public void ShouldFireEventOnErrorSynchronous()
        {
            stub.Error = new Exception("My Error");
            ModelBase.IsAsyncEnabled = false;

            stub.AsyncError += (s, e) => { e.IsHandled = true; };
            stub.BeginInvoke("Value");
        }

        [TestMethod]
        public void ShouldFireEventOnErrorAsynchronous()
        {
            var error = new Exception("My Error");
            stub.Error = error;

            stub.AsyncError += (s, e) => { e.IsHandled = true; };
            AsyncTest.Start(test => stub.BeginInvoke("Value", args =>
                                                                  {
                                                                      if (args.IsHandled) test.Complete();
                                                                  }));
        }
        #endregion

        #region Stubs
        private class Stub : ModelBase
        {
            #region Properties
            public Exception Error { get; set; }
            public int ErrorThrowCount { get; private set; }

            public string Value
            {
                get { return GetPropertyValue<Stub, string>(m => m.Value); }
                set { SetPropertyValue<Stub, string>(m => m.Value, value); }
            }
            #endregion

            #region Methods
            public void BeginInvoke(string value)
            {
                BeginInvoke(() => Process(value));
            }

            public void BeginInvoke(string value, Action<AsyncCallbackArgs> onComplete)
            {
                BeginInvoke(() => Process(value), onComplete);
            }
            #endregion

            #region Internal

            private void Process(string value)
            {
                if (IsAsyncEnabled) Thread.Sleep(100);
                if (Error != null)
                {
                    ErrorThrowCount++;
                    throw Error;
                }
                Value = value;
            }
            #endregion
        }
        #endregion
    }
}
