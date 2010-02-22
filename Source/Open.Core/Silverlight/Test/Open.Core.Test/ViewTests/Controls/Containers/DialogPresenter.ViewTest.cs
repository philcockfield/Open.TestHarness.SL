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
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.UI.Controls;
using System;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Containers
{
    [ViewTestClass]
    public class Dialog__DialogPresenterViewTest
    {
        #region Head
        private readonly Placeholder childContent = new Placeholder { IsTabStop = true };
        private readonly AcceptCancelPresenter dialogContent = new AcceptCancelPresenter { Width = 500, Height = 350 };
        private readonly SampleDialogModel dialogViewModel = new SampleDialogModel();
        private int focusCount;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(DialogPresenter control)
        {
            control.Width = 800;
            control.Height = 600;
            control.Background = Colors.Black.ToBrush(0.05);
            
            // Setup content.
            dialogContent.DataContext = dialogViewModel;
            dialogContent.Content = childContent;
            control.Content = dialogContent;

            // Wire up events.
            dialogContent.GotFocus += delegate { Debug.WriteLine("Content GOT FOCUS"); };
            childContent.GotFocus += delegate
                                         {
                                             focusCount++;
                                             childContent.Text = "GOT FOCUS: " + focusCount;
                                         };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Toggle_IsShowing(DialogPresenter control)
        {
            control.IsShowing = !control.IsShowing;
            Debug.WriteLine("IsShowing: " + control.IsShowing);
        }

        [ViewTest]
        public void Change_Content_to_Placeholder(DialogPresenter control)
        {
            control.Content = new Placeholder {Color = Colors.Blue};
        }

        [ViewTest]
        public void Change_Content_to_AcceptCancelPanel(DialogPresenter control)
        {
            control.Content = dialogContent;
        }

        [ViewTest]
        public void MaskBrush_Black(DialogPresenter control)
        {
            control.MaskBrush = new SolidColorBrush(Colors.Black);
            control.MaskOpacity = 0.5;
            Debug.WriteLine(string.Format("MaskBrush: Black; Opacity: {0}", control.MaskOpacity));
        }

        [ViewTest]
        public void MaskBrush_White(DialogPresenter control)
        {
            control.MaskBrush = new SolidColorBrush(Colors.White);
            control.MaskOpacity = 0.7;
            Debug.WriteLine(string.Format("MaskBrush: White; Opacity: {0}", control.MaskOpacity));
        }
        #endregion

        #region Sample Data
        private class SampleDialogModel : AcceptCancelPresenterViewModel
        {
            
        }
        #endregion
    }
}
