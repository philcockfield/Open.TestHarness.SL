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

namespace Open.Core.UI.Controls.Dialogs
{
    /// <summary>Content view-model for a dialog box.</summary>
    public interface IDialogContent : IAcceptCancelPresenter
    {
        /// <summary>Fires when the dialog is hidden.</summary>
        event EventHandler Hidden;

        /// <summary>Fires when the dialog is revealed.</summary>
        event EventHandler Shown;

        /// <summary>Gets or sets the width of the dialog content.</summary>
        double Width { get; set; }

        /// <summary>Gets or sets the height of the dialog content.</summary>
        double Height { get; set; }

        /// <summary>Gets the content (XAML template + view-model).</summary>
        IViewTemplate Content { get; }

        /// <summary>Gets or sets the parent dialog that this content belongs to.</summary>
        IAcceptCancelDialog ParentDialog { get; set; }

        /// <summary>Causes the dialog box to be shown.</summary>
        void Show();

        /// <summary>Causes the dialog box to be shown.</summary>
        /// <param name="onHidden">Action that is invoked when the dialog is hidden.</param>
        void Show(Action<DialogResult> onHidden);

        /// <summary>Causes the dialog box to be shown.</summary>
        /// <param name="delay">
        ///    Shows the dialog after the specified delay (secs). 
        ///    This allows screen updates to occur prior to the dialog showing.
        ///    Typically set very short, at either 0.1 or 0.
        /// </param>
        /// <param name="onHidden">Action that is invoked when the dialog is hidden.</param>
        void Show(double delay, Action<DialogResult> onHidden);

        /// <summary>Causes the dialog box to be hidden.</summary>
        void Hide();
    }
}
