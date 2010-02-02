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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using Open.Core.Common;
using Open.Core.Common.AttachedBehavior;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Behavior
{
    [ViewTestClass]
    public class Behavior__PositionSliderViewTest
    {
        #region Head
        private Placeholder element;
        private Draggable dragBehavior;
        private PositionSlider sliderBehavior;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(Canvas control)
        {
            element = new Placeholder();
            dragBehavior = new Draggable();
            sliderBehavior = new PositionSlider{Duration = 1};

            control.Width = 500;
            control.Height = 500;
            control.Background = StyleResources.Colors["Brush.Black.005"] as Brush;
            control.Children.Add(element);

            control.SetPosition(element, 50, 50);

            Behaviors.SetDraggable(element, dragBehavior);
            Behaviors.SetPositionSlider(element, sliderBehavior);

            sliderBehavior.SlideStarted += delegate { Debug.WriteLine("!! SlideStarted"); };
            sliderBehavior.SlideComplete += delegate { Debug.WriteLine("!! SlideComplete"); };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Read_Properties(Canvas control)
        {
            Debug.WriteLine("Position: " + sliderBehavior.Position);
            Debug.WriteLine("X: " + sliderBehavior.X);
            Debug.WriteLine("Y: " + sliderBehavior.Y);
            Debug.WriteLine("Canvas: " + sliderBehavior.Canvas);
            Debug.WriteLine("IsWithinCanvas: " + sliderBehavior.IsWithinCanvas);
            Debug.WriteLine("IsAnimating: " + sliderBehavior.IsAnimating);
            Debug.WriteLine("Duration: " + sliderBehavior.Duration);
            Debug.WriteLine("IsSlideEnabled: " + sliderBehavior.IsSlideEnabled);
            Debug.WriteLine("");
        }

        [ViewTest]
        public void UpdatePositionValue(Canvas control)
        {
            sliderBehavior.UpdatePositionValue();
        }

        [ViewTest]
        public void Go_To_TopLeft(Canvas control)
        {
            sliderBehavior.Position = new Point(0,0);
            Read_Properties(control);
        }

        [ViewTest]
        public void Add_X_Amount(Canvas control)
        {
            sliderBehavior.X += 100;
            Read_Properties(control);
        }

        [ViewTest]
        public void Add_Y_Amount(Canvas control)
        {
            sliderBehavior.Y += 100;
            Read_Properties(control);
        }

        [ViewTest]
        public void Subtract_X_Amount(Canvas control)
        {
            sliderBehavior.X -= 100;
            Read_Properties(control);
        }

        [ViewTest]
        public void Subtract_Y_Amount(Canvas control)
        {
            sliderBehavior.Y -= 100;
            Read_Properties(control);
        }

        [ViewTest]
        public void Toggle_IsSlideEnabled(Canvas control)
        {
            sliderBehavior.IsSlideEnabled = !sliderBehavior.IsSlideEnabled;
            Read_Properties(control);
        }

        [ViewTest]
        public void Duration_Slow(Canvas control)
        {
            sliderBehavior.Duration = 1;
        }

        [ViewTest]
        public void Duration_Fast(Canvas control)
        {
            sliderBehavior.Duration = 0.2;
        }
        #endregion
    }
}

