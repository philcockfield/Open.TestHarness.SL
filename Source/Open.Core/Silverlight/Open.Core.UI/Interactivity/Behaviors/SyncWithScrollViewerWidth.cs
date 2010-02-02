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
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Syncs the element with it's parent's scroll-viewers ViewPortWidth.</summary>
    /// <remarks>This is useful because the width changes based on whether the scroll viewer has it's scrollbar displayed or not.</remarks>
    public class SyncWithScrollViewerWidth : Behavior<FrameworkElement>
    {
        #region Head
        public const string PropPadding = "Padding";

        private void Initialize()
        {
            // Retrieve a reference to the parent ScrollViewer.
            ScrollViewer = AssociatedObject.FindFirstVisualAncestor<ScrollViewer>();
            if (ScrollViewer == null) return;

            // Wire up events.
            ScrollViewer.SizeChanged += OnStackPanelSizeChanged; 

            // Finish up.
            SyncWidth();
        }
        #endregion

        #region Event Handlers
        private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= OnAssociatedObjectLoaded;
            Initialize();
        }

        private void OnStackPanelSizeChanged(object sender, SizeChangedEventArgs e)
        {
            SyncWidth();
        }
        #endregion

        #region Properties
        /// <summary>Gets the parent ScrollViewer that the associated-object is container within.</summary>
        public ScrollViewer ScrollViewer { get; private set; }

        /// <summary>Gets or sets the number of pixels to alter the width of the element by compared with the width of it's containing ScrollViewer's view-port.</summary>
        public double Padding
        {
            get { return (double) (GetValue(PaddingProperty)); }
            set { SetValue(PaddingProperty, value); }
        }
        /// <summary>Gets or sets the number of pixels to alter the width of the element by compared with the width of it's containing ScrollViewer's view-port.</summary>
        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register(
                PropPadding,
                typeof (double),
                typeof (SyncWithScrollViewerWidth),
                new PropertyMetadata(0d));
        #endregion

        #region Methods
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += OnAssociatedObjectLoaded;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            UnwireEvents();
            base.OnDetaching();
        }
        #endregion

        #region Internal
        private void UnwireEvents( )
        {
            if (AssociatedObject != null) AssociatedObject.Loaded -= OnAssociatedObjectLoaded;
            if (ScrollViewer != null) ScrollViewer.SizeChanged -= OnStackPanelSizeChanged; 
        }

        private void SyncWidth()
        {
            AssociatedObject.Width = ScrollViewer.ViewportWidth + Padding;
        }
        #endregion
    }
}
