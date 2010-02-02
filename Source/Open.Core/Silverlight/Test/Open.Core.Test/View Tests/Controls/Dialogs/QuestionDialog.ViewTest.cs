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
using Open.Core.Common;
using Open.Core.UI.Controls;
using Open.Core.UI.Controls.Dialogs;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Dialogs
{
    [ViewTestClass]
    public class Dialog__QuestionDialogViewTest : DialogViewTestBase
    {
        #region Head
        private IQuestionDialog questionDialog;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(AcceptCancelDialog control)
        {
            var title = RandomData.LoremIpsum(5, 8);
            var text = RandomData.LoremIpsum(10, 80);

            InitializeDialog(
                control, 
                () => DialogViewModel.QuestionDialog(title, text));

            questionDialog = ContentViewModel as IQuestionDialog;
            questionDialog.Show();
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Change__Title(AcceptCancelDialog control)
        {
            questionDialog.Title = RandomData.LoremIpsum(5, 8);
        }

        [ViewTest]
        public void Change__Text(AcceptCancelDialog control)
        {
            questionDialog.Text = RandomData.LoremIpsum(10, 80);
        }

        [ViewTest]
        public void Change__Icon(AcceptCancelDialog control)
        {
            questionDialog.Icon = questionDialog.Icon.NextValue<NotificationType>();
        }
        #endregion
    }
}
