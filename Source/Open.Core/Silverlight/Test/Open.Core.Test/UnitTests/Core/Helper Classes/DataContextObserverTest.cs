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
using System.Windows.Controls;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Helper_Classes
{
    [Tag("foo")]
    [TestClass]
    public class DataContextObserverTest : SilverlightUnitTest
    {
        #region Head
        private TextBlock element;
        private DataContextObserver observer;
        private int eventCount;

        [TestInitialize]
        public void TestSetup()
        {
            eventCount = 0;
            element = new TextBlock();
            observer = new DataContextObserver(element, () => eventCount++);
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldInvokeActionWhenDataContextChanged()
        {
            element.DataContext = new ViewModelStub();
            eventCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotInvokeWhenDataReferenceIsDead()
        {
            var model = new ViewModelStub();
            element.DataContext = model;
            eventCount.ShouldBe(1);

            observer = null;
            GC.Collect();

            element.DataContext = null;
            eventCount.ShouldBe(1);
        }
        #endregion

        #region Stubs
        private class ViewModelStub : ViewModelBase { }
        #endregion
    }
}
