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
using System.Linq;
using System.Collections.Generic;

namespace Open.Core.Common.Collection
{
    /// <summary>A queue of items waiting to be processed.</summary>
    public class ProcessQueue
    {
        #region Events
        /// <summary>Fires when immediately before the item is processed ('Action' is invoked).</summary>
        public event ProcessQueueEventHandler Processing;
        private void OnProcessing(ProcessQueueEventArgs args)
        {
            if (Processing != null) Processing(this, args);
        }

        /// <summary>Fires when after the item has been processed ('Action' was invoked).</summary>
        public event ProcessQueueEventHandler Processed;
        private void OnProcessed(ProcessQueueEventArgs args) { if (Processed != null) Processed(this, args); }
        #endregion

        #region Head
        private readonly Queue<ProcessQueueHandle> queue = new Queue<ProcessQueueHandle>();
        #endregion

        #region Properties
        /// <summary>Gets the number of items within the queue.</summary>
        public int Count { get { return queue.Count; } }

        /// <summary>Gets or sets the next item to be processed in the queue.</summary>
        public ProcessQueueHandle NextItem { get { return Count == 0 ? null : queue.Peek(); } }
        #endregion

        #region Methods
        /// <summary>Adds a new item to process to the end of the queue.</summary>
        /// <param name="action">The action to process.</param>
        /// <returns>A handle to the item within the queue.</returns>
        public ProcessQueueHandle Add(Action action)
        {
            // Setup initial conditions.
            if (action == null) throw new ArgumentNullException("action");

            // Create the handle and store ite.
            var handle = new ProcessQueueHandle(this, action, OnProcessing, OnProcessed);
            lock (queue)
            {
                queue.Enqueue(handle);
            }

            // Finish up.
            return handle;
        }

        /// <summary>Gets the position of a handle within the queue.</summary>
        /// <param name="handle">The handle to examine.</param>
        /// <returns>The position within the queue, descending (0 is next to be processed, -1 means already processed or not part of this queue).</returns>
        public int GetPosition(ProcessQueueHandle handle)
        {
            if (handle == null) return -1;
            return queue.ToList().IndexOf(handle);
        }

        /// <summary>Determines whether the specified handle is contained within the queue.</summary>
        /// <param name="handle">The handle to look for.</param>
        public bool Contains(ProcessQueueHandle handle)
        {
            return queue.Contains(handle);
        }

        /// <summary>Removes the given handle from the queue.</summary>
        /// <param name="handle">The handle to remove.</param>
        public void Remove(ProcessQueueHandle handle)
        {
            if (handle == null) return;
            lock (queue)
            {
                var list = queue.ToList();
                list.Remove(handle);
                queue.Clear();
                foreach (var item in list)
                {
                    queue.Enqueue(item);
                }
            }
        }

        /// <summary>Processes the next item within the queue.</summary>
        /// <returns>True if the next item was processed, or False if there are no more items in the queue.</returns>
        public bool ProcessNext()
        {
            var nextItem = NextItem;
            if (nextItem == null) return false;
            nextItem.Process();
            return true;
        }
        #endregion
    }
}
