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
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Collection;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Functional_Adorners
{
    [TestClass]
    public class CollectionChangedEventBatchTest : SilverlightUnitTest
    {
        #region Head
        private ObservableCollection<Model> collection;
        private CollectionChangedEventBatch<Model> eventBatcher;

        private Model item1;
        private Model item2;
        private Model item3;
        private Model item4;
        private Model item5;

        [TestInitialize]
        public void TestSetup()
        {
            item1 = new Model { Name = "1" };
            item2 = new Model { Name = "2" };
            item3 = new Model { Name = "3" };
            item4 = new Model { Name = "4" };
            item5 = new Model { Name = "5" };
            collection = new ObservableCollection<Model>();
            collection.AddRange(item1, item2, item3, item4, item5);

            eventBatcher = new CollectionChangedEventBatch<Model>(collection);
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldReportAddAndRemoveBatchChanges()
        {
            // Setup initial conditions.
            eventBatcher.CollectionChanged.ShouldBe(false);
            var add1 = new Model();
            var add2 = new Model();

            // Event montior.
            CollectionChangedEventBatchArgs<Model> argsBatchChange = null;
            eventBatcher.BatchChange += (sender, e) => argsBatchChange = e;

            // Check initial conditions.
            eventBatcher.AddedItems.Count().ShouldBe(0);
            eventBatcher.RemovedItems.Count().ShouldBe(0);

            // Perform changes.
            collection.AddRange(add1, add2);
            collection.Remove(item3);
            collection.Remove(item4);

            // Ensure change properties are updated.
            eventBatcher.CollectionChanged.ShouldBe(true);
            eventBatcher.AddedItems.Count().ShouldBe(2);
            eventBatcher.RemovedItems.Count().ShouldBe(2);

            eventBatcher.ProcessBatch();
            eventBatcher.CollectionChanged.ShouldBe(false);

            // Check for changes.
            argsBatchChange.ShouldNotBe(null);
            argsBatchChange.AddedItems.ShouldContain(add1, add2);
            argsBatchChange.RemovedItems.ShouldContain(item3, item4);

            // Ensure change properties are reset.
            eventBatcher.AddedItems.Count().ShouldBe(0);
            eventBatcher.RemovedItems.Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldReportSimpleReplaceAsAddRemove()
        {
            // Setup initial conditions.
            var replace1 = new Model();

            // Event montior.
            CollectionChangedEventBatchArgs<Model> argsBatchChange = null;
            eventBatcher.BatchChange += (sender, e) => argsBatchChange = e;

            // Perform changes.
            collection[0] = replace1;
            eventBatcher.ProcessBatch();

            // Check for changes.
            argsBatchChange.ShouldNotBe(null);
            argsBatchChange.AddedItems.ShouldContain(replace1);
            argsBatchChange.RemovedItems.ShouldContain(item1);
        }

        [TestMethod]
        public void ShouldReportSwapOperationFromReplace()
        {
            // Event montior.
            CollectionChangedEventBatchArgs<Model> argsBatchChange = null;
            eventBatcher.BatchChange += (sender, e) => argsBatchChange = e;

            // Perform swap operation.
            collection[0] = item2;
            collection[1] = item1;
            eventBatcher.SwappedItems.Count().ShouldBe(1);
            
            eventBatcher.ProcessBatch();
            eventBatcher.SwappedItems.Count().ShouldBe(0); // Reset on batcher, passed on event args.

            // Check for changes.
            argsBatchChange.ShouldNotBe(null);
            argsBatchChange.AddedItems.Count().ShouldBe(0);
            argsBatchChange.RemovedItems.Count().ShouldBe(0);
            argsBatchChange.SwappedItems.Count().ShouldBe(1);

            // Examine the swapped item args.
            var swapped = argsBatchChange.SwappedItems.First();
            swapped.Item1.ShouldBe(item1);
            swapped.Item2.ShouldBe(item2);
            swapped.Item1Index.ShouldBe(1);
            swapped.Item2Index.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldHandleMultipleSwapOperations()
        {
            // Event montior.
            CollectionChangedEventBatchArgs<Model> argsBatchChange = null;
            eventBatcher.BatchChange += (sender, e) => argsBatchChange = e;

            // First swap operation.
            collection[0] = item2;
            collection[1] = item1;

            // Second swap operation.
            collection[1] = item3;
            collection[2] = item1;

            eventBatcher.SwappedItems.Count().ShouldBe(2);

            // Check collection order.
            collection[0].ShouldBe(item2);
            collection[1].ShouldBe(item3);
            collection[2].ShouldBe(item1);

            // Process the batch of changes.
            eventBatcher.ProcessBatch();
            eventBatcher.SwappedItems.Count().ShouldBe(0);
            argsBatchChange.SwappedItems.Count().ShouldBe(2);

            // Get the 'Swapped' objects to examine.
            var swapped1 = argsBatchChange.SwappedItems.ElementAt(0);
            var swapped2 = argsBatchChange.SwappedItems.ElementAt(1);

            // Examine the swapped item args.
            swapped1.Item1.ShouldBe(item1);
            swapped1.Item2.ShouldBe(item2);
            swapped1.Item1Index.ShouldBe(1);
            swapped1.Item2Index.ShouldBe(0);

            swapped2.Item1.ShouldBe(item1);
            swapped2.Item2.ShouldBe(item3);
            swapped2.Item1Index.ShouldBe(2);
            swapped2.Item2Index.ShouldBe(1);

        }

        [TestMethod]
        public void ShouldNotReportChangeWhenCollectionNotChanged()
        {
            EventArgs argsBatchChange = null;
            eventBatcher.BatchChange += (sender, e) => argsBatchChange = e;

            eventBatcher.CollectionChanged.ShouldBe(false);

            eventBatcher.ProcessBatch();
            argsBatchChange.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldDisconnectFromCollectionOnDestroy()
        {
            collection.Add(new Model());
            eventBatcher.CollectionChanged.ShouldBe(true);
            
            eventBatcher.ProcessBatch();
            eventBatcher.CollectionChanged.ShouldBe(false);

            eventBatcher.Destroy();
            collection.Add(new Model());
            eventBatcher.CollectionChanged.ShouldBe(false);
        }

        [TestMethod]
        public void CollectionShouldAllowMultipleEntriesOfSameInstance()
        {
            collection[1] = collection[0];
            collection[0] = item1;
            collection[1] = item1;

            collection.RemoveAll();
            collection.Add(item1);
            collection.Add(item1);

            collection.Count.ShouldBe(2);
            collection[0] = item1;
            collection[1] = item1;
        }
        #endregion

        #region Sample Data
        private class Model
        {
            public string Name { get; set; }
        }
        #endregion
    }
}
