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

using System.Linq;
using System.Collections.ObjectModel;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Behavior
{
    [ViewTestClass]
    public class Behavior__SyncWithScrollViewerWidthViewTest
    {
        #region Head
        private readonly ObservableCollection<Stub> items = new ObservableCollection<Stub>();


        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(SyncWithScrollViewerWidthTestControl control)
        {
            control.Width = 300;
            control.Height = 200;

            control.listBox.ItemsSource = items;
            LoadSampleData(1);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Add_Item(SyncWithScrollViewerWidthTestControl control)
        {
            AddItem();
        }

        [ViewTest]
        public void Remove_Last_Item(SyncWithScrollViewerWidthTestControl control)
        {
            items.Remove(items.LastOrDefault());
        }
        #endregion

        #region Internal
        private void LoadSampleData(int total)
        {
            for (var i = 0; i < total; i++) AddItem();
        }

        private void AddItem()
        {
            var item = new Stub();
            items.Add(item);
        }
        #endregion

        #region Stubs
        public class Stub : ModelBase
        {
            private string text = RandomData.LoremIpsum(1, 3);
            public string Text
            {
                get { return text; }
                set { text = value; OnPropertyChanged<Stub>(m => m.Text); }
            }
        }
        #endregion
    }
}
