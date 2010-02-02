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
using System.Windows.Media;
using System.Diagnostics;
using Open.Core.Common;
using Open.Core.Common.AttachedBehavior;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Behavior
{
    [ViewTestClass]
    public class Behavior__DraggableViewTest
    {
        #region Head
        private Placeholder dragElement;
        private Draggable behavior;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(Canvas control)
        {
            behavior = new Draggable();
            dragElement = new Placeholder();

            control.Width = 500;
            control.Height = 500;
            control.Background = StyleResources.Colors["Brush.Black.005"] as Brush;
            control.Children.Add(dragElement);

            Behaviors.SetDraggable(dragElement, behavior);

            behavior.IsDraggingChanged += delegate { Debug.WriteLine("!! IsDraggingChanged - IsDragging: " + behavior.IsDragging); };
            behavior.DragStarted += delegate { Debug.WriteLine("!! DragStarted"); };
            behavior.DragStopped += delegate { Debug.WriteLine("!! DragStopped"); };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Toggle_DragX(Canvas control)
        {
            behavior.DragX = !behavior.DragX;
            Debug.WriteLine("DragX: " + behavior.DragX);
        }

        [ViewTest]
        public void Toggle_DragY(Canvas control)
        {
            behavior.DragY = !behavior.DragY;
            Debug.WriteLine("DragY: " + behavior.DragY);
        }

        [ViewTest]
        public void WithinContainer_None(Canvas control)
        {
            behavior.DragContainment = DragContainment.None;
            Debug.WriteLine("DragContainment: " + behavior.DragContainment);
        }

        [ViewTest]
        public void WithinContainer_FullyWithin(Canvas control)
        {
            behavior.DragContainment = DragContainment.FullyWithin;
            Debug.WriteLine("DragContainment: " + behavior.DragContainment);
        }

        [ViewTest]
        public void WithinContainer_PixelsWithin(Canvas control)
        {
            behavior.DragContainment = DragContainment.PixelsWithin;
            Debug.WriteLine("DragContainment: " + behavior.DragContainment);
            Debug.WriteLine("PixelsWithinContainer: " + behavior.PixelsWithinContainer);
        }

        [ViewTest]
        public void Cancel_On_Drag_Event(Canvas control)
        {
            var i = 0;
            behavior.Dragging += (sender, e) =>
                                     {
                                         i++;
                                         if (i > 50)
                                         {
                                             Debug.WriteLine("Drag Operation Cancelled from 'Dragging' event."); 
                                             e.Cancel();
                                             i = 0;
                                         }
                                     };
        }

        [ViewTest]
        public void Toggle_IsEnabled(Canvas control)
        {
            behavior.IsEnabled = !behavior.IsEnabled;
            Debug.WriteLine("IsEnabled: " + behavior.IsEnabled);
        }

        [ViewTest]
        public void Detach(Canvas control)
        {
            behavior.Detach();
        }
        #endregion
   }
}
