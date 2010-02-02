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

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.AttachedBehavior;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.AttachedBehavior
{
    [TestClass]
    public class AutoRowDefinitionsTest
    {
        #region Head
        private ObservableCollection<object> collection;
        private AutoRowDefinitions behavior = new AutoRowDefinitions();
        private Grid grid;

        [TestInitialize]
        public void Initialize()
        {
            collection = new ObservableCollection<object>();
            behavior = new AutoRowDefinitions {ItemsSource = collection};

            grid = new Grid();
            Behaviors.SetAutoRowDefinitions(grid, behavior);
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldHaveCorrectDefaultPropertyValues()
        {
            behavior.RowHeight.ShouldBe(default(GridLength));
            behavior.MinRowHeight.ShouldBe(double.NaN);
            behavior.MaxRowHeight.ShouldBe(double.NaN);
        }

        [TestMethod]
        public void ShouldSetHeightValues()
        {
            collection.Add(1);

            var defaultRow = new RowDefinition();

            var row = grid.RowDefinitions[0];
            row.Height.ShouldBe(defaultRow.Height);
            row.MinHeight.ShouldBe(0d);
            row.MaxHeight.ShouldBe(defaultRow.MaxHeight);

            behavior.RowHeight = new GridLength(50);
            behavior.MinRowHeight = 10;
            behavior.MaxRowHeight = 60;

            collection.Add(2);
            row = grid.RowDefinitions[1];
            row.Height.ShouldBe(new GridLength(50));
            row.MinHeight.ShouldBe(10d);
            row.MaxHeight.ShouldBe(60d);
        }

        [TestMethod]
        public void ShouldHaveObservableCollection()
        {
            collection = new ObservableCollection<object>();
            behavior = new AutoRowDefinitions();

            behavior.ItemsSource.ShouldBe(null);
            behavior.ObservableCollection.ShouldBe(null);

            behavior.ItemsSource = collection;
            behavior.ItemsSource.ShouldNotBe(null);
            behavior.ObservableCollection.ShouldNotBe(null);
            behavior.ObservableCollection.ShouldBe(collection);

            behavior.ItemsSource = new[] { "one", "two" };
            behavior.ItemsSource.ShouldNotBe(null);
            behavior.ObservableCollection.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldAddRowsAutomatically()
        {
            grid.RowDefinitions.Count.ShouldBe(0);
            collection.Add("one");

            behavior.RowDefinitions.ShouldNotBe(null);
            behavior.RowDefinitions.Count.ShouldBe(1);
            grid.RowDefinitions.Count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldRemoveRowsAutomatically()
        {
            collection.Count.ShouldBe(0);
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);

            grid.RowDefinitions.Count.ShouldBe(3);
            collection.RemoveAt(1);
            grid.RowDefinitions.Count.ShouldBe(2);
            collection.RemoveAt(0);
            grid.RowDefinitions.Count.ShouldBe(1);
            collection.RemoveAt(0);
            grid.RowDefinitions.Count.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldSyncRowsCorrectlyOnReplaceOperation()
        {
            collection.Count.ShouldBe(0);

            var item1 = new Border();
            var item2 = new Border();
            var item3 = new Border();
            collection.Add(item1);
            collection.Add(item2);

            grid.RowDefinitions.Count.ShouldBe(2);
            collection[0] = item2;
            grid.RowDefinitions.Count.ShouldBe(2);
        }

        [TestMethod]
        public void ShouldClearRowsAutomatically()
        {
            grid.RowDefinitions.Count.ShouldBe(0);
            collection.Count.ShouldBe(0);
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);

            grid.RowDefinitions.Count.ShouldBe(3);
            collection.Clear();
            grid.RowDefinitions.Count.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldLoadColletionOnAttach()
        {
            // Reset test objects.
            collection = new ObservableCollection<object>();
            behavior = new AutoRowDefinitions { ItemsSource = collection };
            grid = new Grid();
            
            // Items added prior to attaching behavior.
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);

            // Attach behavior.
            grid.RowDefinitions.Count.ShouldBe(0);
            Behaviors.SetAutoRowDefinitions(grid, behavior);
            grid.RowDefinitions.Count.ShouldBe(3);
        }
        #endregion
    }
}
