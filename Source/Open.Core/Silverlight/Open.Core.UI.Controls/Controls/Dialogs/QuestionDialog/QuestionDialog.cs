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

using System.Windows.Media;
using Open.Core.UI.Controls.Assets;

using T = Open.Core.UI.Controls.Dialogs.QuestionDialog;

namespace Open.Core.UI.Controls.Dialogs
{
    /// <summary>Represents a dialog box for displaying a simple question.</summary>
    public class QuestionDialog : DialogContent, IQuestionDialog
    {
        #region Head
        public QuestionDialog()
        {
            Content.Template = Templates.Instance.GetDataTemplate(typeof(QuestionDialog).Name);
            Width = 450;
            Height = 250;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the title.</summary>
        public string Title
        {
            get { return GetPropertyValue<T, string>(m => m.Title, StringLibrary.ExceptionDialog_Title); }
            set { SetPropertyValue<T, string>(m => m.Title, value, StringLibrary.ExceptionDialog_Title); }
        }

        /// <summary>Gets or sets the question text.</summary>
        public string Text
        {
            get { return GetPropertyValue<T, string>(m => m.Text); }
            set { SetPropertyValue<T, string>(m => m.Text, value); }
        }

        /// <summary>Gets or sets the type of icon to display.</summary>
        public NotificationType Icon
        {
            get { return GetPropertyValue<T, NotificationType>(m => m.Icon, NotificationType.Question); }
            set { SetPropertyValue<T, NotificationType>(m => m.Icon, value, NotificationType.Question, m => m.IconSource); }
        }

        public ImageSource IconSource { get { return Icon.ToIcon(); } }
        #endregion
    }
}
