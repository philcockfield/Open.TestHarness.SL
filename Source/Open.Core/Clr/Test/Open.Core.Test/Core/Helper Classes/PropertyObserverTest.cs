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
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Common.Helper_Classes
{
    [TestClass]
    public class PropertyObserverTest
    {
        #region Tests
        [TestMethod]
        public void ShouldThrowWhenNullPassedToConstructor()
        {
            Should.Throw<ArgumentNullException>(()=> new PropertyObserver<Stub>(null));
        }

        [TestMethod]
        public void ShouldHaveNoHandersByDefault()
        {
            var stub = new Stub();
            var observer = new PropertyObserver<Stub>(stub);
            observer.Count.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldRegisterHandler()
        {
            var stub = new Stub();

            var fireCount = 0;
            var observer = new PropertyObserver<Stub>(stub).RegisterHandler(s => s.Text, s => { fireCount++; });
            observer.Count.ShouldBe(1);

            stub.Text = "Hello";
            fireCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldRegisterHandlerWithParameterlessAction()
        {
            var stub = new Stub();

            var fireCount = 0;
            var observer = new PropertyObserver<Stub>(stub).RegisterHandler(s => s.Text, () => { fireCount++; });
            observer.Count.ShouldBe(1);

            stub.Text = "Hello";
            fireCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldRegisterHandlerWithNullCallbackAction()
        {
            var stub = new Stub();
            var observer = new PropertyObserver<Stub>(stub).RegisterHandler(s => s.Text, () => { });
            observer.Count.ShouldBe(1);
            stub.Text = "Hello";
        }

        [TestMethod]
        public void ShouldUnregisterHandler()
        {
            var stub = new Stub();

            var fireCount = 0;
            var observer = new PropertyObserver<Stub>(stub).RegisterHandler(s => s.Text, s => { fireCount++; });
            observer.Count.ShouldBe(1);

            observer.UnregisterHandler(s => s.Text);
            observer.Count.ShouldBe(0);

            stub.Text = "Hello";
            fireCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldHaveFluentApiForRegisteringHandlers()
        {
            var stub = new Stub();

            var fireCount = 0;
            var observer = new PropertyObserver<Stub>(stub)
                .RegisterHandler(s => s.Text, s => { fireCount++; })
                .RegisterHandler(s => s.Number, s => { fireCount++; });

            stub.Text = "Hello";
            stub.Number++;

            fireCount.ShouldBe(2);
        }

        [TestMethod]
        public void ShouldHaveFluentApiForUnregisteringHandlers()
        {
            var stub = new Stub();

            var fireCount = 0;
            var observer = new PropertyObserver<Stub>(stub)
                        .RegisterHandler(s => s.Text, s => { fireCount++; })
                        .RegisterHandler(s => s.Number, s => { fireCount++; });

            observer
                .UnregisterHandler(s => s.Text)
                .UnregisterHandler(s => s.Number);

            stub.Text = "Hello";
            stub.Number++;

            fireCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldThrowArgumentExceptionWhenExpressionNotPassed()
        {
            var stub = new Stub();
            var observer = new PropertyObserver<Stub>(stub);
            Should.Throw<ArgumentException>(() => observer.RegisterHandler(null, s => {}));
        }

        [TestMethod]
        public void ShouldThrowArgumentExceptionWhenExpressionPassedNotReferencingProperty()
        {
            var stub = new Stub();
            var observer = new PropertyObserver<Stub>(stub);
            Should.Throw<ArgumentException>(() => observer.RegisterHandler(m => m.DoSomething(), s => { }));
        }

        [TestMethod]
        public void ShouldUnregisterWhenSourceIsDisposed()
        {
            var stub = new Stub();

            var fireCount = 0;
            var observer = new PropertyObserver<Stub>(stub)
                        .RegisterHandler(s => s.Text, s => { fireCount++; })
                        .RegisterHandler(s => s.Number, s => { fireCount++; });
            observer.Count.ShouldBe(2);

            stub.Text = "Hello";
            stub.Number++;
            fireCount.ShouldBe(2);

            // ---
            fireCount = 0;
            stub.Dispose();
            observer.Count.ShouldBe(0);
            observer.IsDisposed.ShouldBe(true);

            stub.Text = "Goodbye";
            stub.Number++;

            fireCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldDispose()
        {
            var stub = new Stub();

            var fireCount = 0;
            var observer = new PropertyObserver<Stub>(stub).RegisterHandler(s => s.Text, s => { fireCount++; });

            observer.Dispose();
            observer.IsDisposed.ShouldBe(true);

            stub.Text = "Hello";
            fireCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldFireWhenDisposed()
        {
            var stub = new Stub();
            var count = 0;
            stub.Disposed += delegate { count++; };

            stub.Dispose();
            stub.Dispose();
            stub.Dispose();
            count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldReturnPropertySource()
        {
            var stub = new Stub();
            var observer = new PropertyObserver<Stub>(stub);
            observer.PropertySource.ShouldBe(stub);
        }

        [TestMethod]
        public void ShouldNotReturnPropertySource()
        {
            var stub = new Stub();
            var observer = new PropertyObserver<Stub>(stub);

            stub = null;
            GC.Collect();
            observer.PropertySource.ShouldBe(null);
        }

        #endregion

        #region Stubs
        public class Stub : ModelBase
        {
            public const string PropText = "Text";
            public const string PropNumber = "Number";
            private string text;
            private int number;

            public string Text
            {
                get { return text; }
                set { text = value; OnPropertyChanged(PropText); }
            }

            public int Number
            {
                get { return number; }
                set { number = value; OnPropertyChanged(PropNumber); }
            }

            public object DoSomething() { return null; }
        }
        #endregion
    }
}
