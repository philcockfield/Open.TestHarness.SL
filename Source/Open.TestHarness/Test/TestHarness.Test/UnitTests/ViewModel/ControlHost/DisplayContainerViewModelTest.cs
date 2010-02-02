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
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using Open.Core.UI.Silverlight.Controls;
using Open.TestHarness.Model;
using Open.TestHarness.Test.Model;
using Open.TestHarness.View.ControlHost;

namespace Open.TestHarness.Test.ViewModel.ControlHost
{
    [TestClass]
    public class DisplayContainerViewModelTest : SilverlightTest
    {
        #region Head
        private DisplayContainerViewModel viewModel;
        private ViewTestClass classModel;

        [TestInitialize]
        public void TestInitialize()
        {
            classModel = new ViewTestClass(typeof(SampleViewTestClass1), "File.xap");
            viewModel = new DisplayContainerViewModel(classModel);
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldExposeCurrentControls()
        {
            classModel.CurrentViewTest.ShouldBe(classModel.DefaultViewTest);
            classModel.CurrentViewTest.MethodInfo.Name.ShouldBe(SampleViewTestClass1.PropMethod2);

            viewModel.CurrentControls.Count.ShouldBe(3);
            viewModel.CurrentControls[0].Control.GetType().ShouldBe(typeof(Placeholder));
            viewModel.CurrentControls[1].Control.GetType().ShouldBe(typeof(Border));
            viewModel.CurrentControls[2].Control.GetType().ShouldBe(typeof(Placeholder));

            viewModel.CurrentControls[0].Control.ShouldNotBe(viewModel.CurrentControls[2]);
        }

        [TestMethod]
        public void ShouldChangeCurrentControls()
        {
            PropertyChangedEventArgs args = null;
            viewModel.PropertyChanged += (sender, e) => args = e;

            viewModel.CurrentControls.Count.ShouldBe(3);
            classModel.ViewTests[0].Execute();

            args.PropertyName.ShouldBe(DisplayContainerViewModel.PropCurrentControls);

            viewModel.CurrentControls.Count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldReuseExistingControlInstances()
        {
            var firstInstance = viewModel.CurrentControls[0].Control;
            classModel.ViewTests[0].Execute();
            viewModel.CurrentControls[0].Control.ShouldBe(firstInstance);
        }
        #endregion
    }
}
