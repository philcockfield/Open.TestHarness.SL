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
using System.Diagnostics;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Primitives.Buttons
{
    [ViewTestClass]
    public class Buttons__VisualButtonViewTest
    {
        #region Head
        private ShapeButton shapeButton;
        private ImageButton imageButton;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(VisualButtonTestControl control)
        {
            // Setup initial conditions.
            shapeButton = control.shapeButton;
            imageButton = control.imageButton;

            // Setup sizes.
            shapeButton.SetSize(119, 119);
            imageButton.SetSize(119, 119);

            // Setup the image button.
            imageButton.Source = GetImage("Button.Default.png");
            imageButton.DisabledSource = GetImage("Button.Disabled.png");
            imageButton.OverSource = GetImage("Button.Over.png");
            imageButton.DownSource = GetImage("Button.Down.png");

            // Wire up events.
            shapeButton.Click += delegate { Output.Write(Colors.Orange,  "!! CLICK: ShapeButton"); };
            imageButton.Click += delegate { Output.Write(Colors.Orange, "!! CLICK: ImageButton"); };

            // Finish up.
            Offsets__Down_Only(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Offsets__Up_Down(VisualButtonTestControl control)
        {
            SetOffsets(shapeButton, new Point(-1, -1), new Point(1, 1));
            SetOffsets(imageButton, new Point(-1, -1), new Point(1, 1));
        }

        [ViewTest]
        public void Offsets__Down_Only(VisualButtonTestControl control)
        {
            SetOffsets(shapeButton, new Point(0, 0), new Point(1, 1));
            SetOffsets(imageButton, new Point(0, 0), new Point(1, 1));
        }

        [ViewTest]
        public void Offsets__None(VisualButtonTestControl control)
        {
            ResetOffsets(shapeButton);
            ResetOffsets(imageButton);
        }

        [ViewTest]
        public void Toggle_IsEnabled(VisualButtonTestControl control)
        {
            shapeButton.IsEnabled = !shapeButton.IsEnabled;
            imageButton.IsEnabled = !imageButton.IsEnabled;
        }

        [ViewTest]
        public void Toggle_Padding(VisualButtonTestControl control)
        {
            TogglePadding(shapeButton);
            TogglePadding(imageButton);
        }

        [ViewTest]
        public void Set_Color__Red(VisualButtonTestControl control)
        {
            control.shapeButton.SetBrushColors(Colors.Red);
        }

        [ViewTest]
        public void Set_Color__Black(VisualButtonTestControl control)
        {
            control.shapeButton.SetBrushColors(Colors.Black);
        }

        [ViewTest]
        public void ShapeButton__Change_DefaultBrush(VisualButtonTestControl control)
        {
            shapeButton.DefaultBrush = new SolidColorBrush(Colors.Red);
        }

        [ViewTest]
        public void ShapeButton__Change_Stretch(VisualButtonTestControl control)
        {
            shapeButton.Stretch = shapeButton.Stretch.NextValue<Stretch>();
            Debug.WriteLine("Stretch: " + shapeButton.Stretch);
        }

        [ViewTest]
        public void ShapeButton__Change_ShapePathData(VisualButtonTestControl control)
        {
            shapeButton.ShapePathData = Shapes.TriangleDown;
        }

        [ViewTest]
        public void Show__RemoveButton_14_x_14(RemoveButton control)
        {
            control.SetSize(14, 14);
        }

        [ViewTest]
        public void Show__RemoveButton_48_x_48(RemoveButton control)
        {
            control.SetSize(48, 48);
        }

        [ViewTest]
        public void Show__CrossButton(CrossButton control)
        {
            control.SetSize(22, 22);
        }

        [ViewTest]
        public void Show__TriangleButton(TriangleButton control)
        {
            control.SetSize(48, 48);
        }

        [ViewTest]
        public void Show__TriangleButton_Next_PointerDirection(TriangleButton control)
        {
            control.SetSize(48, 48);
            control.PointerDirection = control.PointerDirection.NextValue<ArrowDirection>();
        }

        [ViewTest]
        public void Show__RoundedPointerButton(RoundedPointerButton control)
        {
            control.SetSize(31, 48);
        }

        [ViewTest]
        public void Show__RoundedPointerButton_Next_PointerDirection(RoundedPointerButton control)
        {
            control.SetSize(31, 48);
            control.PointerDirection = control.PointerDirection.NextValue<ArrowDirection>();
        }

        [ViewTest]
        public void ImageButton__Change_Stretch(VisualButtonTestControl control)
        {
            imageButton.Stretch = imageButton.Stretch.NextValue<Stretch>();
            Output.Write("Stretch: " + imageButton.Stretch);
        }

        [ViewTest]
        public void ImageButton__Increase_Size(VisualButtonTestControl control)
        {
            imageButton.Stretch = Stretch.None;
            imageButton.Width = 250;
            imageButton.Height = 250;
            Output.Write("Stretch: " + imageButton.Stretch);
            Output.Write(string.Format("Width: {0} | Height: {1}", imageButton.Width, imageButton.Height));
        }
        #endregion

        #region Internal
        private static void SetOffsets(VisualButton button, Point overOffset, Point downOffset)
        {
            button.OverOffset = overOffset;
            button.DownOffset = downOffset;
        }

        private static void ResetOffsets(VisualButton button)
        {
            button.OverOffset = default(Point);
            button.DownOffset = default(Point);
        }

        private static ImageSource GetImage(string name)
        {
            var path = string.Format("/View Tests/Controls.Primitives/Buttons/Images/{0}", name);
            return path.ToImageSource();
        }

        private static void TogglePadding(Control control)
        {
            control.Padding = control.Padding.Left == 0 ? new Thickness(10) : new Thickness(0);
            Output.Write(control.GetType().Name);
            Output.Write("Padding: " + control.Padding);
            Output.Break();
        }
        #endregion
    }
}
