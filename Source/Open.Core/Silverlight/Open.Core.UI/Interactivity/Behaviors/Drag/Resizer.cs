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

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Makes an element a resize handler, causing the Target to be resized when it is dragged.</summary>
    public class Resizer : DragBehavior<FrameworkElement>
    {
        #region Head
        private FrameworkElement target;
        private Size originalSize;
        private Point? startPosition;

        public Resizer()
        {
            // Set default values.
            ResizeWidth = true;
            ResizeHeight = true;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the name of the target element to resize.</summary>
        public string TargetName { get; set; }

        /// <summary>Gets the target element to resize.</summary>
        public FrameworkElement Target
        {
            get
            {
                if (target == null) target = AssociatedObject.FindByName(TargetName) as FrameworkElement;
                return target;
            }
        }

        /// <summary>Gets or sets whether the width of the target element is to be resized.</summary>
        public bool ResizeWidth { get; set; }

        /// <summary>Gets or sets whether the height of the target element is to be resized.</summary>
        public bool ResizeHeight { get; set; }
        #endregion

        #region Methods - Override
        protected override void OnDragStarted()
        {
            if (Target == null) return;
            originalSize = new Size(Target.ActualWidth, Target.ActualHeight);
        }

        protected override void OnDrag(Point newPosition)
        {
            // Setup initial conditions.
            if (Target == null) return;
            if (startPosition == null) startPosition = newPosition;

            // Update appropricate dimension.
            if (ResizeWidth) ChangeWidth(newPosition);
            if (ResizeHeight) ChangeHeight(newPosition);
        }

        protected override void OnDragStopped()
        {
            originalSize = default(Size);
            startPosition = null;
        }
        #endregion

        #region Internal
        private void ChangeWidth(Point newPosition)
        {
            // Calculate width.
            var xDifference = newPosition.X - startPosition.Value.X;
            var newWidth = (originalSize.Width + xDifference).WithinBounds(0, double.MaxValue);

            // Account for min-width.
            if (IsOverMax(newWidth, Target.MaxWidth)) newWidth = Target.MaxWidth;
            if (IsUnderMin(newWidth, Target.MinWidth)) newWidth = Target.MinWidth;

            // Update width on target.
            Target.Width = newWidth;
        }

        private void ChangeHeight(Point newPosition)
        {
            // Calculate height.
            var yDifference = newPosition.Y - startPosition.Value.Y;
            var newHeight = (originalSize.Height + yDifference).WithinBounds(0, double.MaxValue);

            // Account for min-height.
            if (IsOverMax(newHeight, Target.MaxHeight)) newHeight = Target.MaxHeight;
            if (IsUnderMin(newHeight, Target.MinHeight)) newHeight = Target.MinHeight;

            // Update height on target.
            Target.Height = newHeight;
        }

        private static bool IsOverMax(double value, double maxValue) { return value > maxValue; }
        private static bool IsUnderMin(double value, double minValue) { return value < minValue; }
        #endregion
    }
}
