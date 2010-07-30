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

using System.ComponentModel;
using System.Windows;
using Open.Core.Common;

namespace Open.Core.UI
{
    /// <summary>Abstractly defines a simple button.</summary>
    public interface IButton : IViewFactory, IClickable, IVisibility, IEnableable, INotifyPropertyChanged
    {
        /// <summary>Gets or sets the text label of the button.</summary>
        string Text { get; set; }

        /// <summary>Gets or sets the margin to put around the button.</summary>
        Thickness Margin { get; set; }

        /// <summary>Gets or sets the tooltip of the button.</summary>
        string ToolTip { get; set; }

        /// <summary>Gets or sets an arbitrary state object associated with the button.</summary>
        object Tag { get; set; }
    }
}
