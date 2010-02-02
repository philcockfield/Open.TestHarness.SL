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
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Open.Core.Common
{
    /// <summary>Monitors a ScrollViewer control reporting on changes to the top-most element as it scrolls.</summary>
    /// <remarks>The montior assumes that the ScrollPanel contains an StackPanel.</remarks>
    /// <typeparam name="TElement">
    ///    The type of XAML element rendered within the scroller's StackPanel 
    ///    (ie. The type within an ItemControl's ItemTemplate).
    /// </typeparam>
    public class ScrollViewerMonitor<TElement> : ModelBase where TElement : FrameworkElement
    {
        #region Events
        /// <summary>Fires when the 'TopElement' changes.</summary>
        public event EventHandler TopElementChanged;
        private void OnTopElementChanged()
        {
            if (!IsActive) return;
            if (TopElementChanged != null) TopElementChanged(this, new EventArgs());
        }
        #endregion

        #region Head
        private readonly List<StackPanelChild> stackPanelChildren = new List<StackPanelChild>();
        private StackPanel stackPanel;
        private Point? previousOffset;
        private TElement topElement;
        private bool isInitialized;
        private int stackPanelOffsetFailCount;

        /// <summary>Constructor.</summary>
        /// <param name="scrollViewer">The scroll-viewer to retrieve.</param>
        public ScrollViewerMonitor(ScrollViewer scrollViewer)
        {
            // Setup initial conditions.
            if (scrollViewer == null) throw new ArgumentNullException("scrollViewer");

            // Store values.
            ScrollViewer = scrollViewer;

            // Attempt to retrieve the child elements.
            // NB: This will only work if the ScrollViewer is already loaded into the visual tree.
            var childrenRetrieved = RetrieveChildElements(scrollViewer, false);

            // If the child element were not successfully retrieved get them after load.
            if (!childrenRetrieved) ScrollViewer.Loaded += OnLoaded;
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            ScrollViewer.Loaded -= OnLoaded;
            WireUpEvents(false);
        }
        #endregion

        #region Event Handlers
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Setup initial conditions.
            ScrollViewer.Loaded -= OnLoaded;

            // Attempt for the second time to get the child elements not the scroll-viewer is loaded.
            RetrieveChildElements(ScrollViewer, true);
        }

        private void OnStackPanelSizeChanged(object sender, SizeChangedEventArgs e)
        {
            BuildElementDimensionsList();
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            if (!IsActive) return;
            Refresh();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the monitor is active (when inactive no events are fired).</summary>
        public bool IsActive
        {
            get { return GetPropertyValue<ScrollViewerMonitor<TElement>, bool>(m => m.IsActive, true); }
            set
            {
                if (value == IsActive) return;
                SetPropertyValue<ScrollViewerMonitor<TElement>, bool>(m => m.IsActive, value, true);
                if (value) Refresh();
            }
        }

        /// <summary>Gets the scroll-viewer that is being monitored.</summary>
        public ScrollViewer ScrollViewer { get; private set; }

        /// <summary>Gets the pixel offset of the child-StackPanel within the ScrollViewer.</summary>
        public Point StackPanelOffset
        {
            get
            {
                if (!isInitialized) return default(Point);
                try
                {
                    return stackPanel == null
                        ? default(Point)
                        : stackPanel.TransformToVisual(ScrollViewer).Transform(new Point(0, 0));
                }
                catch (ArgumentException)
                {
                    stackPanelOffsetFailCount++;
                    if (stackPanelOffsetFailCount > 10) Dispose();
                    return default(Point);
                }
            }
        }

        /// <summary>Gets the top most element.</summary>
        public TElement TopElement
        {
            get { return topElement; }
            private set
            {
                if (value == topElement) return;
                topElement = value;
                OnPropertyChanged<ScrollViewerMonitor<TElement>>(m => m.TopElement, m => m.TopElementViewModel);
                OnTopElementChanged();
            }
        }

        /// <summary>
        ///    Gets the DataContext of the top most element, or null if there is no current TopElement, 
        ///    or the TopElement does not have a DataContext.
        /// </summary>
        public object TopElementViewModel{get { return TopElement == null ? null : TopElement.DataContext; }}
        #endregion

        #region Methods
        /// <summary>Performs the calculation again to refresh the TopElement values (and fire corresponding events).</summary>
        public void Refresh()
        {
            // Setup initial conditions.
            if (stackPanel == null) return;
            if (ScrollViewer.Visibility != Visibility.Visible) return;

            // Get the offset position of the stack-panel.
            var stackOffset = StackPanelOffset;
            if (previousOffset != null && Equals(stackOffset, previousOffset))
            {
                // Don't continue because the scroll position has not changed.
                previousOffset = stackOffset;
                return;
            }

            // Retrieve the top most item.
            TopElement = GetTopElement(stackOffset);

            // Finish up.
            previousOffset = stackOffset;
            isInitialized = true;
        }
        #endregion

        #region Internal
        private bool RetrieveChildElements(DependencyObject scrollViewer, bool fromLoadedHandler)
        {
            try
            {
                // NB: The boolean fail return value is used to handle scenarios where this monitor is 
                //       constructed before the scroll-viewer has been built within the visual tree.

                // Get the stack panel.
                stackPanel = scrollViewer.FindFirstChildOfType<StackPanel>();
                if (fromLoadedHandler && stackPanel == null) throw new NotFoundException("The ScrollViewer must contain an StackPanel.");
                if (stackPanel == null) return false;
            }
            catch (Exception)
            {
                if (fromLoadedHandler) throw;
                return false;
            }

            // Wire up events.
            WireUpEvents(false);
            WireUpEvents(true);

            // Finish up.
            BuildElementDimensionsList();
            return true;
        }

        private void WireUpEvents(bool addHandler)
        {
            if (addHandler)
            {
                if (stackPanel != null) stackPanel.SizeChanged += OnStackPanelSizeChanged;
                ScrollViewer.LayoutUpdated += OnLayoutUpdated;
            }
            else
            {
                if (stackPanel != null) stackPanel.SizeChanged -= OnStackPanelSizeChanged;
                ScrollViewer.LayoutUpdated -= OnLayoutUpdated;
            }
        }

        private void BuildElementDimensionsList()
        {
            // Setup initial conditions.
            if (stackPanel == null) return;
            stackPanelChildren.Clear();

            // Enumerate the children getting the dimensions of each one.
            foreach (var child in stackPanel.Children)
            {
                // Retrieve the element (inside the StackPanel's ContentPresenter).
                var element = child.FindFirstChildOfType<TElement>();
                if (element == null) continue;

                // Create the item to store and get the dimensions.
                var item = new StackPanelChild
                               {
                                   Element = element,
                                   Position = element.TransformToVisual(stackPanel).Transform(new Point(0, 0))
                               };

                // Store the item.
                stackPanelChildren.Add(item);
            }
        }

        private TElement GetTopElement(Point stackOffset)
        {
            // Setup initial conditions.
            if (stackPanel == null) return default(TElement);

            // Retrieve the top most item.
            var topMost = (from n in stackPanelChildren
                           where n.Bottom + stackOffset.Y > 0
                           select n).FirstOrDefault();

            // Finish up.
            return topMost == null 
                       ? default(TElement)
                       : topMost.Element;
        }
        #endregion

        private class StackPanelChild
        {
            #region Properties
            public Point Position { get; set; }
            public TElement Element { get; set; }
            public double Bottom { get { return Position.Y + Element.ActualHeight; } }
            #endregion
        }
    }
}
