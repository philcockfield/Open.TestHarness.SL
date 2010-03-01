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
using Open.Core.Common.Testing;

namespace Open.Core.Test.Extensions
{
    [TestClass]
    public class CollectionExtensionsTest
    {
        #region Tests
        [TestMethod]
        public void ShouldRemoveAllFromCollection()
        {
            var collection = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6 };
            collection.Count.ShouldBe(6);

            collection.RemoveAll();
            collection.Count.ShouldBe(0);

            collection = new ObservableCollection<int>();
            collection.RemoveAll();
        }

        [TestMethod]
        public void ShouldAddRange()
        {
            var collection = new ObservableCollection<int>();
            var range = new[] { 1, 2, 3, 4, 5, 6 };
            collection.AddRange(range);
            collection.Count.ShouldBe(6);

            collection = new ObservableCollection<int> { 9 };
            collection.AddRange(range);
            collection[0].ShouldBe(9);
            collection[1].ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotFailWhenNullItemsPassedToAddRange()
        {
            var collection = new ObservableCollection<int>();
            collection.AddRange((int[])null);
            collection.AddRange((IEnumerable<int>)null);
        }

        [TestMethod]
        public void ShouldAddRangeFromArray()
        {
            var collection = new ObservableCollection<int>();
            collection.AddRange(1, 2, 3, 4);
            collection.Count.ShouldBe(4);
        }

        [TestMethod]
        public void ShouldDisposeOfCollection()
        {
            var list = new List<DisposableStub>
                           {
                               new DisposableStub(),
                               new DisposableStub(),
                               null,
                               new DisposableStub(),
                           };
            list.DisposeOfChildren();
            foreach (var stub in list)
            {
                if (stub != null) stub.IsDisposed.ShouldBe(true);
            }

            // ---

            ((List<DisposableStub>)null).DisposeOfChildren();
        }

        [TestMethod]
        public void ShouldDisposeOfDisposableItemsWithinGeneralCollection()
        {
            var stub1 = new DisposableStub();
            var stub2 = new DisposableStub();

            var list = new List<object>
                           {
                               stub1,
                               "string",
                               42,
                               null,
                               stub2,
                           };
            list.DisposeOfChildren();

            stub1.IsDisposed.ShouldBe(true);
            stub2.IsDisposed.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldGetNextItem()
        {
            var item1 = new Stub();
            var item2 = new Stub();
            var item3 = new Stub();
            var item4 = new Stub();
            var list = new List<Stub> {item1, item2, item3};

            // ---

            ((List<Stub>)null).NextItem(item1, true).ShouldBe(null);
            list.NextItem(null, true).ShouldBe(null);
            list.NextItem(item4, true).ShouldBe(null);

            // ---

            list.NextItem(item1, false).ShouldBe(item2);
            list.NextItem(item2, false).ShouldBe(item3);
            list.NextItem(item3, false).ShouldBe(null);

            // ---

            list.NextItem(item1, true).ShouldBe(item2);
            list.NextItem(item2, true).ShouldBe(item3);
            list.NextItem(item3, true).ShouldBe(item1);

            // ---

            list = new List<Stub> {item1};
            list.NextItem(item1, false).ShouldBe(null);
            list.NextItem(item1, true).ShouldBe(item1);
        }

        [TestMethod]
        public void ShouldGetPreviousItem()
        {
            var item1 = new Stub();
            var item2 = new Stub();
            var item3 = new Stub();
            var item4 = new Stub();
            var list = new List<Stub> { item1, item2, item3 };

            // ---

            ((List<Stub>)null).PreviousItem(item1, true).ShouldBe(null);
            list.PreviousItem(null, true).ShouldBe(null);
            list.PreviousItem(item4, true).ShouldBe(null);

            // ---

            list.PreviousItem(item3, false).ShouldBe(item2);
            list.PreviousItem(item2, false).ShouldBe(item1);
            list.PreviousItem(item1, false).ShouldBe(null);

            // ---

            list.PreviousItem(item3, true).ShouldBe(item2);
            list.PreviousItem(item2, true).ShouldBe(item1);
            list.PreviousItem(item1, true).ShouldBe(item3);

            // ---

            list = new List<Stub> { item1 };
            list.PreviousItem(item1, false).ShouldBe(null);
            list.PreviousItem(item1, true).ShouldBe(item1);
        }

        [TestMethod]
        public void ShouldDetermineIfEnumerableIsEmpty()
        {
            ((List<Stub>) null).IsEmpty().ShouldBe(true);

            var list = new List<Stub> ();
            list.IsEmpty().ShouldBe(true);
            
            list.Add(new Stub());
            list.IsEmpty().ShouldBe(false);
        }

        [TestMethod]
        public void ShouldRemoveDuplicateEntires()
        {
            var stub1 = new Stub{Text = "One"};
            var stub2 = new Stub{Text = "Two"};
            var list = new List<Stub> { stub1, stub1, stub2, stub1, stub2 };

            var distinct = list.Distinct((s1, s2) => s1 == s2);
            distinct.Count().ShouldBe(2);
            distinct.First().ShouldBe(stub1);
            distinct.Last().ShouldBe(stub2);
        }

        [TestMethod]
        public void ShouldNotHaveDuplicateEntires()
        {
            var list = new List<Stub> { new Stub(), new Stub(), new Stub() };
            list.Distinct((s1, s2) => s1 == s2).Count().ShouldBe(3);
        }

        [TestMethod]
        public void ShouldTakeEmptyParamsInUniqueMethod()
        {
            new List<Stub>().Distinct((s1, s2) => s1 == s2).Count().ShouldBe(0);
            ((List<Stub>)null).Distinct((s1, s2) => s1 == s2).ShouldBe(null);
        }
        #endregion

        #region Stubs
        private class DisposableStub : ModelBase
        {
        }

        private class Stub : ModelBase
        {
            public string Text { get; set; }
        }
        #endregion
    }
}
