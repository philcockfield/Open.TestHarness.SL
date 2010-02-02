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
using System.Windows;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Event args that are fired on the mouse-move event when an item is being dragged.</summary>
    /// <remarks>See the 'Draggable' behavior.</remarks>
    public class DraggingEventArgs : EventArgs
    {
        #region Properties
        /// <summary>Gets or sets the X:Y pixel position of the elemnt within it's container.</summary>
        public Point Position { get; set; }

        /// <summary>Gets or sets whether the drag operation has been cancelled by a listener.</summary>
        public bool Cancelled { get; set; }
        #endregion

        #region Methods
        /// <summary>Causes the drag operation to be stopped.</summary>
        /// <remarks>Same as setting the 'Cancelled' property to True.</remarks>
        public void Cancel()
        {
            Cancelled = true;
        }
        #endregion
    }
}
