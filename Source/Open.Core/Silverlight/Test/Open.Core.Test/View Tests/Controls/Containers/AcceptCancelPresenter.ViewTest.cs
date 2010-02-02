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

using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.UI.Controls;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Containers
{
    [ViewTestClass]
    public class AcceptCancelPresenterViewTest
    {
        #region Head
        private ViewModelSample viewModel;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(AcceptCancelPresenter control)
        {
            control.Width = 600;
            control.Height = 450;

            viewModel = new ViewModelSample();
            control.ViewModel = viewModel;
            control.Content = new Placeholder();
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Toggle__IsAcceptEnabled(AcceptCancelPresenter control)
        {
            viewModel.IsAcceptEnabled = !viewModel.IsAcceptEnabled;
            Debug.WriteLine("IsAcceptEnabled: " + viewModel.IsAcceptEnabled);
        }

        [ViewTest]
        public void Toggle__IsCancelEnabled(AcceptCancelPresenter control)
        {
            viewModel.IsCancelEnabled = !viewModel.IsCancelEnabled;
            Debug.WriteLine("IsCancelEnabled: " + viewModel.IsCancelEnabled);
        }

        [ViewTest]
        public void Toggle__IsAcceptVisible(AcceptCancelPresenter control)
        {
            viewModel.IsAcceptVisible = !viewModel.IsAcceptVisible;
            Debug.WriteLine("IsAcceptVisible: " + viewModel.IsAcceptVisible);
        }

        [ViewTest]
        public void Toggle__IsCancelVisible(AcceptCancelPresenter control)
        {
            viewModel.IsCancelVisible = !viewModel.IsCancelVisible;
            Debug.WriteLine("IsCancelVisible: " + viewModel.IsCancelVisible);
        }

        [ViewTest]
        public void Change__Content(AcceptCancelPresenter control)
        {
            control.Content = new Placeholder{Color = Colors.Blue};
        }

        [ViewTest]
        public void Remove__Content(AcceptCancelPresenter control)
        {
            control.Content = null;
        }

        [ViewTest]
        public void Toggle__TopShadowOpacity(AcceptCancelPresenter control)
        {
            control.TopShadowOpacity = control.TopShadowOpacity == 0 ? 0.4 : 0;
        }

        [ViewTest]
        public void Labels__Random_Values(AcceptCancelPresenter control)
        {
            viewModel.AcceptLabel = RandomData.LoremIpsum(1, 3);
            viewModel.CancelLabel = RandomData.LoremIpsum(1, 3);
        }

        [ViewTest]
        public void Labels__Reset(AcceptCancelPresenter control)
        {
            viewModel.Reset();
        }
        #endregion

        #region Sample Classes
        private class ViewModelSample : AcceptCancelPresenterViewModel
        {
            protected override void OnAcceptClick()
            {
                Debug.WriteLine("CLICK: Accept");
            }
            protected override void OnCancelClick()
            {
                Debug.WriteLine("CLICK: Cancel");
            }
        }
        #endregion
    }
}
