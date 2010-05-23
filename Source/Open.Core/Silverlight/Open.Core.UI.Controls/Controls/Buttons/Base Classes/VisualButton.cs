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
using System.Windows.Media;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>Base class for buttons that are simple visuals.</summary>
    public abstract class VisualButton : Button
    {
        #region Events
        /// <summary>Fires when the VisualState has been updated.</summary>
        public event EventHandler VisualStateChanged;
        protected void OnVisualStateChanged() { if (VisualStateChanged != null) VisualStateChanged(this, new EventArgs()); }
        #endregion

        #region Head
        private bool isInitialized;

        /// <summary>Constructor.</summary>
        protected VisualButton()
        {
            // Set default values.
            Padding = new Thickness(0);
        }

        public override void OnApplyTemplate()
        {
            // Setup initial conditions.
            base.OnApplyTemplate();
            if (RootVisual == null) throw new Exception("The RootVisual property has not been set.  Make sure this is set in 'OnApplyTemplate' and then call this method from the base class.");

            // Wire up events.
            RootVisual.MouseLeftButtonDown += delegate { OnMouseDown(); };
            Click += delegate { OnMouseUp(); };
            MouseEnter += delegate { OnMouseEnter(); }; 
            MouseLeave += delegate { OnMouseLeave(); };
            IsEnabledChanged += delegate { UpdateVisualState(); };

            // Finish up.
            isInitialized = true;
            UpdateVisualState();
        }
        #endregion

        #region Event Handlers
        private void OnMouseDown()
        {
            IsMouseDown = true;
            UpdateVisualState();
        }

        private void OnMouseUp()
        {
            IsMouseDown = false;
            UpdateVisualState();
        }

        private void OnMouseEnter()
        {
            UpdateVisualState();
        }

        private void OnMouseLeave()
        {
            IsMouseDown = false;
            UpdateVisualState();
        }
        #endregion

        #region Properties - Protected
        /// <summary>Gets the root visual within the button.</summary>
        protected abstract UIElement RootVisual { get; }

        /// <summary>Gets whether the mouse is currently depressed.</summary>
        public bool IsMouseDown { get; private set; }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the pixel offset to adjust the button by when the mouse is over it.</summary>
        public Point OverOffset
        {
            get { return (Point) (GetValue(OverOffsetProperty)); }
            set { SetValue(OverOffsetProperty, value); }
        }
        /// <summary>Gets or sets the pixel offset to adjust the button by when the mouse is over it.</summary>
        public static readonly DependencyProperty OverOffsetProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<VisualButton>(m => m.OverOffset),
                typeof (Point),
                typeof (VisualButton),
                new PropertyMetadata(default(Point), (s, e) => ((VisualButton)s).UpdateOffset()));


        /// <summary>Gets or sets the pixel offset to adjust the button by when the mouse is depressed.</summary>
        public Point DownOffset
        {
            get { return (Point) (GetValue(DownOffsetProperty)); }
            set { SetValue(DownOffsetProperty, value); }
        }
        /// <summary>Gets or sets the pixel offset to adjust the button by when the mouse is depressed.</summary>
        public static readonly DependencyProperty DownOffsetProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<VisualButton>(m => m.DownOffset),
                typeof (Point),
                typeof (VisualButton),
                new PropertyMetadata(default(Point), (s, e) => ((VisualButton) s).UpdateOffset()));


        /// <summary>Gets or sets the opacity of the button when in a disabled state.</summary>
        public double DisabledOpacity
        {
            get { return (double) (GetValue(DisabledOpacityProperty)); }
            set { SetValue(DisabledOpacityProperty, value); }
        }
        /// <summary>Gets or sets the opacity of the button when in a disabled state.</summary>
        public static readonly DependencyProperty DisabledOpacityProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<VisualButton>(m => m.DisabledOpacity),
                typeof (double),
                typeof (VisualButton),
                new PropertyMetadata(0.3d, (s, e) => ((VisualButton)s).UpdateOpacity()));
        #endregion

        #region Dependency Properties - Background
        /// <summary>Gets or sets the template used to render the button background.</summary>
        public DataTemplate BackgroundTemplate
        {
            get { return (DataTemplate)(GetValue(BackgroundTemplateProperty)); }
            set { SetValue(BackgroundTemplateProperty, value); }
        }
        /// <summary>Gets or sets the template used to render the button background.</summary>
        public static readonly DependencyProperty BackgroundTemplateProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<VisualButton>(m => m.BackgroundTemplate),
                typeof(DataTemplate),
                typeof(VisualButton),
                new PropertyMetadata(ButtonTemplates.SimpleButtonBackground));
        #endregion

        #region Internal
        private void UpdateVisualState()
        {
            UpdateOffset();
            UpdateOpacity();
            OnVisualStateChanged();
        }

        private void UpdateOpacity( )
        {
            if (!isInitialized) return;
            RootVisual.Opacity = IsEnabled ? 1 : DisabledOpacity.WithinBounds(0, 1);
        }

        private void UpdateOffset()
        {
            // Setup initial conditions.
            if (!isInitialized) return;

            // Determine offset.
            var offset = default(Point);
            if (IsMouseOver) offset = OverOffset;
            if (IsMouseDown) offset = DownOffset;

            // Remove the offset Translation if there is no offsetting to be done.
            if (offset == default(Point))
            {
                if (RootVisual.RenderTransform != null) RootVisual.RenderTransform = null;
                return;
            }

            // Apply the offset.
            RootVisual.RenderTransform = new TranslateTransform { X = offset.X, Y = offset.Y };
        }
        #endregion
    }
}
