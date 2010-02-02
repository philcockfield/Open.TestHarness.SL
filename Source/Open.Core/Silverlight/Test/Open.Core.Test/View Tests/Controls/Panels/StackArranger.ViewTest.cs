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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Containers
{
    [ViewTestClass]
    public class StackArrangerViewTest
    {
        #region Head
        private Placeholder child1;
        private Placeholder child2;
        private Placeholder child3;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(SampleCanvas control)
        {
            control.Background = StyleResources.Colors["Brush.Black.010"] as SolidColorBrush;
            control.Arranger = new StackArranger(control.Children, Orientation.Vertical, null, null);

            child1 = CreateChild("One", Colors.Red);
            child2 = CreateChild("Two", Colors.Green);
            child3 = CreateChild("Three", Colors.Blue);

            if (control.Children.Count == 0)
            {
                control.Children.Add(child1);
                control.Children.Add(child2);
                control.Children.Add(child3);
            }

            Orientation_Vertical(control);
        }

        private static Placeholder CreateChild(string name, Color color)
        {
            return new Placeholder
                       {
                           Name = name + Guid.NewGuid(),
                           Text = name,
                           Width = double.NaN,
                           Margin = new Thickness(3),
                           Color = color,
                       };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void UpdateLayout(SampleCanvas control)
        {
            control.UpdateLayout();
        }

        [ViewTest]
        public void Orientation_Vertical(SampleCanvas control)
        {
            control.Width = 200;
            control.Height = double.NaN;
            foreach (FrameworkElement child in control.Children)
            {
                child.Width = double.NaN;
                child.Height = 100;
            }
            control.Arranger.Orientation = Orientation.Vertical;
            UpdateLayout(control);
        }

        [ViewTest]
        public void Orientation_Horizontal(SampleCanvas control)
        {
            control.Width = double.NaN;
            control.Height = 200;
            foreach (FrameworkElement child in control.Children)
            {
                child.Width = 100;
                child.Height = double.NaN;
            }
            control.Arranger.Orientation = Orientation.Horizontal;
            UpdateLayout(control);
        }


        [ViewTest]
        public void Set_Children_Width_120(SampleCanvas control)
        {
            foreach (FrameworkElement child in control.Children)
            {
                child.Width = 120;
            }
            UpdateLayout(control);
        }


        [ViewTest]
        public void Set_Children_Height_80(SampleCanvas control)
        {
            foreach (FrameworkElement child in control.Children)
            {
                child.Height = 80;
            }
            UpdateLayout(control);
        }


        [ViewTest]
        public void Exclude_Second_Child(SampleCanvas control)
        {
            control.Arranger.Exclude = (element => element == child2); 
        }

        [ViewTest]
        public void Stop_Excluding(SampleCanvas control)
        {
            control.Arranger.Exclude = null;
        }

        [ViewTest]
        public void Add_Child(SampleCanvas control)
        {
            var child = CreateChild((control.Children.Count + 1).ToString(), Colors.Orange);
            control.Children.Add(child);
        }
        #endregion

        public class SampleCanvas : Panel
        {
            public StackArranger Arranger { get; set; }

            protected override Size MeasureOverride(Size availableSize)
            {
                return Arranger == null ? availableSize : Arranger.Measure(availableSize) ;
            }

            protected override Size ArrangeOverride(Size finalSize)
            {
                return Arranger == null ? finalSize : Arranger.Arrange(finalSize);
            }
        }

    }
}
