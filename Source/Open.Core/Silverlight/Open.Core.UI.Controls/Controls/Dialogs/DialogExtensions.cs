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
using System.Windows;
using System.ComponentModel;

namespace Open.Core.UI.Controls.Dialogs
{
    public static class DialogExtensions
    {
        #region Methods
        /// <summary>Reveals the specified exception within an ExceptionDialog content viewer.</summary>
        /// <param name="dialog">The dialog box.</param>
        /// <param name="error">The error being shown.</param>
        /// <returns>The new ExceptionDialog content.</returns>
        public static IExceptionDialog ExceptionDialog(this IAcceptCancelDialog dialog, Exception error)
        {
            // Setup initial conditions.
            if (dialog == null) throw new ArgumentNullException("dialog");

            // Create the exception content.
            var dialogContent = new ExceptionDialog
                                            {
                                                ParentDialog = dialog,
                                                Error = error,
                                            };

            // Setup the parent dialog.
            dialog.ContentMargin = new Thickness(15);
            AssignAsPresenter(dialog, dialogContent);

            // Finish up.
            return dialogContent;
        }

        /// <summary>Reveals the specified content within the dialog box.</summary>
        /// <param name="dialog">The dialog box.</param>
        /// <param name="template">The XAML template to render the content with.</param>
        /// <param name="viewModel">The logical representation of the content.</param>
        /// <param name="width">The pixel width of the dialog.</param>
        /// <param name="height">The pixel height of the dialog.</param>
        /// <returns>The new dialog content.</returns>
        public static IDialogContent ContentDialog(this IAcceptCancelDialog dialog, DataTemplate template, INotifyPropertyChanged viewModel, double width, double height)
        {
            if (dialog == null) throw new ArgumentNullException("dialog");
            return dialog.ContentDialog(new ViewTemplate(template, viewModel), width, height);
        }

        /// <summary>Reveals the specified content within the dialog box.</summary>
        /// <param name="dialog">The dialog box.</param>
        /// <param name="content">The content to show within the dialog.</param>
        /// <param name="width">The pixel width of the dialog.</param>
        /// <param name="height">The pixel height of the dialog.</param>
        /// <returns>The new dialog content.</returns>
        public static IDialogContent ContentDialog(this IAcceptCancelDialog dialog, IViewTemplate content, double width, double height)
        {
            // Setup initial conditions.
            if (dialog == null) throw new ArgumentNullException("dialog");
            if (content == null) throw new ArgumentNullException("content");

            // Create the content view.
            var dialogContent = new DialogContent
                                    {
                                        ParentDialog = dialog,
                                        Width = width,
                                        Height = height,
                                        Content = { Template = content.Template },
                                    };
            if (content.ViewModel != null) dialogContent.Content.ViewModel = content.ViewModel;
            AssignAsPresenter(dialog, dialogContent);

            // Finish up.
            return dialogContent;
        }


        /// <summary>Reveals the specified content within the dialog box.</summary>
        /// <param name="dialog">The dialog box.</param>
        /// <param name="title">The title of the dialog.</param>
        /// <param name="text">The text message.</param>
        /// <returns>The new dialog content.</returns>
        public static IQuestionDialog QuestionDialog(this IAcceptCancelDialog dialog, string title, string text)
        {
            // Setup initial conditions.
            if (dialog == null) throw new ArgumentNullException("dialog");
            dialog.ContentMargin = new Thickness(15);

            // Create the exception content.
            var dialogContent = new QuestionDialog
                                    {
                                        ParentDialog = dialog,
                                        Title = title,
                                        Text = text,
                                    };
            AssignAsPresenter(dialog, dialogContent);

            // Finish up.
            return dialogContent;
        }
        #endregion

        #region Internal
        private static void AssignAsPresenter(IAcceptCancelDialog parentDialog, IAcceptCancelPresenter dialogContent)
        {
            var dialog = parentDialog as AcceptCancelDialogViewModel;
            if (dialog == null) return;
            dialog.AcceptCancelPresenter = dialogContent;
        }
        #endregion
    }
}
