﻿//------------------------------------------------------
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
using System.Windows.Controls;
using System.Windows.Media;

namespace Open.Core.UI.Controls
{
    /// <summary>A ToolBar tool which behaves like a button.</summary>
    public interface IButtonTool : ITool
    {
        /// <summary>Fires when tool is clicked.</summary>
        event EventHandler Click;

        /// <summary>Gets or sets the orentation of the label relative to the icon.</summary>
        Orientation Orientation { get; set; }

        /// <summary>Gets or sets the XAML elements that make up the tool.</summary>
        IButtonToolStyles Styles { get; set; }

        /// <summary>Gets or sets the style of behavior the button expresses.</summary>
        ButtonToolType ButtonType { get; set; }

        /// <summary>Gets or sets the icon image.</summary>
        Image Icon { get; set; }

        /// <summary>Gets or sets the text label of the button.</summary>
        string Text { get; set; }

        /// <summary>Gets or sets the tooltip of the button.</summary>
        string ToolTip { get; set; }

        /// <summary>Gets or sets the mouse related state of the button.</summary>
        ButtonMouseState MouseState { get; set; }

        /// <summary>Gets whether the mouse is currently over the button.</summary>
        bool IsMouseOver { get; }

        /// <summary>Gets whether the mouse is currently pressing the button.</summary>
        bool IsMouseDown { get; }

        /// <summary>Gets or sets whether the default background is rendered when the mouse is not over the tool (see 'Styles').</summary>
        bool IsDefaultBackgroundVisible { get; set; }
    }

    /// <summary>The various types of button behavior a ButtonTool can express.</summary>
    public enum ButtonToolType
    {
        Default,
        DropDown,
        Split,
    }

}
