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

using Open.Core.Common;
using System.Diagnostics;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Behavior
{
    [ViewTestClass]
    public class Behavior__DisposeWithViewModelViewTest
    {
        #region Head
        private Stub viewModel;
        public class Stub : ViewModelBase { }

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(DisposeWithViewModelTestControl control)
        {
            control.Width = 500;
            control.Height = 500;

            viewModel = new Stub();
            control.placeholder.DataContext = viewModel;

            viewModel.Disposed += delegate { Debug.WriteLine("View-Model Disposed"); };
            control.placeholder.Disposed += delegate { Debug.WriteLine("Placeholder Disposed"); };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Dispose_Of_ViewModel(DisposeWithViewModelTestControl control)
        {
            viewModel.Dispose();
        }

        [ViewTest]
        public void Change_ViewModel(DisposeWithViewModelTestControl control)
        {
            viewModel= new Stub();
            control.placeholder.DataContext = viewModel;
        }
        #endregion
    }
}
