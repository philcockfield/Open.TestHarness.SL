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
using System.Windows.Media.Animation;

namespace Open.Core.UI.Controls
{
    /// <summary>Defines the 'DialogPresenter' control.</summary>
    public interface IDialogPresenter
    {
        /// <summary>Gets or sets whether the dialog is currently being shown.</summary>
        bool IsShowing { get; set; }

        /// <summary>Gets or sets the duration (in seconds) of the animated slide when the dialog is shown or hidden.</summary>
        double AnimationDuration { get; set; }

        /// <summary>Gets or sets the brush used for the mask which covers the UI when the dialog is showing.</summary>
        Brush MaskBrush { get; set; }

        /// <summary>Gets or sets the opacity of the mask which covers the UI when the dialog is showing.</summary>
        double MaskOpacity { get; set; }

        /// <summary>Gets or sets the easing function to animate the slide with (Null if not required).</summary>
        IEasingFunction Easing { get; set; }
    }
}
