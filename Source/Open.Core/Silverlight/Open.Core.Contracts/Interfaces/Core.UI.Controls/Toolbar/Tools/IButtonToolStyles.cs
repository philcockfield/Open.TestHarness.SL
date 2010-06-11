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

using System.Windows;

namespace Open.Core.UI.Controls
{
    /// <summary>A set of styles for an IButtonTool.</summary>
    public interface IButtonToolStyles
    {
        /// <summary>Gets or sets the default background of the tool.</summary>
        DataTemplate BackgroundDefault { get; set; }

        /// <summary>Gets or sets the background of the tool when the mouse is over it.</summary>
        DataTemplate BackgroundOver { get; set; }

        /// <summary>Gets or sets the background of the tool when the mouse is pressed.</summary>
        DataTemplate BackgroundDown { get; set; }

        /// <summary>Gets or sets the background of the tool when the button is pressed (and the mouse is not over or down).</summary>
        /// <remarks>Only used when the button is in toggle mode (IsToggleButton == true)</remarks>
        DataTemplate BackgroundTogglePressed { get; set; }
    }
}
