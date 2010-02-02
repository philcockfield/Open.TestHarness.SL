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
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Collection;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Collection
{
    [TestClass]
    public class ObservableCollectionWrapperTest
    {
        #region Tests
        [TestMethod]
        public void ShouldExposeSourceCollection()
        {
            var collectionA = new ObservableCollection<Model>();
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));
            wrapper.Source.ShouldBe(collectionA);
        }

        [TestMethod]
        public void ShouldAddWrapperItemsInCorrectPosition()
        {
            var collectionA = new ObservableCollection<Model>();
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));

            var item1 = new Model();
            var item2 = new Model();
            var item3 = new Model();

            collectionA.Add(item1);
            wrapper.Count.ShouldBe(1);
            wrapper[0].Source.ShouldBe(item1);
            wrapper.ContainsWrapper(item1).ShouldBe(true);

            collectionA.Add(item2);
            wrapper.Count.ShouldBe(2);
            wrapper[1].Source.ShouldBe(item2);
            wrapper.ContainsWrapper(item2).ShouldBe(true);

            collectionA.Insert(1, item3);
            wrapper.Count.ShouldBe(3);
            wrapper[1].Source.ShouldBe(item3);
            wrapper.ContainsWrapper(item3).ShouldBe(true);
        }


        [TestMethod]
        public void ShouldAddWrapperItemsInCorrectPositionFromConstructor()
        {
            var list = new List<string> { "item1", "Clown", "item1", "Zebra", "apple", "item1", "apple" };

            var collectionA = new ObservableCollection<string>();
            collectionA.AddRange(list);

            collectionA[0].ShouldBe("item1");
            collectionA[1].ShouldBe("Clown");
            collectionA[2].ShouldBe("item1");
            collectionA[3].ShouldBe("Zebra");
            collectionA[4].ShouldBe("apple");
            collectionA[5].ShouldBe("item1");
            collectionA[6].ShouldBe("apple");

            var wrapper = new ObservableCollectionWrapper<string, StringWrapper>(collectionA, item => new StringWrapper(item));
            wrapper.Count.ShouldBe(7);

            wrapper[0].Source.ShouldBe(list[0]);
            wrapper[1].Source.ShouldBe(list[1]);
            wrapper[2].Source.ShouldBe(list[2]);
            wrapper[3].Source.ShouldBe(list[3]);
            wrapper[4].Source.ShouldBe(list[4]);
            wrapper[5].Source.ShouldBe(list[5]);
            wrapper[6].Source.ShouldBe(list[6]);
        }

        [TestMethod]
        public void ShouldRemoveStringWrapperItems()
        {
            var list = new List<string> { "item1", "Clown", "item1", "Zebra"};
            var collectionA = new ObservableCollection<string>();
            collectionA.AddRange(list);

            var wrappers = new ObservableCollectionWrapper<string, StringWrapper>(collectionA, item => new StringWrapper(item));
            wrappers.Count.ShouldBe(4);

            var wrapper1 = wrappers[0];
            var wrapper2 = wrappers[1];
            var wrapper3 = wrappers[2];
            var wrapper4 = wrappers[3];

            collectionA.RemoveAt(2);

            wrappers.Count.ShouldBe(3);
            wrappers.Contains(wrapper1).ShouldBe(true);
            wrappers.Contains(wrapper3).ShouldBe(false); // Should remove the 3rd item, not the 1st item (of the same string value).
        }

        [TestMethod]
        public void ShouldRemoveWrapperItems()
        {
            var item1 = new Model { Name = "One" };
            var item2 = new Model { Name = "Two" };
            var item3 = new Model { Name = "Three" };
            var collectionA = new ObservableCollection<Model> { item1, item2, item3 };
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));

            collectionA.Remove(item2);
            wrapper.Count.ShouldBe(2);
        }

        [TestMethod]
        public void ShouldReplaceWrapperItems()
        {
            var item1 = new Model { Name = "One" };
            var item2 = new Model { Name = "Two" };
            var item3 = new Model { Name = "Three" };
            var item4 = new Model { Name = "Four" };
            
            var collectionA = new ObservableCollection<Model> { item1, item2, item3 };
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));
            wrapper.Count.ShouldBe(3);

            collectionA[1] = item4;
            wrapper[1].Source.ShouldBe(item4);

            collectionA[0] = item4;
            wrapper[0].Source.ShouldBe(item4);

            collectionA[2] = item4;
            wrapper[2].Source.ShouldBe(item4);
        }

        [TestMethod]
        public void ShouldRemoveAll()
        {
            var collectionA = new ObservableCollection<Model>();
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));

            collectionA.Add(new Model());
            collectionA.Add(new Model());
            collectionA.Add(new Model());
            wrapper.Count.ShouldBe(3);

            collectionA.RemoveAll();
            wrapper.Count.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldAddInitialItems()
        {
            var collectionA = new ObservableCollection<Model>{new Model(), new Model(), new Model()};
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));
            wrapper.Count.ShouldBe(3);

            wrapper[0].Source.ShouldBe(collectionA[0]);
            wrapper[1].Source.ShouldBe(collectionA[1]);
            wrapper[2].Source.ShouldBe(collectionA[2]);
        }

        [TestMethod]
        public void ShouldGetSource()
        {
            var item1 = new Model{Name = "One"};
            var item2 = new Model{Name = "Two"};
            var collectionA = new ObservableCollection<Model> { item1, item2 };
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));

            wrapper.GetSource(wrapper[0]).ShouldBe(item1);
            wrapper.GetSource(new ModelWrapper(item2)).ShouldBe(null);
            wrapper.GetSource(null).ShouldBe(null);
        }

        [TestMethod]
        public void ShouldGetWrapper()
        {
            var item1 = new Model { Name = "One" };
            var item2 = new Model { Name = "Two" };
            var collectionA = new ObservableCollection<Model> { item1, item2};
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));
            
            wrapper.GetWrapper(item2).ShouldBe(wrapper[1]);
            wrapper.GetWrapper(new Model()).ShouldBe(null);
            wrapper.GetWrapper(null).ShouldBe(null);
        }

        [TestMethod]
        public void ShouldBeAbleToAddSameItemMultipleTimes()
        {
            var item1 = new Model();
            var collectionA = new ObservableCollection<Model> { item1, item1, item1 };
            var wrappersA = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));

            wrappersA.GetSource(wrappersA[0]).ShouldBe(item1);
            wrappersA.GetSource(wrappersA[1]).ShouldBe(item1);
            wrappersA.GetSource(wrappersA[2]).ShouldBe(item1);

            wrappersA.GetWrapper(item1).ShouldBe(wrappersA[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ShouldThrowWhenCleared()
        {
            var collectionA = new ObservableCollection<Model>();
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));
            collectionA.Clear();
        }


        [TestMethod]
        public void ShouldBeDisposable()
        {
            var item1 = new Model();
            var collectionA = new ObservableCollection<Model> { item1};
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));

            var wrappedModel = wrapper.GetWrapper(item1);

            wrapper.Dispose();
            wrappedModel.IsDisposed.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldDisposeOfItemOnRemoval()
        {
            var item1 = new Model { Name = "One" };
            var item2 = new Model { Name = "Two" };
            var collectionA = new ObservableCollection<Model> { item1, item2};
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));

            wrapper.DisposeOfWrapperOnRemoval.ShouldBe(true); // Default value.

            var wrapped1 = wrapper.GetWrapper(item1);

            collectionA.Remove(item1);
            wrapped1.IsDisposed.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldNotDisposeOfItemOnRemoval()
        {
            var item1 = new Model { Name = "One" };
            var item2 = new Model { Name = "Two" };
            var collectionA = new ObservableCollection<Model> { item1, item2 };
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));

            wrapper.DisposeOfWrapperOnRemoval = false; // Turn off auto destruction of wrappers.

            var wrapped1 = wrapper.GetWrapper(item1);
            wrapped1.IsDisposed.ShouldBe(false);

            collectionA.Remove(item1);
            wrapped1.IsDisposed.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldBeAbleToChangeCreateWrapperFunction()
        {
            var wrapper1 = new ModelWrapper(new Model { Name = "One" });
            var wrapper2 = new ModelWrapper(new Model { Name = "Two" });

            var collectionA = new ObservableCollection<Model>();
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => wrapper1);

            collectionA.Add(wrapper1.Source);
            wrapper.Contains(wrapper1).ShouldBe(true);

            // ---

            wrapper.CreateWrapper = item => wrapper2;

            collectionA.Add(wrapper2.Source);
            wrapper.Contains(wrapper2).ShouldBe(true);
        }
        #endregion

        #region Errors
        [TestMethod]
        public void ShouldThrowWhenCreateFunctionNotPassedToConstructor()
        {
            var collectionA = new ObservableCollection<Model>();
            Should.Throw<ArgumentNullException>(() => new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, null));
        }

        [TestMethod]
        public void ShouldThrowWhenSourceCollectionNotPassedToConstructor()
        {
            Should.Throw<ArgumentNullException>(() => new ObservableCollectionWrapper<Model, ModelWrapper>(null, item => new ModelWrapper(item)));
        }

        [TestMethod]
        public void ShouldThrowWhenCreateWrapperPropertySetToNull()
        {
            var collectionA = new ObservableCollection<Model>();
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));
            Should.Throw<ArgumentNullException>(() => wrapper.CreateWrapper = null);
        }
        #endregion

        #region Events

        [TestMethod]
        public void ShouldReportAddedItems()
        {
            // Setup initial conditions.
            var item1 = new Model { Name = "One" };
            var collectionA = new ObservableCollection<Model>();
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));

            // Wire up events.
            var events = new List<ObservableCollectionWrapperEventArgs<Model, ModelWrapper>>();
            wrapper.ItemAdded += (s, e) => events.Add(e);

            // Alter collection.
            collectionA.Add(item1);

            // Ensure event fired.
            events.ElementAt(0).Source.ShouldBe(item1);
            events.ElementAt(0).Wrapper.ShouldBe(wrapper.GetWrapper(item1));
        }

        [TestMethod]
        public void ShouldReportRemovedItems()
        {

            // Setup initial conditions.
            var item1 = new Model { Name = "One" };
            var collectionA = new ObservableCollection<Model>();
            var wrapper = new ObservableCollectionWrapper<Model, ModelWrapper>(collectionA, item => new ModelWrapper(item));

            // Wire up events.
            var events = new List<ObservableCollectionWrapperEventArgs<Model, ModelWrapper>>();
            wrapper.ItemRemoved += (s, e) => events.Add(e);

            // Setup collection.
            collectionA.Add(item1);
            var wrapper1 = wrapper.GetWrapper(item1);

            // Alter collection.
            collectionA.Remove(item1);

            // Ensure event fired.
            events.ElementAt(0).Source.ShouldBe(item1);
            events.ElementAt(0).Wrapper.ShouldBe(wrapper1);
        }
        #endregion

        #region Sample Data
        private class Model
        {
            public string Name { get; set; }
        }

        private class ModelWrapper : IDisposable
        {
            public bool IsDisposed { get; set; }
            public Model Source { get; set; }
            public ModelWrapper(Model source)
            {
                Source = source;
            }
            public void Dispose() { IsDisposed = true; }
        }

        private class StringWrapper : ViewModelBase
        {
            public string Source { get; private set; }
            public StringWrapper(string source)
            {
                Source = source;
            }
        }
        #endregion
    }
}
