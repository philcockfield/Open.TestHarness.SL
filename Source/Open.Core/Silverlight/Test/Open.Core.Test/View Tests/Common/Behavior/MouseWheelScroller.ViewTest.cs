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

using System.Collections.Generic;
using System.Windows.Controls;
using Open.Core.Common;
using Open.Core.UI.Controls;
using Open.Core.Common.AttachedBehavior;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Behavior
{
    [ViewTestClass]
    public class Behavior__MouseWheelScrollerViewTest
    {
        #region Tests
        [ViewTest(Default = true)]
        public void ScrollViewer(ScrollViewer control)
        {
            control.Width = 300;
            control.Height = 200;

            var placeholder = new Placeholder { Height = 500 };
            control.Content = placeholder;
            control.SetValue(Behaviors.MouseWheelScrollerProperty, new MouseWheelScroller());
        }

        [ViewTest]
        public void ListBox(ListBox control)
        {
            // Setup initial conditions.
            control.Width = 300;
            control.Height = 200;

            // Populate listbox with random data.
            var list = new List<string>();
            for (int i = 1; i <= 200; i++)
            {
                list.Add(string.Format("{0} - {1}", i, RandomData.LoremIpsum(1,3)));
            }
            control.ItemsSource = list;

            // Apply the behavior.
            control.SetValue(Behaviors.ListBoxMouseWheelScrollerProperty, new ListBoxMouseWheelScroller());
        }
        #endregion
    }
}
