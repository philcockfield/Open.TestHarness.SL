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
    /// <summary>An item within a process queue.</summary>
    public class ProcessQueueHandle
    {
        #region Events
        /// <summary>Fires when immediately before the item is processed ('Action' is invoked).</summary>
        public event ProcessQueueEventHandler Processing;
        private ProcessQueueEventArgs OnProcessing()
        {
            var args = new ProcessQueueEventArgs(this);
            if (Processing != null) Processing(this, args);
            queueOnProcessing(args);
            return args;
        }

        /// <summary>Fires when after the item has been processed ('Action' was invoked).</summary>
        public event ProcessQueueEventHandler Processed;
        private void OnProcessed()
        {
            var args = new ProcessQueueEventArgs(this);
            if (Processed != null) Processed(this, args);
            queueOnProcessed(args);
        }
        #endregion

        #region Head
        private readonly Action<ProcessQueueEventArgs> queueOnProcessing;
        private readonly Action<ProcessQueueEventArgs> queueOnProcessed;

        internal ProcessQueueHandle(ProcessQueue queue, Action action, Action<ProcessQueueEventArgs> queueOnProcessing, Action<ProcessQueueEventArgs> queueOnProcessed)
        {
            Queue = queue;
            Action = action;
            this.queueOnProcessing = queueOnProcessing;
            this.queueOnProcessed = queueOnProcessed;
        }
        #endregion

        #region Properties
        /// <summary>Gets the queue that the handle belongs to.</summary>
        public ProcessQueue Queue { get; private set; }

        /// <summary>Gets the position within the queue in descending order (0 is next to be processed, -1 means already processed).</summary>
        public int Position { get { return Queue.GetPosition(this); } }

        /// <summary>Gets the action that will be processed.</summary>
        public Action Action { get; private set; }

        /// <summary>Gets whether the handle is currently queued.</summary>
        /// <remarks>This will be false after the item has been processed.</remarks>
        public bool IsQueued { get { return Queue.Contains(this); } }

        /// <summary>Gets whether the handle has been processed.</summary>
        public bool IsProcessed { get; private set; }
        #endregion

        #region Methods
        /// <summary>Removes the item from the queue.</summary>
        public void Remove()
        {
            Queue.Remove(this);
        }

        /// <summary>Processes the item, invoking the action and removing it from the queue.</summary>
        public void Process()
        {
            // Setup initial conditions.
            if (IsProcessed) return;

            // Remove it from the queue.
            Remove();

            // Execute the action.
            if (OnProcessing().Cancel) return;
            Action();

            // Finish up.
            IsProcessed = true;
            OnProcessed();
        }
        #endregion
    }
}
