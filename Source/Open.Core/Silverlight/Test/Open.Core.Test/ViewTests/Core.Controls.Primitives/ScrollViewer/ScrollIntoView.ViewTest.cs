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
using System.Diagnostics;
using System.Linq;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Open.Core.Common;
using Open.Core.UI.Controls;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Primitives
{
    [ViewTestClass]
    public class ScrollIntoViewViewTest
    {
        #region Head
        private readonly ObservableCollection<Stub> items = new ObservableCollection<Stub>();
        private ScrollViewer scrollViewer;
        private ItemsControl itemsControl;
        private ScrollViewerMonitor<Placeholder> scrollViewerMonitor;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ScrollIntoViewTestControl control)
        {
            // Setup initial conditions.
            control.Width = 300;
            control.Height = 350;

            scrollViewer = control.scrollViewer;
            itemsControl = control.itemsControl;

            // Populate collection.
            PopulateCollection(10);

            // Setup binding.
            control.itemsControl.ItemsSource = items;

            // Setup the scroll-viewer-monitor.
            scrollViewerMonitor = new ScrollViewerMonitor<Placeholder>(scrollViewer);
            scrollViewerMonitor.TopElementChanged += delegate { Debug.WriteLine("!! TopElementChanged: " + scrollViewerMonitor.TopElement.Text); };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Toggle_ScrollViewer__Padding(ScrollIntoViewTestControl control)
        {
            scrollViewer.Padding = scrollViewer.Padding.Left == 0 ? new Thickness(5) : new Thickness(0);
        }

        [ViewTest]
        public void ScrollIntoView__First_Item(ScrollIntoViewTestControl control)
        {
            scrollViewer.ScrollToTop<Placeholder>(items.ElementAt(0));
        }

        [ViewTest]
        public void ScrollIntoView__Second_Item(ScrollIntoViewTestControl control)
        {
            scrollViewer.ScrollToTop<Placeholder>(items.ElementAt(1));
        }

        [ViewTest]
        public void ScrollIntoView__Last_Item(ScrollIntoViewTestControl control)
        {
            scrollViewer.ScrollToTop<Placeholder>(items.Last());
        }

        [ViewTest]
        public void Change_Elements_Height(ScrollIntoViewTestControl control)
        {
            foreach (var item in items)
            {
                var element = itemsControl.GetElementFromViewModel<Placeholder>(item);
                element.Height = RandomData.Random.Next(10, 120);
            }
        }

        [ViewTest]
        public void ScrollViewerMonitor__Toggle_IsActive(ScrollIntoViewTestControl control)
        {
            scrollViewerMonitor.IsActive = !scrollViewerMonitor.IsActive;
            Debug.WriteLine("IsActive: " + scrollViewerMonitor.IsActive);
        }

        [ViewTest]
        public void ScrollViewerMonitor__Refresh(ScrollIntoViewTestControl control)
        {
            scrollViewerMonitor.Refresh();
        }
        #endregion

        #region Internal
        private void PopulateCollection(int count)
        {
            items.RemoveAll();
            for (var i = 0; i < count; i++)
            {
                var item = new Stub {Text = "Item " + (i+1)};
                items.Add(item);
            }
        }
        #endregion

        #region Stubs
        public class Stub : ViewModelBase
        {
            public string Text
            {
                get { return GetPropertyValue<Stub, string>(m => m.Text, "My Value"); }
                set { SetPropertyValue<Stub, string>(m => m.Text, value, "My Value"); }
            }
        }
        #endregion
    }
}
