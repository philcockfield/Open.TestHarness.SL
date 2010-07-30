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

namespace Open.Core.Common.Collection
{
    /// <summary>Defines a event related to the processing of an item within a ProcessQueue.</summary>
    /// <param name="sender">The object firing the event.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void ProcessQueueEventHandler(object sender, ProcessQueueEventArgs e);

    /// <summary>Event arguments that accompany the pre-process events from a processing queue.</summary>
    public class ProcessQueueEventArgs : EventArgs
    {
        #region Head
        public ProcessQueueEventArgs(ProcessQueueHandle handle)
        {
            Handle = handle;
        }
        #endregion

        #region Properties
        /// <summary>Gets the handle to the item being processed.</summary>
        public ProcessQueueHandle Handle { get; private set; }

        /// <summary>Gets or sets whether the process should be cancelled.</summary>
        public bool Cancel { get; set; }
        #endregion
    }
}
