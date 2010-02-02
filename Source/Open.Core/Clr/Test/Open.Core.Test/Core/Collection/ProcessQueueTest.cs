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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Collection;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Collection
{
    [TestClass]
    public class ProcessQueueTest
    {
        [TestMethod]
        public void ShouldReturnHandleWhenItemAddedToQueue()
        {
            var queue = new ProcessQueue();
            queue.Count.ShouldBe(0);

            Action action = () => { };
            var handle = queue.Add(action);
            
            handle.ShouldNotBe(null);
            handle.Action.ShouldBe(action);
            queue.Count.ShouldBe(1);

            Should.Throw<ArgumentNullException>(() => queue.Add(null));
        }


        [TestMethod]
        public void ShouldHandleShouldExposeQueueAndPositionInQueue()
        {
            var queue = new ProcessQueue();
            Action action = () => { };

            var handle1 = queue.Add(action);
            var handle2 = queue.Add(action);
            var handle3 = queue.Add(action);

            handle1.Queue.ShouldBe(queue);

            // --

            var notInQueueHandle = new ProcessQueue().Add(action);

            queue.GetPosition(null).ShouldBe(-1);
            queue.GetPosition(notInQueueHandle).ShouldBe(-1);
            queue.GetPosition(handle1).ShouldBe(0);
            queue.GetPosition(handle2).ShouldBe(1);
            queue.GetPosition(handle3).ShouldBe(2);

            // --

            handle1.Position.ShouldBe(0);
            handle2.Position.ShouldBe(1);
            handle3.Position.ShouldBe(2);
        }

        [TestMethod]
        public void ShouldDetermineIfHandleIsContainedWithinQueue()
        {
            var queue = new ProcessQueue();
            Action action = () => { };
            queue.Contains(null).ShouldBe(false);

            var handle1 = queue.Add(action);
            queue.Contains(handle1).ShouldBe(true);

            queue.Remove(handle1);
            queue.Contains(handle1).ShouldBe(false);
        }

        [TestMethod]
        public void ShouldRemoveHandleFromQueue()
        {
            var queue = new ProcessQueue();
            Action action = () => { };

            var handle1 = queue.Add(action);
            var handle2 = queue.Add(action);
            var handle3 = queue.Add(action);

            queue.Count.ShouldBe(3);
            queue.Remove(null);
            queue.Count.ShouldBe(3);

            queue.Remove(handle1);
            queue.Count.ShouldBe(2);
            queue.Contains(handle1).ShouldBe(false);

            handle2.Position.ShouldBe(0);
            handle3.Position.ShouldBe(1);

            queue.Remove(handle2);
            queue.Remove(handle3);
            queue.Count.ShouldBe(0);

            // --
            queue.Remove(handle1);
            queue.Remove(handle2);
            queue.Remove(handle3);
        }

        [TestMethod]
        public void ShouldRemoveHandleFromQueueViaHandle()
        {
            var queue = new ProcessQueue();
            Action action = () => { };

            var handle1 = queue.Add(action);
            var handle2 = queue.Add(action);
            var handle3 = queue.Add(action);

            queue.Count.ShouldBe(3);
            queue.Contains(handle1).ShouldBe(true);

            handle1.Remove();
            queue.Count.ShouldBe(2);
            queue.Contains(handle1).ShouldBe(false);

            // --
            handle2.Position.ShouldBe(0);
            handle3.Position.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldReportIsQueued()
        {
            var queue = new ProcessQueue();
            Action action = () => { };

            var handle1 = queue.Add(action);
            var handle2 = queue.Add(action);
            var handle3 = queue.Add(action);

            handle1.IsQueued.ShouldBe(true);
            handle2.IsQueued.ShouldBe(true);

            queue.Remove(handle1);

            handle1.IsQueued.ShouldBe(false);
            handle2.IsQueued.ShouldBe(true);
            handle3.IsQueued.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldProcessAndBeRemovedFromQueue()
        {
            var queue = new ProcessQueue();
            Action action = () => { };

            var handle1 = queue.Add(action);
            var handle2 = queue.Add(action);

            handle1.IsProcessed.ShouldBe(false);
            handle2.IsProcessed.ShouldBe(false);

            handle1.Process();
            handle1.IsProcessed.ShouldBe(true);
            queue.Count.ShouldBe(1);
            queue.Contains(handle1).ShouldBe(false);
        }

        [TestMethod]
        public void ShouldNotProcessMoreThanOnce()
        {
            var queue = new ProcessQueue();

            var processCount = 0;
            Action action = () => { processCount++; };

            var handle1 = queue.Add(action);

            handle1.Process();
            processCount.ShouldBe(1);

            handle1.Process();
            handle1.Process();
            handle1.Process();
            processCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldBeAbleToProcessEvenIfRemovedFromQueue()
        {
            var queue = new ProcessQueue();

            var processCount = 0;
            Action action = () => { processCount++; };

            var handle1 = queue.Add(action);
            handle1.Remove();
            queue.Contains(handle1).ShouldBe(false);
            queue.Count.ShouldBe(0);

            handle1.IsProcessed.ShouldBe(false);
            handle1.IsQueued.ShouldBe(false);
            processCount.ShouldBe(0);

            handle1.Process();
            processCount.ShouldBe(1);
            handle1.IsProcessed.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldBeAbleToProcessInAnyOrder()
        {
            var queue = new ProcessQueue();

            var processCount = 0;
            Action action = () => { processCount++; };

            var handle1 = queue.Add(action);
            var handle2 = queue.Add(action);

            handle2.Process();
            handle1.Process();

            processCount.ShouldBe(2);
            queue.Count.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldFirePreAndPostProcessEventsFromHandle()
        {
            var queue = new ProcessQueue();
            Action action = () => { };
            var handle1 = queue.Add(action);

            var processingCount = 0;
            handle1.Processing += (s, e) =>
                                      {
                                          processingCount++;
                                          e.Handle.ShouldBe(handle1);
                                          e.Cancel.ShouldBe(false);
                                      };

            var processedCount = 0;
            handle1.Processed += (s, e) =>
                                      {
                                          processedCount++;
                                          e.Handle.ShouldBe(handle1);
                                          e.Cancel.ShouldBe(false);
                                          handle1.IsProcessed.ShouldBe(true);
                                      };

            handle1.Process();
            processingCount.ShouldBe(1);
            processedCount.ShouldBe(1);
            handle1.IsProcessed.ShouldBe(true);
        }


        [TestMethod]
        public void ShouldFirePreAndPostProcessEventsFromQueue()
        {
            var queue = new ProcessQueue();
            Action action = () => { };
            var handle1 = queue.Add(action);

            var processingCount = 0;
            queue.Processing += (s, e) =>
                                            {
                                                processingCount++;
                                                e.Handle.ShouldBe(handle1);
                                                e.Cancel.ShouldBe(false);
                                            };

            var processedCount = 0;
            queue.Processed += (s, e) =>
                                            {
                                                processedCount++;
                                                e.Handle.ShouldBe(handle1);
                                                e.Cancel.ShouldBe(false);
                                                handle1.IsProcessed.ShouldBe(true);
                                            };

            handle1.Process();
            processingCount.ShouldBe(1);
            processedCount.ShouldBe(1);
            handle1.IsProcessed.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldCancelProcessViaHandleEvent()
        {
            var queue = new ProcessQueue();
            var processCount = 0;
            Action action = () => { processCount++; };

            var handle1 = queue.Add(action);
            queue.Contains(handle1).ShouldBe(true);

            // Cancel the process when the pre-event fires.
            handle1.Processing += (s, e) => { e.Cancel = true; };

            // Attempt to process.
            handle1.Process();
            processCount.ShouldBe(0);
            handle1.IsProcessed.ShouldBe(false);
            handle1.IsQueued.ShouldBe(false);

            // Cancelled item should be removed from queue.
            queue.Contains(handle1).ShouldBe(false);
        }

        [TestMethod]
        public void ShouldCancelProcessViaQueueEvent()
        {
            var queue = new ProcessQueue();
            var processCount = 0;
            Action action = () => { processCount++; };

            var handle1 = queue.Add(action);
            queue.Contains(handle1).ShouldBe(true);

            // Cancel the process when the pre-event fires.
            queue.Processing += (s, e) => { e.Cancel = true; };

            // Attempt to process.
            handle1.Process();
            processCount.ShouldBe(0);
            handle1.IsProcessed.ShouldBe(false);
            handle1.IsQueued.ShouldBe(false);

            // Cancelled item should be removed from queue.
            queue.Contains(handle1).ShouldBe(false);
        }

        [TestMethod]
        public void ShouldExposeTheNextItem()
        {
            var queue = new ProcessQueue();
            queue.NextItem.ShouldBe(null);

            Action action = () => { };
            var handle1 = queue.Add(action);
            var handle2 = queue.Add(action);
            var handle3 = queue.Add(action);

            queue.NextItem.ShouldBe(handle1);

            handle1.Process();
            queue.NextItem.ShouldBe(handle2);

            queue.Remove(handle2);
            queue.NextItem.ShouldBe(handle3);

            handle3.Process();
            queue.Count.ShouldBe(0);
            queue.NextItem.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldProcessNext()
        {
            var queue = new ProcessQueue();

            var processCount = 0;
            Action action = () => { processCount++; };

            queue.Add(action);
            queue.Add(action);
            queue.Add(action);

            queue.ProcessNext().ShouldBe(true);
            queue.ProcessNext().ShouldBe(true);
            queue.ProcessNext().ShouldBe(true);
            queue.ProcessNext().ShouldBe(false); // Empty

            processCount.ShouldBe(3);
        }
    }
}
