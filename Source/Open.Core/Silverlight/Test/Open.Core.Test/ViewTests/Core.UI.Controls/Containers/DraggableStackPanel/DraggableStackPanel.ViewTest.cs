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
using System.Diagnostics;
using Open.Core.Common;
using Open.Core.Common.AttachedBehavior;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Containers
{
    [ViewTestClass]
    public class DraggableStackPanelViewTest
    {
        #region Head
        private readonly SampleViewModel viewModel = new SampleViewModel();
        private int instanceCounter = 3;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(DraggableStackPanelTestControl control)
        {
            control.itemsControl.DataContext = viewModel;
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Orientation_Vertical(DraggableStackPanelTestControl control)
        {
            var stack = control.DraggableStackPanel;
            stack.Width = 200;
            stack.Height = 400;
            foreach (FrameworkElement child in stack.Children)
            {
                child.Width = double.NaN;
                child.Height = 100;
            }
            stack.Orientation = Orientation.Vertical;
            stack.UpdateLayout();
        }

        [ViewTest]
        public void Orientation_Horizontal(DraggableStackPanelTestControl control)
        {
            var stack = control.DraggableStackPanel;
            stack.Width = 400;
            stack.Height = 200;
            foreach (FrameworkElement child in stack.Children)
            {
                child.Width = 100;
                child.Height = double.NaN;
            }
            stack.Orientation = Orientation.Horizontal;
            stack.UpdateLayout();
        }

        [ViewTest]
        public void Change_Order_1_and_2(DraggableStackPanelTestControl control)
        {
            var items = viewModel.Items;
            var item1 = items[0];
            var item2 = items[1];

            items[0] = item2;
            items[1] = item1;
        }

        [ViewTest]
        public void Change_Order_1_and_3(DraggableStackPanelTestControl control)
        {
            var items = viewModel.Items;
            var item1 = items[0];
            var item2 = items[2];

            items[0] = item2;
            items[2] = item1;
        }

        [ViewTest]
        public void Change_Order_2_and_3(DraggableStackPanelTestControl control)
        {
            var items = viewModel.Items;
            var item1 = items[1];
            var item2 = items[2];

            items[1] = item2;
            items[2] = item1;
        }

        [ViewTest]
        public void Add_To_End(DraggableStackPanelTestControl control)
        {
            instanceCounter++;
            viewModel.Items.Add(new SampleItemViewModel { Text = "Item " + instanceCounter});
        }

        [ViewTest]
        public void Insert_At_Beginning(DraggableStackPanelTestControl control)
        {
            instanceCounter++;
            viewModel.Items.Insert(0, new SampleItemViewModel { Text = "Item " + instanceCounter });
        }

        [ViewTest]
        public void Remove_First(DraggableStackPanelTestControl control)
        {
            viewModel.Items.RemoveAt(0);
        }

        [ViewTest]
        public void Toggle_SlideDuration(DraggableStackPanelTestControl control)
        {
            var panel = control.DraggableStackPanel;
            panel.SlideDuration = panel.SlideDuration == 1 ? 0.1 : 1;
            Debug.WriteLine("SlideDuration: " + panel.SlideDuration + " seconds");
        }

        [ViewTest]
        public void Toggle_DragContainment(DraggableStackPanelTestControl control)
        {
            var panel = control.DraggableStackPanel;
            panel.DragContainment = panel.DragContainment == DragContainment.FullyWithin
                ? DragContainment.PixelsWithin 
                : DragContainment.FullyWithin;
            Debug.WriteLine("DragContainment: " + panel.DragContainment);
        }

        [ViewTest]
        public void TEMP(DraggableStackPanelTestControl control)
        {
            var panel = control.DraggableStackPanel;

            panel.Height = panel.ActualHeight == 400 ? 450 : 400;

        }

        #endregion

        #region Sample Data
        public class SampleViewModel : ViewModelBase
        {
            #region Head
            private readonly ObservableCollection<SampleItemViewModel> items = new ObservableCollection<SampleItemViewModel>();
            public SampleViewModel()
            {
                Items.Add(new SampleItemViewModel { Text = "One" });
                Items.Add(new SampleItemViewModel { Text = "Two" });
                Items.Add(new SampleItemViewModel { Text = "Three" });

                Text = "Lorem psum";
            }
            #endregion

            #region Properties
            public ObservableCollection<SampleItemViewModel> Items
            {
                get { return items; }
            }

            public string Text { get; set; }
            #endregion
        }

        public class SampleItemViewModel : ViewModelBase
        {
            #region Head
            public const string PropText = "Text";
            private string text;
            private static int instanceCounter;

            public SampleItemViewModel()
            {
                instanceCounter++;
                Instance = instanceCounter;
            }
            #endregion

            #region Properties
            public int Instance { get; private set; }

            public string Text
            {
                get { return text; }
                set { text = value; OnPropertyChanged(PropText); }
            }
            #endregion

            #region Methods
            public override string ToString()
            {
                return string.Format("{0} ({1})", Text, Instance);
            }
            #endregion
        }
        #endregion
    }
}
