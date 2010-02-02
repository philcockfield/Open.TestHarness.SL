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

using T = Open.Core.Common.AttachedBehavior.RoundedCorners;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Applies rounded corners to a border using a percentage structure.</summary>
    public class RoundedCorners : Behavior<Border>
    {
        #region Event Handlers
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateCornerRadius();
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the corner radius as a percentage (0..1).</summary>
        public double RadiusPercentage
        {
            get { return (double) (GetValue(RadiusPercentageProperty)); }
            set { SetValue(RadiusPercentageProperty, value); }
        }
        /// <summary>Gets or sets the corner radius as a percentage (0..1).</summary>
        public static readonly DependencyProperty RadiusPercentageProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.RadiusPercentage),
                typeof (double),
                typeof (T),
                new PropertyMetadata(1d, (s, e) => ((T) s).UpdateCornerRadius()));


        /// <summary>Gets or sets the orientation of the border (used to calcuate which side is used as the rounding percentage).</summary>
        public Orientation Orientation
        {
            get { return (Orientation) (GetValue(OrientationProperty)); }
            set { SetValue(OrientationProperty, value); }
        }
        /// <summary>Gets or sets the orientation of the border (used to calcuate which side is used as the rounding percentage).</summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.Orientation),
                typeof (Orientation),
                typeof (T),
                new PropertyMetadata(Orientation.Horizontal, (s, e) => ((T)s).UpdateCornerRadius()));
        #endregion

        #region Methods
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SizeChanged += OnSizeChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SizeChanged -= OnSizeChanged;
        }
        #endregion

        #region Internal
        private void UpdateCornerRadius()
        {
            // Setup initial conditions.
            if (AssociatedObject == null) return;

            // Get values.
            var percent = RadiusPercentage.WithinBounds(0, 1);
            var sideLength = Orientation == Orientation.Horizontal
                                                       ? AssociatedObject.ActualHeight
                                                       : AssociatedObject.ActualWidth;
            var radius = (sideLength*0.5)*percent;

            // Apply the new corner radius.
            AssociatedObject.CornerRadius = new CornerRadius(radius);
        }
        #endregion
    }
}
