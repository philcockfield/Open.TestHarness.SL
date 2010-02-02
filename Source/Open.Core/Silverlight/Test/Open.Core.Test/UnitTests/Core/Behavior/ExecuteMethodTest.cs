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
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.AttachedBehavior;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests
{
    [TestClass]
    public class ExecuteMethodTest : SilverlightUnitTest
    {
        #region Head
        private StubViewModel stub;
        private Border element;
        private ExecuteMethodStub trigger;
        private ExecuteMethodStub unAttachedTrigger;

        [TestInitialize]
        public void TestSetup()
        {
            stub = new StubViewModel();
            element = new Border{DataContext = stub};

            trigger = new ExecuteMethodStub { MethodName = "IncrementNumber" };
            trigger.Attach(element);

            unAttachedTrigger = new ExecuteMethodStub();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldAttach()
        {
            trigger.AssociatedObject.ShouldBe(element);
            trigger.AssociatedObject.DataContext.ShouldBe(stub);
        }

        [TestMethod]
        public void ShouldExposeDataBoundViewModel()
        {
            trigger.ViewModel.ShouldBe(stub);
            unAttachedTrigger.ViewModel.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldGetNamedMethod()
        {
            trigger.Method.Name.ShouldBe("IncrementNumber");

            trigger.MethodName = "MethodWithOneParameter";
            trigger.Method.Name.ShouldBe("MethodWithOneParameter");
        }

        [TestMethod]
        public void ShouldNotHaveMethod()
        {
            trigger.MethodName = "MethodWithTwoParameters";
            trigger.Method.ShouldBe(null);

            trigger.MethodName = "PrivateMethod";
            trigger.Method.ShouldBe(null);

            trigger.MethodName = "NotAMethod";
            trigger.Method.ShouldBe(null);

            trigger.MethodName = "OverLoad"; // Ambigous.
            trigger.Method.ShouldBe(null);

            trigger.MethodName = "   ";
            trigger.Method.ShouldBe(null);

            trigger.MethodName = null;
            trigger.Method.ShouldBe(null);

            unAttachedTrigger.Method.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldInvokeMethodWithNoParameter()
        {
            stub.Number.ShouldBe(0);
            trigger.Invoke(null);
            stub.Number.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldInvokeMethodWithOneParameter()
        {
            trigger.MethodName = "MethodWithOneParameter";
            stub.ParameterArgs.ShouldBe(null);

            var args = new EventArgs();
            trigger.Invoke(args);
            stub.ParameterArgs.ShouldBe(args);
        }

        [TestMethod]
        public void ShouldDoNothingWhenInvokedWithOutAViewModel()
        {
            unAttachedTrigger.ViewModel.ShouldBe(null);

            stub.Number.ShouldBe(0);
            unAttachedTrigger.Invoke(null);
            stub.Number.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldDoNothingWhenInvokedWithOutIncorrectMethodName()
        {
            trigger.MethodName = "MethodWithTwoParameters";

            stub.Number.ShouldBe(0);
            trigger.Invoke(null);
            stub.Number.ShouldBe(0);
        }
        #endregion

        #region Stubs
        public class StubViewModel : ViewModelBase
        {
            private int number;
            public int Number
            {
                get { return number; }
                set { number = value; OnPropertyChanged<StubViewModel>(m => m.Number); }
            }
            public void IncrementNumber()
            {
                Number++;
            }

            public EventArgs ParameterArgs;
            public void MethodWithOneParameter(EventArgs e) { ParameterArgs = e; }
            public void MethodWithTwoParameters(string text, int value) { }
            private void PrivateMethod() { }

            public void OverLoad() { }
            public void OverLoad(string text) { }
        }

        public class ExecuteMethodStub : ExecuteMethod
        {
            public new FrameworkElement AssociatedObject { get { return base.AssociatedObject; } }
            public new void Invoke(object parameter) { base.Invoke(parameter); }
        }
        #endregion
    }
}
