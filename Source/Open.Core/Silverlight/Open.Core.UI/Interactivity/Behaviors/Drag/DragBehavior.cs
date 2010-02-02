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
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Diagnostics;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Base class for behavior that involve dragging.</summary>
    /// <typeparam name="T">The type of element the behavior can attach to.</typeparam>
    public abstract class DragBehavior<T> : Behavior<T> where T:UIElement
    {
        #region Events
        /// <summary>Fires when the IsDragging property changes</summary>
        public event EventHandler IsDraggingChanged;
        private void OnIsDraggingChanged() { if (IsDraggingChanged != null) IsDraggingChanged(this, new EventArgs()); }

        /// <summary>Fires when during a drag operation (on MouseMove).</summary>
        public event EventHandler<DraggingEventArgs> Dragging;
        private DraggingEventArgs OnDragging(Point position)
        {
            var args = new DraggingEventArgs {Position = position};
            if (Dragging != null) Dragging(this, args);
            return args;
        }

        /// <summary>Fires when a drag operation starts.</summary>
        public event EventHandler DragStarted;
        private void FireDragStarted()
        {
            OnDragStarted();
            if (DragStarted != null) DragStarted(this, new EventArgs());
        }

        /// <summary>Fires when a drag operation stops.</summary>
        public event EventHandler DragStopped;
        private void FireDragStopped()
        {
            OnDragStopped();
            if (DragStopped != null) DragStopped(this, new EventArgs());
        }
        #endregion

        #region Head
        private UIElement uiElement;
        private bool isDragging;
        private bool isMouseDown;
        private bool isEnabled = true;
        private Point offset;
        private Point startPosition;

        protected DragBehavior()
        {
            // Set default values.
            Debounce = 3;
        }
        #endregion

        #region Event Handlers
        private void HandleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Setup initial conditions.
            if (ParentElement == null) return;
            if (! isEnabled) return;
            isMouseDown = true;

            // Ensure that the mouse can't leave the element.
            AssociatedObject.CaptureMouse();

            // Determine where the mouse 'grabbed' the element to use during the 'MouseMove' handler.
            offset = e.GetPosition(AssociatedObject);
            startPosition = new Point(Canvas.GetLeft(AssociatedObject), Canvas.GetTop(AssociatedObject));
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            // Setup initial conditions.
            if (! isMouseDown) return;

            // Calculate the current mouse position.
            var currentPosition = e.GetPosition(ParentElement);
            var newPosition = new Point(currentPosition.X - offset.X, currentPosition.Y - offset.Y);

            // Ensure de-bounce threshold has been reached.
            if (!IsDragging)
            {
                if (!HasDebounced(startPosition, currentPosition)) return;
                IsDragging = true;
            }

            // Update the position of the element.
            OnDrag(newPosition);

            // Alert listeners, and stop the drag operation if any of them ask.
            if (OnDragging(newPosition).Cancelled) StopDragging();
        }

        private void HandleMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StopDragging();
        }

        private void StopDragging()
        {
            if (! isMouseDown) return;
            AssociatedObject.ReleaseMouseCapture();
            IsDragging = false;
            isMouseDown = false;
            offset = default(Point);
            startPosition = default(Point);
        }
        #endregion

        #region Properties
        /// <summary>Gets the parent element that the AssociatedObject resides within.</summary>
        public UIElement ParentElement
        {
            get
            {
                if (AssociatedObject == null) return null;
                if (uiElement == null) uiElement = VisualTreeHelper.GetParent(AssociatedObject) as UIElement;
                return uiElement;
            }
        }

        /// <summary>Gets whether the element is currently in the middle of a drag operation.</summary>
        public bool IsDragging
        {
            get { return isDragging; }
            private set
            {
                if (value == IsDragging) return;
                isDragging = value;
                OnIsDraggingChanged();
                if (isDragging) FireDragStarted(); else FireDragStopped();
            }
        }

        /// <summary>Gets or sets the number of pixels to use as the de-bounce.</summary>
        /// <remarks>Debounce prevents accidental dragging of items.</remarks>
        public int Debounce { get; set; }

        /// <summary>Gets or sets whether the element can be dragged.</summary>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                if (! isEnabled && IsDragging) StopDragging();
            }
        }
        #endregion

        #region Methods
        protected override void OnAttached()
        {
            // Setup initial conditions.
            base.OnAttached();

            // Wire up events.
            AssociatedObject.MouseLeftButtonDown += HandleMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += HandleMouseLeftButtonUp;
            AssociatedObject.MouseMove += HandleMouseMove;
        }

        protected override void OnDetaching()
        {
            // Setup initial conditions.
            base.OnDetaching();
            if (AssociatedObject == null) return;

            // Unwire events.
            AssociatedObject.MouseLeftButtonDown -= HandleMouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp -= HandleMouseLeftButtonUp;
            AssociatedObject.MouseMove -= HandleMouseMove;
        }

        /// <summary>Invoked at each point in a drag operation.</summary>
        /// <param name="newPosition">The new position of the item within it's parent.</param>
        protected virtual void OnDrag(Point newPosition) { }

        /// <summary>Invoked when a drag operation starts.</summary>
        protected virtual void OnDragStarted() { }

        /// <summary>Invoked when a drag operation stops.</summary>
        protected virtual void OnDragStopped() { }
        #endregion

        #region Internal
        private bool HasDebounced(Point startingPosition, Point newPosition)
        {
            if (Math.Abs(newPosition.X - startingPosition.X) > Debounce) return true;
            if (Math.Abs(newPosition.Y - startingPosition.Y) > Debounce) return true;
            return false;
        }
        #endregion
    }
}
