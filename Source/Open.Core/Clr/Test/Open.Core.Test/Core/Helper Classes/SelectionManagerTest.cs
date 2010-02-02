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
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Common
{
    [TestClass]
    public class SelectionManagerTest
    {
        #region Head
        private ObservableCollection<Stub> collection;
        private SelectionManager<Stub> manager;
        private int selectionChangedCount;

        [TestInitialize]
        public void TestSetup()
        {
            collection = new ObservableCollection<Stub>();
            manager = new SelectionManager<Stub>(collection);

            selectionChangedCount = 0;
            manager.SelectionChanged += delegate { selectionChangedCount++; };
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldImplementINotifyDisposed()
        {
            manager.GetType().IsA(typeof (INotifyDisposed));
        }

        [TestMethod]
        public void ShouldConstructWithCollection()
        {
            manager.Collection.ShouldBe(collection);
        }

        [TestMethod]
        public void ShouldBeSingleSelectionModelByDefault()
        {
            manager.SelectionMode.ShouldBe(SelectionMode.Single);
        }

        [TestMethod]
        public void ShouldThrowIfCollectionNotPassed()
        {
            Should.Throw<ArgumentNullException>(() => new SelectionManager<Stub>(null));
        }

        [TestMethod]
        public void ShouldHaveNothingSelected()
        {
            manager.SelectedItems.Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldHaveSelectedItem()
        {
            AddStubsToCollection(1);
            collection[0].IsSelected = true;
            manager.SelectedItems.Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldOnlyAllowOneItemToBeSelectedAtATime()
        {
            AddStubsToCollection(3);

            collection[0].IsSelected = true;
            collection[1].IsSelected = true;
            collection[2].IsSelected = true;

            manager.SelectedItems.Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldFireEventOnceWhenSelectionChanged()
        {
            AddStubsToCollection(3);
            var item1 = collection[0];
            var item2 = collection[1];
            var item3 = collection[2];

            item1.IsSelected = true;
            selectionChangedCount.ShouldBe(1);

            item1.IsSelected = false;
            selectionChangedCount.ShouldBe(2);
        }

        [TestMethod]
        public void ShouldStopReactingToEventsWhenItemRemovedFromCollection()
        {
            AddStubsToCollection(3);
            var item = collection[0];

            manager.Collection.Remove(item);
            item.IsSelected = true;
            selectionChangedCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldStopReactingToEventsWhenItemIsReplaced()
        {
            AddStubsToCollection(3);
            var item1 = manager.Collection[0];
            var item2 = new Stub();

            manager.Collection[0] = item2;
            item1.IsSelected = true;
            selectionChangedCount.ShouldBe(0);

            // --

            item2.IsSelected = true;
            selectionChangedCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldReactToEventsWhenItemAddedToCollection()
        {
            manager.Collection.Add(new Stub());
            var item = manager.Collection[0];

            item.IsSelected = true;
            selectionChangedCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldReactToEventsOfPreexistingItemsAddedToConstructor()
        {
            AddStubsToCollection(3);
            collection.Count.ShouldBe(3);

            manager = new SelectionManager<Stub>(collection);
            manager.SelectedItems.Count().ShouldBe(0);

            manager.Collection[0].IsSelected = true;
            selectionChangedCount.ShouldBe(1);

            manager.Collection[2].IsSelected = true;

            manager.Collection[0].IsSelected.ShouldBe(false);
            selectionChangedCount.ShouldBe(2);
        }

        [TestMethod]
        public void ShouldDispose()
        {
            AddStubsToCollection(3);
            manager.Dispose();

            collection[0].IsSelected = true;
            collection[1].IsSelected = true;
            collection[2].IsSelected = true;

            selectionChangedCount.ShouldBe(0);

            var item = new Stub();
            collection.Add(item);
            item.IsSelected = true;
            selectionChangedCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldFireDisposedEvent()
        {
            var count = 0;
            manager.Disposed += delegate { count++; };
            manager.Dispose();
            manager.Dispose();
            count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotFailWhenCollectionContainsNullEntries()
        {
            AddStubsToCollection(2);
            collection.Add(null);
            AddStubsToCollection(2);

            manager = new SelectionManager<Stub>(collection);
        }

        [TestMethod]
        public void ShouldHandleAllSelectionModes()
        {
            AddStubsToCollection(3);

            // NB: Will cause an error when 'SelectionMode' is added to and has not yet been supported in the Manager.
            foreach (SelectionMode mode in typeof(SelectionMode).GetEnumValues())
            {
                manager.SelectionMode = mode;
                var item = manager.Collection[1];
                item.IsSelected = !item.IsSelected;
            }
        }

        [TestMethod]
        public void ShouldThrowWhenClearIsCalledOnCollection()
        {
            AddStubsToCollection(3);
            Should.Throw<NotSupportedException>(() => collection.Clear());
        }
        #endregion

        #region Internal
        private void AddStubsToCollection(int total)
        {
            for (var i = 0; i < total; i++)
            {
                collection.Add(new Stub());
            }
        }
        #endregion

        #region Stubs
        public class Stub : ModelBase, ISelectable
        {
            public const string PropIsSelected = "IsSelected";
            public const string PropAnotherProperty = "AnotherProperty";
            private bool isSelected;
            public bool IsSelected
            {
                get { return isSelected; }
                set
                {
                    isSelected = value; 
                    OnPropertyChanged(PropIsSelected, PropAnotherProperty);
                }
            }
        }
        #endregion
    }
}
