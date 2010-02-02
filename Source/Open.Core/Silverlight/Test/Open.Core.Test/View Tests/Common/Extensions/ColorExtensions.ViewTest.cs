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

using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;


namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Extensions
{
    [ViewTestClass]
    public class ColorExtensionsViewTest
    {
        #region Head
        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(Border control)
        {
            control.Width = 50;
            control.Height = 50;
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Black__100_percent(Border control)
        {
            SetBackground(control, Colors.Black, 1);
        }


        [ViewTest]
        public void Black__70_percent(Border control)
        {
            SetBackground(control, Colors.Black, 0.7);
        }

        [ViewTest]
        public void Black__50_percent(Border control)
        {
            SetBackground(control, Colors.Black, 0.5);
        }

        [ViewTest]
        public void Black__30_percent(Border control)
        {
            SetBackground(control, Colors.Black, 0.3);
        }

        [ViewTest]
        public void Black__10_percent(Border control)
        {
            SetBackground(control, Colors.Black, 0.1);
        }

        [ViewTest]
        public void Black__0_percent(Border control)
        {
            SetBackground(control, Colors.Black, 0);
        }

        [ViewTest]
        public void Green__50_percent(Border control)
        {
            SetBackground(control, Colors.Green, 0.5);
        }
        #endregion

        #region Internal
        private static void SetBackground(Border control, Color color, double opacity)
        {
            var alphaColor = color.ToAlpha(opacity);
            Debug.WriteLine("A: " + alphaColor.A);
            Debug.WriteLine("R: " + alphaColor.R);
            Debug.WriteLine("G: " + alphaColor.G);
            Debug.WriteLine("B: " + alphaColor.B);
            Debug.WriteLine("");
            control.Background = color.ToBrush(opacity);
        }
        #endregion
    }
}
