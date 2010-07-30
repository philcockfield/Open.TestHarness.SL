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
    /// <summary>An object that acts as a source for an output log.</summary>
    public class OutputWriter : IOutput
    {
        #region Events
        /// <summary>Fires when the output is written to</summary>
        public event EventHandler<OutputEventArgs> WrittenTo;
        private void OnWrittenTo(IOutputLine e) { if (WrittenTo != null) WrittenTo(this, new OutputEventArgs { Line = e }); }

        /// <summary>Fires when the log is cleared</summary>
        public event EventHandler Cleared;
        private void OnCleared() { if (Cleared != null) Cleared(this, new EventArgs()); }

        /// <summary>Fires when a line break is inserted.</summary>
        public event EventHandler BreakInserted;
        private void OnBreakInserted() { if (BreakInserted != null) BreakInserted(this, new EventArgs()); }
        #endregion

        #region Methods
        /// <summary>Writes the empty line to the log.</summary>
        public void Write()
        {
            Write((object)null);
        }

        /// <summary>Writes the given value to the log.</summary>
        /// <param name="value">The value to write.</param>
        public void Write(object value)
        {
            Write(new OutputLine { Value = value });
        }

        /// <summary>Writes the given value to the log.</summary>
        /// <param name="args">The definition of the line to write.</param>
        public void Write(IOutputLine args)
        {
            if (args == null) return;
            OnWrittenTo(args);
        }

        /// <summary>Inserts a line break into the log.</summary>
        public void Break()
        {
            OnBreakInserted();
        }

        /// <summary>Clears the log.</summary>
        public void Clear()
        {
            OnCleared();
        }
        #endregion
    }
}
