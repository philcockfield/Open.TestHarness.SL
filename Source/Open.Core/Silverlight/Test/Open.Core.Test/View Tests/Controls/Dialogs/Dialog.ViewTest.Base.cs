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
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.UI.Controls;
using Open.Core.UI.Controls.Dialogs;
using System.Diagnostics;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Dialogs
{
    public abstract class DialogViewTestBase
    {
        #region Head

        protected DialogViewTestBase()
        {
            DialogViewModel = new AcceptCancelDialogViewModel();
        }
        #endregion

        #region Methods
        public void InitializeDialog(AcceptCancelDialog dialog, Func<IDialogContent> getContentViewModel)
        {
            // Setup control.
            dialog.Width = 800;
            dialog.Height = 600;
            dialog.Background = Colors.Black.ToBrush(0.05);

            // Assign view-model to root dialog.
            dialog.ViewModel = DialogViewModel;

            // Initialize the child content.
            ContentViewModel = getContentViewModel();

            // Wire up events.
            ContentViewModel.Hidden += delegate { Debug.WriteLine("!! Hidden | Result: " + ContentViewModel.Result); };
            ContentViewModel.Shown += delegate { Debug.WriteLine("!! Shown | Result: " + ContentViewModel.Result); };
        }
        #endregion

        #region Properties
        /// <summary>Gets the dialog view-model.</summary>
        public IAcceptCancelDialog DialogViewModel { get; private set; }

        /// <summary>Gets the embedded content view-model.</summary>
        public IDialogContent ContentViewModel { get; private set; }
        #endregion

        #region Tests
        [ViewTest]
        public void Toggle_IsShowing(AcceptCancelDialog control)
        {
            DialogViewModel.IsShowing = !DialogViewModel.IsShowing;
        }

        [ViewTest]
        public void Toggle__Width(AcceptCancelDialog control)
        {
            if (ContentViewModel == null) return;
            ContentViewModel.Width = ContentViewModel.Width == DialogContent.DefaultWidth
                                         ? 750
                                         : DialogContent.DefaultWidth;
        }

        [ViewTest]
        public void Toggle__Height(AcceptCancelDialog control)
        {
            if (ContentViewModel == null) return;
            ContentViewModel.Height = ContentViewModel.Height == DialogContent.DefaultHeight
                                          ? 550
                                          : DialogContent.DefaultHeight;
        }

        [ViewTest]
        public void Show_With_Callback(AcceptCancelDialog control)
        {
            if (ContentViewModel == null) return;
            ContentViewModel.Show(result => Debug.WriteLine("Show Callback.  Result: " + result));
        }

        #endregion
    }
}
