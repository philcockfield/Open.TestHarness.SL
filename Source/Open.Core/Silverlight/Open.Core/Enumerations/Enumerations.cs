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

namespace Open.Core.Common
{
    /// <summary>Size dimension a value pertains to.</summary>
    public enum SizeDimension
    {
        /// <summary>The width of the element.</summary>
        Width,

        /// <summary>The height of the element.</summary>
        Height
    }

    /// <summary>Flags representing a person's gender.</summary>
    [Flags]
    public enum GenderFlag
    {
        None = 0,
        Male = 1 << 0,
        Female = 1 << 1,
        Both = Male | Female
    }

    /// <summary>A person's gender.</summary>
    public enum Gender
    {
        Unknown = 0,
        Male = 1,
        Female = 2
    }

    /// <summary>The various kinds of click gestures.</summary>
    public enum ClickGesture
    {
        SingleClick,
        DoubleClick
    }

    /// <summary>A unit of file size.</summary>
    public enum FileSizeUnit
    {
        Byte,
        Kilobyte,
        Megabyte,
        Gigabyte,
        Terabyte,
    }

    /// <summary>The edges of a rectangle.</summary>
    public enum RectEdge
    {
        Left,
        Top,
        Right,
        Bottom
    }

    /// <summary>Flags representing the edges of a rectangle.</summary>
    [Flags]
    public enum RectEdgeFlag
    {
        None = 0,
        Left = 1 << 0,
        Top = 1 << 1,
        Right = 1 << 2,
        Bottom = 1 << 3,
    }
}
