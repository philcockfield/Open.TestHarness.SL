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
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.UI.Controls;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Containers
{
    [ViewTestClass]
    public class Dialog__AcceptCancelDialogViewTest
    {
        #region Head
        private AcceptCancelDialogViewModel viewModel;

        private DataTemplate content1;
        private DataTemplate content2;
        private StubModel contentModel1;
        private StubModel contentModel2;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(AcceptCancelDialog control)
        {
            // Setup initial conditions.
            control.Width = 800;
            control.Height = 600;

            // Get the sample DataTemplates.
            content1 = SampleTemplates.Instance.GetDataTemplate("AcceptCancelDialog.Content1");
            content2 = SampleTemplates.Instance.GetDataTemplate("AcceptCancelDialog.Content2");
            contentModel1 = new StubModel { Text = "My Content Model One" };
            contentModel2 = new StubModel { Text = "My Content Model Two" };

            // Create the view-model.
            viewModel = new AcceptCancelDialogViewModel(content1, contentModel1)
                            {
                                IsShowing = true,
                            };
            control.ViewModel = viewModel;

            // Wire up events.
            viewModel.AcceptClick += delegate { Debug.WriteLine("!! AcceptClick"); };
            viewModel.CancelClick += delegate { Debug.WriteLine("!! CancelClick"); };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Toggle__IsShowing(AcceptCancelDialog control)
        {
            viewModel.IsShowing = !viewModel.IsShowing;
            Debug.WriteLine("IsShowing: " + viewModel.IsShowing);
        }

        [ViewTest]
        public void AnimationDuration_Long(AcceptCancelDialog control)
        {
            viewModel.AnimationDuration = 1;
            Debug.WriteLine("AnimationDuration : " + viewModel.AnimationDuration);
        }

        [ViewTest]
        public void AnimationDuration_Short(AcceptCancelDialog control)
        {
            viewModel.AnimationDuration = 0.2;
            Debug.WriteLine("AnimationDuration : " + viewModel.AnimationDuration);
        }

        [ViewTest]
        public void Toggle__MaskOpacity(AcceptCancelDialog control)
        {
            viewModel.MaskOpacity = viewModel.MaskOpacity == 1 ? 0.7 : 1;
            Debug.WriteLine("MaskOpacity: " + viewModel.MaskOpacity);
        }

        [ViewTest]
        public void Toggle__MaskBrush(AcceptCancelDialog control)
        {
            viewModel.MaskBrush = isBlack ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
            isBlack = !isBlack;
        }
        private bool isBlack;

        [ViewTest]
        public void Toggle__ContentTemplate(AcceptCancelDialog control)
        {
            viewModel.Content.Template = viewModel.Content.Template == content1 ? content2 : content1;
            viewModel.Content.ViewModel = viewModel.Content.ViewModel == content1 ? contentModel1 : contentModel2;
        }

        [ViewTest]
        public void Toggle__IsAcceptEnabled(AcceptCancelDialog control)
        {
            viewModel.IsAcceptEnabled = !viewModel.IsAcceptEnabled;
        }

        [ViewTest]
        public void Toggle__IsCancelEnabled(AcceptCancelDialog control)
        {
            viewModel.IsCancelEnabled = !viewModel.IsCancelEnabled;
        }

        [ViewTest]
        public void Change__Dialog_Labels(AcceptCancelDialog control)
        {
            viewModel.AcceptLabel = RandomData.LoremIpsum(1, 2);
            viewModel.CancelLabel = RandomData.LoremIpsum(1, 2);
        }

        [ViewTest]
        public void Toggle__ContentMargin(AcceptCancelDialog control)
        {
            viewModel.ContentMargin = viewModel.ContentMargin.Left == 0 ? new Thickness(5) : new Thickness(0);
        }

        [ViewTest]
        public void Toggle__AutoHideOnCancel(AcceptCancelDialog control)
        {
            viewModel.AutoHideOnCancel = !viewModel.AutoHideOnCancel;
            Debug.WriteLine("AutoHideOnCancel: " + viewModel.AutoHideOnCancel);
        }

        [ViewTest]
        public void Set_BackgroundColor_Green(AcceptCancelDialog control)
        {
            viewModel.Background = new SolidColorBrush(Colors.Green);
        }
        #endregion

        #region Stubs
        public class StubModel : ModelBase
        {
            public string Text
            {
                get { return GetPropertyValue<StubModel, string>(m => m.Text); }
                set { SetPropertyValue<StubModel, string>(m => m.Text, value); }
            }
        }
        #endregion
    }
}
