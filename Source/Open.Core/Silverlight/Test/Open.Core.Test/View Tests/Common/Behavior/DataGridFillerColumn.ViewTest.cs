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
using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;
using Open.Core.Common.AttachedBehavior;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Behavior
{
    [ViewTestClass]
    public class Behavior__DataGridFillerColumnViewTest
    {
        #region Head
        private ViewModel viewModel;
        private DataGridFillerColumn behavior;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(DataGrid control)
        {
            control.Width = 500;
            control.Height = 200;
            control.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;

            behavior = new DataGridFillerColumn{ColumnIndex = 1};
            Behaviors.SetDataGridFillerColumn(control, behavior);

            viewModel = new ViewModel();
            viewModel.Load(5);
            control.ItemsSource = viewModel.Items;
        }
        #endregion

        #region Tests

        [ViewTest]
        public void Clear_Grid(DataGrid control)
        {
            viewModel.Items.Clear();
        }

        [ViewTest]
        public void Load_5_Items(DataGrid control)
        {
            viewModel.Load(5);
        }

        [ViewTest]
        public void Load_20_Items(DataGrid control)
        {
            viewModel.Load(20);
        }

        [ViewTest]
        public void Load_80_Items(DataGrid control)
        {
            viewModel.Load(80);
        }

        [ViewTest]
        public void Toggle_Width(DataGrid control)
        {
            control.Width = control.Width == 500 ? 600 : 500;
        }

        [ViewTest]
        public void Toggle_Border(DataGrid control)
        {
            control.BorderThickness = control.BorderThickness.Left > 0 
                                                    ? new Thickness(0) 
                                                    : new Thickness(1);
            behavior.UpdateWidth();
        }

        [ViewTest]
        public void Toggle_Horizontal_Scrollbar(DataGrid control)
        {
            control.HorizontalScrollBarVisibility = control.HorizontalScrollBarVisibility == ScrollBarVisibility.Auto
                                                    ? ScrollBarVisibility.Hidden 
                                                    : ScrollBarVisibility.Auto;
        }
        #endregion

        #region Sample Data
        public class ViewModel : ViewModelBase
        {
            public const string PropItems = "Items";
            private readonly ObservableCollection<RowItem> items = new ObservableCollection<RowItem>();
            public ObservableCollection<RowItem> Items { get { return items; } }
            private static RowItem CreateItem()
            {
                return new RowItem
                               {
                                   Text = "Text",
                                   Name = "Name",
                                   Value = "Value",
                                   Number = RandomData.Random.Next(),
                                   Date = DateTime.Now
                               };
            }

            public void Load(int total)
            {
                Items.Clear();
                for (var i = 0; i < total; i++)
                {
                    Items.Add(CreateItem());
                }
            }
        }

        public class RowItem 
        {
            public string Text { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public double Number { get; set; }
            public DateTime Date { get; set; }
        }
        #endregion
    }
}
