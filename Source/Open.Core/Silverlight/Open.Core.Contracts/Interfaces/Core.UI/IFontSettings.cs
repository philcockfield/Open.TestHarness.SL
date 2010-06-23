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
using System.Windows.Media;

namespace Open.Core.UI
{
    /// <summary>An element that renders type with a font.</summary>
    public interface IFontSettings
    {
        /// <summary>Gets or sets the font used to display text in the control.</summary> 
        FontFamily FontFamily { get; set; }

        /// <summary>Gets or sets the size of the text in this control.</summary> 
        double FontSize { get; set; }

        /// <summary>Gets or sets the degree to which a font is condensed or expanded on the screen.</summary> 
        FontStretch FontStretch { get; set; }

        /// <summary>Gets or sets the style in which the text is rendered.</summary> 
        FontStyle FontStyle { get; set; }

        /// <summary>Gets or sets the thickness of the specified font.</summary> 
        FontWeight FontWeight { get; set; }

        /// <summary>Gets or sets the color of the font.</summary>
        Brush Color { get; set; }

        /// <summary>Gets or sets the opacity of the font (0..1).</summary>
        double Opacity { get; set; }

        /// <summary>Gets or sets the text-wrapping to apply to the font.</summary>
        TextWrapping TextWrapping { get; set; }

        /// <summary>Gets or sets the text-trimming to apply to the font.</summary>
        TextTrimming TextTrimming { get; set; }
    }
}
