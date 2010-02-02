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
using System.Windows.Input;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Flags indicating how a dragged element is to be contained within its parent.</summary>
    /// <remarks>See the 'Draggable' behavior.</remarks>
    public enum DragContainment
    {
        /// <summary>No containment.  The element can be dragged outside the bounds of its parent.</summary>
        None,
    
        /// <summary>The element is fully contained within it's parent.</summary>
        FullyWithin,

        /// <summary>The element is contained within the pixel threshold set on the 'PixelsWithinContainer' property.</summary>
        PixelsWithin
    }

    /// <summary>Makes an element draggable (within a canvas container).</summary>
    public class Draggable : DragBehavior<UIElement>
    {
        #region Head
        public Draggable()
        {
            // Set default values.
            DragX = true;
            DragY = true;
            DragContainment = DragContainment.PixelsWithin;
            PixelsWithinContainer = new Point(10, 10);
        }
        #endregion

        #region Properties
        /// <summary>Gets the associated object.</summary>
        public new UIElement AssociatedObject { get { return base.AssociatedObject; } }

        /// <summary>Gets or sets whether the element can be dragged horizontally.</summary>
        public bool DragX { get; set; }

        /// <summary>Gets or sets whether the element can be dragged vertically.</summary>
        public bool DragY { get; set; }

        /// <summary>Gets the canvas that the element resides within.</summary>
        public Canvas Canvas { get { return ParentElement as Canvas; } }

        /// <summary>Gets or sets how the element is contained within it's parent.</summary>
        public DragContainment DragContainment { get; set; }

        /// <summary>Gets or sets the pixel threshold for containment.</summary>
        /// <remarks>This property is only relevant when the 'DragContainment' property is set to 'PixelsWithin'.</remarks>
        public Point PixelsWithinContainer { get; set; }
        #endregion

        #region Methods
        /// <summary>Invoked at each point in a drag operation.</summary>
        protected override void OnDrag(Point newPosition)
        {
            // Ensure the element (if moved) would still be within the container.
            var position = AdjustForContainment(newPosition);

            // Update the position.
            if (DragX) Canvas.SetLeft(AssociatedObject, position.X);
            if (DragY) Canvas.SetTop(AssociatedObject, position.Y);
        }
        #endregion

        #region Internal
        private Point AdjustForContainment(Point position)
        {
            var elementSize = AssociatedObject.RenderSize;
            switch (DragContainment)
            {
                case DragContainment.None: break; // No change.

                case DragContainment.FullyWithin:
                    position = AdjustForContainment(
                        position,
                        0, 0,
                        Canvas.ActualWidth - elementSize.Width,
                        Canvas.ActualHeight - elementSize.Height);
                    break;

                case DragContainment.PixelsWithin:
                    position = AdjustForContainment(
                        position,
                        0 - (elementSize.Width - PixelsWithinContainer.X),
                        0 - (elementSize.Height - PixelsWithinContainer.Y),
                        Canvas.ActualWidth - PixelsWithinContainer.X,
                        Canvas.ActualHeight - PixelsWithinContainer.Y);
                    break;

                default: throw new NotSupportedException(DragContainment.ToString());
            }
            return position;
        }

        private static Point AdjustForContainment(Point position, double minLeft, double minTop, double maxLeft, double maxTop)
        {
            if (position.X < minLeft) position.X = minLeft;
            if (position.Y < minTop) position.Y = minTop;

            if (position.X > maxLeft) position.X = maxLeft;
            if (position.Y > maxTop) position.Y = maxTop;

            return position;
        }        
        #endregion
    }
}
