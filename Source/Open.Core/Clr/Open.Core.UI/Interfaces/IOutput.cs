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
using System.Windows.Media;
using System.Windows;

namespace Open.Core.Common
{
    /// <summary>Represents a single line in an output log.</summary>
    public interface IOutputLine
    {
        /// <summary>Get or sets the value to write.</summary>
        object Value { get; set; }

        /// <summary>Gets or sets the color to associate with the log entry.</summary>
        Color Color { get; set; }

        /// <summary>Gets or sets the weight of the font.</summary>
        FontWeight FontWeight { get; set; }
    }

    /// <summary>Event arguments fired by the output log.</summary>
    public class OutputEventArgs : EventArgs
    {
        /// <summary>Gets or sets the data representing the line in the output log.</summary>
        public IOutputLine Line { get; set; }
    }

    /// <summary>An output log.</summary>
    public interface IOutput
    {
        /// <summary>Fires when the output log is written to.</summary>
        event EventHandler<OutputEventArgs> WrittenTo;

        /// <summary>Fires when the log is cleared.</summary>
        event EventHandler Cleared;

        /// <summary>Fires when a line break is inserted.</summary>
        event EventHandler BreakInserted;

        /// <summary>Writes the empty line to the log.</summary>
        void Write();

        /// <summary>Writes the given value to the log.</summary>
        /// <param name="value">The value to write.</param>
        void Write(object value);

        /// <summary>Writes the given value to the log.</summary>
        /// <param name="args">The definition of the line to write.</param>
        void Write(IOutputLine args);

        /// <summary>Inserts a line break into the log.</summary>
        void Break();

        /// <summary>Clears the log.</summary>
        void Clear();
    }
}
