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
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Functional_Adorners
{
    [TestClass]
    public class StackArrangerTest : SilverlightTest
    {

        #region Tests
        [TestMethod]
        public void ShouldArrangeVertically()
        {
            var canvas = GetCanvas();
            var arranger = new StackArranger(canvas.Children, Orientation.Vertical);

            var size = new Size(200, 800);
            List<ElementBounds> layout;
            arranger.Measure(size);
            arranger.ArrangeCalculate(size, out layout);

            layout[0].Bounds.Top.ShouldBe(0.0);
            layout[1].Bounds.Top.ShouldBe(100.0);
            layout[2].Bounds.Top.ShouldBe(200.0);

            foreach (FrameworkElement child in canvas.Children)
            {
                child.Margin = new Thickness(5);
            }

            arranger.Measure(size);
            arranger.ArrangeCalculate(size, out layout);

            layout[0].Bounds.Top.ShouldBe(0.0);
            layout[1].Bounds.Top.ShouldBe(110.0);
            layout[2].Bounds.Top.ShouldBe(220.0);
        }

        [TestMethod]
        public void ShouldArrangeHorizontally()
        {
            var canvas = GetCanvas();
            var arranger = new StackArranger(canvas.Children, Orientation.Horizontal);

            var size = new Size(800, 200);
            List<ElementBounds> layout;
            arranger.Measure(size);
            arranger.ArrangeCalculate(size, out layout);

            layout[0].Bounds.Left.ShouldBe(0.0);
            layout[1].Bounds.Left.ShouldBe(100.0);
            layout[2].Bounds.Left.ShouldBe(200.0);

            foreach (FrameworkElement child in canvas.Children)
            {
                child.Margin = new Thickness(5);
            }

            arranger.Measure(size);
            arranger.ArrangeCalculate(size, out layout);

            layout[0].Bounds.Left.ShouldBe(0.0);
            layout[1].Bounds.Left.ShouldBe(110.0);
            layout[2].Bounds.Left.ShouldBe(220.0);
        }

        [TestMethod]
        public void ShouldStretchToFit()
        {
            var canvas = GetCanvas();
            var arranger = new StackArranger(canvas.Children, Orientation.Vertical);

            var size = new Size(200, 800);
            List<ElementBounds> layout;
            arranger.Measure(size);
            arranger.ArrangeCalculate(size, out layout);

            // Not stretching.
            layout[0].Bounds.Width.ShouldBe(100.0);
            layout[1].Bounds.Width.ShouldBe(100.0);
            layout[2].Bounds.Width.ShouldBe(100.0);

            foreach (FrameworkElement child in canvas.Children)
            {
                child.Width = double.NaN;
            }

            arranger.Measure(size);
            arranger.ArrangeCalculate(size, out layout);

            layout[0].Bounds.Width.ShouldBe(200.0);
            layout[1].Bounds.Width.ShouldBe(200.0);
            layout[2].Bounds.Width.ShouldBe(200.0);

            // Horizontal
            arranger.Orientation = Orientation.Horizontal;
            size = new Size(800, 200);
            arranger.Measure(size);
            arranger.ArrangeCalculate(size, out layout);

            layout[0].Bounds.Height.ShouldBe(100.0);
            layout[1].Bounds.Height.ShouldBe(100.0);
            layout[2].Bounds.Height.ShouldBe(100.0);

            foreach (FrameworkElement child in canvas.Children)
            {
                child.Height = double.NaN;
            }

            arranger.Measure(size);
            arranger.ArrangeCalculate(size, out layout);

            layout[0].Bounds.Height.ShouldBe(200.0);
            layout[1].Bounds.Height.ShouldBe(200.0);
            layout[2].Bounds.Height.ShouldBe(200.0);
        }
        #endregion

        #region Internal
        private static Canvas GetCanvas()
        {
            var canvas = new Canvas{Width = 1000, Height = 1000};

            for (var i = 0; i < 3; i++)
            {
                var child = new Placeholder
                                {
                                    Width = 100,
                                    Height = 100,
                                    Margin = new Thickness(0)
                                };
                canvas.Children.Add(child);
            }

            return canvas;
        }
        #endregion
    }
}
