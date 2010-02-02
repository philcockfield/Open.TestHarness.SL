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

using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Causes a scrollable panel to scroll when on the mouse-wheel events.</summary>
    public class ListBoxMouseWheelScroller : Behavior<ListBox>
    {
        #region Head
        private ScrollViewer scroller;
        #endregion

        #region Event Handlers
        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Setup initial conditions.
            if (Scroller == null) return;

            // Calculate the offset (items).
            double offset = 1;
            if (e.Delta > 0) offset = offset*-1;
            offset = (Scroller.VerticalOffset) + offset;

            // Update the scroll position.
            Scroller.ScrollToVerticalOffset(offset);
        }
        #endregion

        #region Properties - Internal
        private ScrollViewer Scroller
        {
            get
            {
                // Setup initial conditions.
                if (scroller != null) return scroller;

                // Retrieve an initialize scroller on first call.
                scroller = AssociatedObject.FindFirstChildOfType<ScrollViewer>();

                // Finish up.
                return scroller;
            }
        }
        #endregion


        #region Methods
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseWheel += OnMouseWheel;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseWheel -= OnMouseWheel;
            scroller = null;
        }
        #endregion
    }
}
