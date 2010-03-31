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
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Collection;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Common.Collection
{
    [TestClass]
    public class NotifyPropertyChangedCollectionMonitorTest : SilverlightTest
    {
        #region Tests
        [TestMethod]
        public void ShouldExposeSourceCollection()
        {
            var collection = new ObservableCollection<Sample>();
            var monitor = new NotifyPropertyChangedCollectionMonitor<Sample>(collection);
            monitor.Collection.ShouldBe(collection);
        }

        [TestMethod]
        public void ShouldReportChangeWhenExistingChildrenChange()
        {
            var collection = new ObservableCollection<Sample> { new Sample(), new Sample(), new Sample() };
            var monitor = new NotifyPropertyChangedCollectionMonitor<Sample>(collection);

            ChildPropertyChangedEventArgs<Sample> args = null;
            monitor.PropertyChanged += (sender, e) => args = e;

            collection[1].Text = "Value";

            args.PropertyName.ShouldBe(Sample.PropText);
            args.Source.ShouldBe(collection[1]);
        }

        [TestMethod]
        public void ShouldReportChangeWhenAddedChildrenChange()
        {
            var collection = new ObservableCollection<Sample>();
            var monitor = new NotifyPropertyChangedCollectionMonitor<Sample>(collection);

            var child = new Sample();
            collection.Add(child);

            ChildPropertyChangedEventArgs<Sample> args = null;
            monitor.PropertyChanged += (sender, e) => args = e;

            child.Text = "Value";
            args.PropertyName.ShouldBe(Sample.PropText);
            args.Source.ShouldBe(child);
        }

        [TestMethod]
        public void ShouldStopReportingChangesWhenRemovedFromCollection()
        {
            var collection = new ObservableCollection<Sample> { new Sample(), new Sample(), new Sample() };
            var monitor = new NotifyPropertyChangedCollectionMonitor<Sample>(collection);

            ChildPropertyChangedEventArgs<Sample> args = null;
            monitor.PropertyChanged += (sender, e) => args = e;

            var child = collection[0];
            collection.Remove(child);
            child.Text = "Value";

            args.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldStopReportingChangesWhenRemovedByReplaceOperationInCollection()
        {
            var collection = new ObservableCollection<Sample> { new Sample(), new Sample(), new Sample() };
            var monitor = new NotifyPropertyChangedCollectionMonitor<Sample>(collection);

            ChildPropertyChangedEventArgs<Sample> args = null;
            monitor.PropertyChanged += (sender, e) => args = e;

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
        public void ShouldThrowWhenClearCalled()
        {
            var collection = new ObservableCollection<Sample>();
            var monitor = new NotifyPropertyChangedCollectionMonitor<Sample>(collection);

            Should.Throw<NotSupportedException>(collection.Clear);
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
