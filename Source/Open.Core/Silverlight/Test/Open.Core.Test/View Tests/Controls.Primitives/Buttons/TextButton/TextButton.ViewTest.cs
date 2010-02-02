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
using System.Diagnostics;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Controls.Primitives.Buttons
{
    [ViewTestClass]
    public class TextButtonViewTest
    {
        #region Head
        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(TextButtonTestControl control)
        {
            // Wire up events.
            control.button1.Click += delegate { Debug.WriteLine("!! CLICK: Button1"); };
            control.button2.Click += delegate { Debug.WriteLine("!! CLICK: Button2"); };

            var t = new TextBlock();

            Debug.WriteLine("" + t.FontSize);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Font_Large(TextButtonTestControl control)
        {
            control.button1.FontSize = 24;
            control.button2.FontSize = 24;
        }

        [ViewTest]
        public void Font_Small(TextButtonTestControl control)
        {
            control.button1.FontSize = 11;
            control.button2.FontSize = 11;
        }

        [ViewTest]
        public void Clear_Icons(TextButtonTestControl control)
        {
            control.button1.LeftIcon = null;
            control.button1.RightIcon = null;
        }

        [ViewTest]
        public void Set_Icon__Left(TextButtonTestControl control)
        {
            control.button1.LeftIcon = SilkIcons.Accept;
        }

        [ViewTest]
        public void Set_Icon__Right(TextButtonTestControl control)
        {
            control.button1.RightIcon = SilkIcons.Brick;
        }
        #endregion
    }
}
