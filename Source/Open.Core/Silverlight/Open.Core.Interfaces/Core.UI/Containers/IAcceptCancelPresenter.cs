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
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Open.Core.UI.Controls
{
    /// <summary>Flags representing whether the proposition in the dialog was accepted or cancelled.</summary>
    public enum DialogResult
    {
        None,
        Accepted,
        Cancelled,
    }

    public interface IAcceptCancelPresenter : INotifyPropertyChanged, IDisposable
    {
        /// <summary>Fires when the Accept button is clicked.</summary>
        event EventHandler AcceptClick;

        /// <summary>Fires when the Cancel button is clicked</summary>
        event EventHandler CancelClick;

        /// <summary>Gets or sets the label to display on the 'Accept' button.</summary>
        string AcceptLabel { get; set; }

        /// <summary>Gets or sets the label to display on the 'Cancel' button.</summary>
        string CancelLabel { get; set; }

        /// <summary>Gets or sets whether the user accepted or cancelled.</summary>
        DialogResult Result { get; set; }

        /// <summary>Gets or sets the margin to put around the presenter's content.</summary>
        Thickness ContentMargin { get; set; }

        /// <summary>Gets the 'Accept' click command.</summary>
        ICommand AcceptCommand { get; }

        /// <summary>Gets the 'Cancel' click command.</summary>
        ICommand CancelCommand { get; }

        /// <summary>Gets or sets whether the 'Accept' button is enabled.</summary>
        bool IsAcceptEnabled { get; set; }

        /// <summary>Gets or sets whether the 'Cancel' button is enabled.</summary>
        bool IsCancelEnabled { get; set; }

        /// <summary>Gets or sets whether the 'Accept' button is visible.</summary>
        bool IsAcceptVisible { get; set; }

        /// <summary>Gets or sets whether the 'Cancel' button is visible.</summary>
        bool IsCancelVisible { get; set; }

        /// <summary>Gets or sets the background of the presenter.</summary>
        Brush Background { get; set; }

        /// <summary>Resets the OK/Cancel labels to their default values and ensure both button are visible.</summary>
        void Reset();
    }
}
