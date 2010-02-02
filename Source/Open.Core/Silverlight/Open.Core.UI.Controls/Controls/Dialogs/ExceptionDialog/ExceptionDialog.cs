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
using Open.Core.UI.Controls.Assets;

using T = Open.Core.UI.Controls.Dialogs.ExceptionDialog;

namespace Open.Core.UI.Controls.Dialogs
{
    /// <summary>Represents a dialog box for displaying an error.</summary>
    public class ExceptionDialog : DialogContent, IExceptionDialog
    {
        #region Head
        public ExceptionDialog()
        {
            Content.Template = Templates.Instance.GetDataTemplate(typeof (ExceptionDialog).Name);
            Width = 450;
            Height = 300;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the error that is being displayed in the dialog.</summary>
        public Exception Error
        {
            get { return GetPropertyValue<T, Exception>(m => m.Error); }
            set { SetPropertyValue<T, Exception>(m => m.Error, value, m => m.ExceptionText); }
        }

        /// <summary>Gets or sets the error title.</summary>
        public string Title
        {
            get { return GetPropertyValue<T, string>(m => m.Title, StringLibrary.ExceptionDialog_Title); }
            set { SetPropertyValue<T, string>(m => m.Title, value, StringLibrary.ExceptionDialog_Title); }
        }

        /// <summary>Gets the exception as text.</summary>
        public string ExceptionText { get { return CreateErrorText(Error); } }

        /// <summary>Gets or sets the type of icon to display.</summary>
        public NotificationType Icon
        {
            get { return GetPropertyValue<T, NotificationType>(m => m.Icon, NotificationType.Error); }
            set { SetPropertyValue<T, NotificationType>(m => m.Icon, value, NotificationType.Error, m => m.IconSource); }
        }
        public ImageSource IconSource { get { return Icon.ToIcon(); } }
        #endregion

        #region Internal
        private static string CreateErrorText(Exception error)
        {
            if (error == null) return null;

            var inner = "";
            if (error.InnerException != null) inner = string.Format("\r\r------------------------\rInner Exception:\r{0}", CreateErrorText(error.InnerException));

            var stackTrace = "";
            if (error.StackTrace.AsNullWhenEmpty() != null) stackTrace = string.Format("\r\rStack Trace: {0}", error.StackTrace);

            return string.Format("{0} \r{1}{2}{3}",
                                 error.GetType().Name,
                                 error.Message,
                                 stackTrace,
                                 inner);
        }
        #endregion
    }
}
