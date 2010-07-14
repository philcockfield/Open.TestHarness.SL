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
using System.Windows.Media.Animation;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>Flags indicating the various dialog size options.</summary>
    public enum DialogSize
    {
        /// <summary>The dialog size is fixed to the size of the content.</summary>
        Fixed,

        /// <summary>The dialog fills the available space.</summary>
        Fill,

        /// <summary>The dialog stretches horizontally.</summary>
        StretchHorizontal,

        /// <summary>The dialog stretches vertically.</summary>
        StretchVertical
    }

    /// <summary>A dialog box which drops down from the top of the screen.</summary>
    public interface IDropdownDialog : IViewFactory, INotifyPropertyChanged
    {
        /// <summary>Fires when the dialog starts to show (slide on).</summary>
        event EventHandler Showing;

        /// <summary>Fires when the dialog has completed showing.</summary>
        event EventHandler Shown;

        /// <summary>Fires when the dialog starts to hide (slide off).</summary>
        event EventHandler Hiding;

        /// <summary>Fires when the dialog is completes the hide sequence.</summary>
        event EventHandler Hidden;

        /// <summary>Gets or sets whether the dialog is currently showing.</summary>
        bool IsShowing { get; set; }

        /// <summary>Gets or sets the size mode.</summary>
        DialogSize SizeMode { get; set; }

        /// <summary>Gets or sets the duration (in seconds) of the dialog slide animation.</summary>
        double SlideDuration { get; set; }

        /// <summary>Gets or sets the animation easing function to apply to the slide animation.</summary>
        IEasingFunction Easing { get; set; }

        /// <summary>Gets or sets the opacity of the drop-shadow.</summary>
        double DropShadowOpacity { get; set; }

        /// <summary>Gets the UI mask that is visible when the dialog IsShowing.</summary>
        IBackground Mask { get;  }

        /// <summary>Gets the background of the dialog.</summary>
        IBackground Background { get; }

        /// <summary>Gets the prompt buttons.</summary>
        IPromptButtonBar ButtonBar { get; }

        /// <summary>Gets or sets the padding within the dialog (around the Content).</summary>
        Thickness Padding { get; set; }

        /// <summary>Gets or sets the margin around the dialog.</summary>
        Thickness Margin { get; set; }

        /// <summary>Gets or sets the content of the dialog.</summary>
        IViewFactory Content { get; set; }
    }
}
