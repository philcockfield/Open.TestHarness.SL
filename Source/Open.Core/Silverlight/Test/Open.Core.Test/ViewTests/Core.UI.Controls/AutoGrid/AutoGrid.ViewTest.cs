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
using System.Windows.Media;
using Open.Core.Common.Testing;
using System.Diagnostics;
using Open.Core.Common;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Primitives
{
    [ViewTestClass]
    public class AutoGridViewTest
    {
        #region Head
        private ObservableCollection<SampleModel> collection;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(AutoGridTestControl control)
        {
            var grid = control.AutoGrid;

            grid.Width = 400;
            grid.Height = 300;
            grid.Background = StyleResources.Colors["Brush.Black.010"] as Brush;

            collection = new ObservableCollection<SampleModel>();
            grid.ItemsSource = collection;

            Add_Item(control);
            Add_Item(control);
            Add_Item(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Add_Item(AutoGridTestControl control)
        {
            collection.Add(SampleModel.Create());
            Write(control);
        }

        [ViewTest]
        public void Add_Item_After_First(AutoGridTestControl control)
        {
            var index = collection.Count == 0 ? 0 : 1;
            collection.Insert(index, SampleModel.Create());
            Write(control);
        }

        [ViewTest]
        public void Remove_Item(AutoGridTestControl control)
        {
            if (collection.Count == 0) return;
            collection.RemoveAt(0);
            Write(control);
        }


        [ViewTest]
        public void Add_Multiple_Items(AutoGridTestControl control)
        {
            for (int i = 0; i < 15; i++)
            {
                Add_Item(control);
            }
        }

        [ViewTest]
        public void Remove_Multiple_Items(AutoGridTestControl control)
        {
            for (int i = 0; i < 15; i++)
            {
                Remove_Item(control);
            }
        }

        [ViewTest]
        public void Set_Heights(AutoGridTestControl control)
        {
            var grid = control.AutoGrid;
            grid.RowHeight = new GridLength(20);
        }

        private static void Write(AutoGridTestControl control)
        {
            var grid = control.AutoGrid;
            Debug.WriteLine("RowDefinitions.Count: " + grid.RowDefinitions.Count + " | ColumnDefinitions.Count: " + grid.ColumnDefinitions.Count);
            Debug.WriteLine("grid.Children.Count: " + grid.Children.Count);
            Debug.WriteLine("");
        }
        #endregion

        #region Sample Data
        public class SampleModel : ViewModelBase
        {
            private  static int count;
            public SampleModel()
            {
                count++;
                Count = count;
            }

            public int Count { get; private set; }
            public string Name { get; set; }
            public int Age { get; set; }

            public static SampleModel Create()
            {
                return new SampleModel
                           {
                               Name = RandomData.GetDisplayName(), 
                               Age = RandomData.Random.Next(1, 90)
                           };
            }
        }
        #endregion
    }
}
