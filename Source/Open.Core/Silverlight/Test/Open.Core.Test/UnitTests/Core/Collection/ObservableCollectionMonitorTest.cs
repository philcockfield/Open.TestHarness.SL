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
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Collection;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Common.Collection
{
    [TestClass]
    public class ObservableCollectionMonitorTest : SilverlightTest
    {
        #region Head
        private ChildPropertyChangedEventArgs<Sample> args;

        [TestInitialize]
        public void TestInitialize()
        {
            args = null;
        }


        private void Handle_Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            args = new ChildPropertyChangedEventArgs<Sample>(e.PropertyName, sender as Sample);
        }

        private void WireEvent(Sample item, bool add)
        {
            if (add) item.PropertyChanged += Handle_Item_PropertyChanged;
            else item.PropertyChanged -= Handle_Item_PropertyChanged;
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldExposeSourceCollection()
        {
            var collection = new ObservableCollection<Sample>();
            var monitor = new ObservableCollectionMonitor<Sample>(collection, (c, item) => WireEvent(item, true), (c, item) => WireEvent(item, false));
            monitor.Collection.ShouldBe(collection);
        }

        [TestMethod]
        public void ShouldReportChangeWhenExistingChildrenChange()
        {
            var collection = new ObservableCollection<Sample> { new Sample(), new Sample(), new Sample() };
            var monitor = new ObservableCollectionMonitor<Sample>(collection, (c, item) => WireEvent(item, true), (c, item) => WireEvent(item, false));

            collection[1].Text = "Value";

            args.PropertyName.ShouldBe(Sample.PropText);
            args.Source.ShouldBe(collection[1]);
        }

        [TestMethod]
        public void ShouldReportChangeWhenAddedChildrenChange()
        {
            var collection = new ObservableCollection<Sample>();
            var monitor = new ObservableCollectionMonitor<Sample>(collection, (c, item) => WireEvent(item, true), (c, item) => WireEvent(item, false));

            var child = new Sample();
            collection.Add(child);

            child.Text = "Value";
            args.PropertyName.ShouldBe(Sample.PropText);
            args.Source.ShouldBe(child);
        }

        [TestMethod]
        public void ShouldStopReportingChangesWhenRemovedFromCollection()
        {
            var collection = new ObservableCollection<Sample> { new Sample(), new Sample(), new Sample() };
            var monitor = new ObservableCollectionMonitor<Sample>(collection, (c, item) => WireEvent(item, true), (c, item) => WireEvent(item, false));

            var child = collection[0];
            collection.Remove(child);
            child.Text = "Value";

            args.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldStopReportingChangesWhenRemovedByReplaceOperationInCollection()
        {
            var collection = new ObservableCollection<Sample> { new Sample(), new Sample(), new Sample() };
            var monitor = new ObservableCollectionMonitor<Sample>(collection, (c, item) => WireEvent(item, true), (c, item) => WireEvent(item, false));

            var childOld = collection[0];
            var childNew = new Sample();
            collection[0] = childNew;

            childOld.Text = "Value";
            args.ShouldBe(null);

            childNew.Text = "Value";
            args.PropertyName.ShouldBe(Sample.PropText);
            args.Source.ShouldBe(childNew);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ShouldThrowWhenClearCalled()
        {
            var collection = new ObservableCollection<Sample>();
            var monitor = new ObservableCollectionMonitor<Sample>(collection, (c, item) => WireEvent(item, true), (c, item) => WireEvent(item, false));
            collection.Clear();
        }
        #endregion

        #region Sample Data
        private class Sample : NotifyPropertyChangedBase
        {
            public const string PropText = "Text";
            private string text;
            public string Text
            {
                get { return text; }
                set
                {
                    text = value;
                    OnPropertyChanged(PropText);
                }
            }
        }
        #endregion
    }
}
