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
using System.ComponentModel;

namespace Open.Core.UI.Controls
{
    /// <summary>Defines the 'AcceptCancelDialog', a 'AcceptCancelPresenter' within a 'DialogPresenter' driven by this view-model.</summary>
    public interface IAcceptCancelDialog : INotifyDisposed, IDialogPresenter, IAcceptCancelPresenter
    {
        /// <summary>Gets or sets the pixel width of the dialog.</summary>
        double DialogWidth { get; set; }

        /// <summary>Gets or sets the pixel height of the dialog.</summary>
        double DialogHeight { get; set; }

        /// <summary>Gets the content (Template + View-Model).</summary>
        IViewTemplate Content { get; }

        /// <summary>Gets or sets whether the dialog is automatically hidden when the Cancel button is clicked.</summary>
        bool AutoHideOnCancel { get; set; }

        /// <summary>Gets or sets whether the dialog is automatically hidden when the Accept button is clicked.</summary>
        bool AutoHideOnAccept { get; set; }
    }
}
