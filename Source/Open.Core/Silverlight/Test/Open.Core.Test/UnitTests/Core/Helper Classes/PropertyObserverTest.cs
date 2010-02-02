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

using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Helper_Classes
{
    /// <remarks>
    ///    This class only tests the SL specific portions of the partial class.
    ///    The full set of tests for the 'PropertyObserver' are in the Core.Common.Test CLR project.
    /// </remarks>
    [TestClass]
    public class PropertyObserverTest : SilverlightUnitTest
    {
        #region Tests
        [TestMethod]
        public void ShouldRegisterHandler()
        {
            var stub = new Stub();

            var fireCount = 0;
            var observer = new PropertyObserver<Stub>(stub).RegisterHandler(s => s.Text, s => { fireCount++; });

            stub.Text = "Hello";
            fireCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldUnregisterHandler()
        {
            var stub = new Stub();

            var fireCount = 0;
            var observer = new PropertyObserver<Stub>(stub).RegisterHandler(s => s.Text, s => { fireCount++; });
            observer.UnregisterHandler(s => s.Text);

            stub.Text = "Hello";
            fireCount.ShouldBe(0);
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
