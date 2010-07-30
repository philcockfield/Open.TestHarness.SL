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

using System.Windows.Controls;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Helper_Classes
{
    [TestClass]
    public class DataContextPropertyObserverTest
    {
        #region Head
        private TextBlock element;
        private DataContextPropertyObserver<Stub> observer;
        private int eventCount;
        private Stub viewModel;

        [TestInitialize]
        public void TestSetup()
        {
            if (observer != null) observer.Dispose();
            eventCount = 0;

            element = new TextBlock();
            observer = new DataContextPropertyObserver<Stub>(element);

            viewModel = new Stub();
            observer.RegisterHandler(m => m.Text, m => eventCount++);
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldNotInvokeAction()
        {
            viewModel.Text = "Value";
            eventCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldInvokeActionWhenPropertyChangedOnDataContext()
        {
            element.DataContext = viewModel;
            viewModel.Text = "Value";
            eventCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotInvokeActionWhenDataContextRemoved()
        {
            element.DataContext = viewModel;
            viewModel.Text = "One";
            eventCount.ShouldBe(1);

            // ---

            element.DataContext = null;
            viewModel.Text = "Two";
            eventCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotInvokeAfterUnregistered()
        {
            element.DataContext = viewModel;
            observer.UnregisterHandler(m => m.Text);

            viewModel.Text = "Value";
            eventCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldNotInvokeAfterDisposed()
        {
            element.DataContext = viewModel;
            observer.Dispose();

            viewModel.Text = "Value";
            eventCount.ShouldBe(0);
        }

        [Tag("foo")]
        [TestMethod]
        public void ShouldRegisterNewHandler()
        {
            element.DataContext = viewModel;

            var myCount = 0;
            observer.RegisterHandler(m => m.Text, m => myCount++);

            viewModel.Text = "Value";
            eventCount.ShouldBe(0);
            myCount.ShouldBe(1);
        }
        #endregion

        #region Stubs
        private class Stub : ViewModelBase
        {
            public string Text
            {
                get { return GetPropertyValue<Stub, string>(m => m.Text); }
                set { SetPropertyValue<Stub, string>(m => m.Text, value); }
            }
        }
        #endregion
    }
}
