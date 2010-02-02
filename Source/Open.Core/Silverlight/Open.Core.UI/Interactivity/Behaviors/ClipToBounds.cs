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
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Controls;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Sets a clipping region on the attached element to the size of that element.</summary>
    /// <remarks>
    ///    If this is applied to a Border element, the 'RadiusX' and 'RadiusY' values are automatically dervied from the
    ///    Border's 'CornerRadius' property unless explicit 'RadiusX' and 'RadiusY' values are set.<BR/>
    ///    To auto derive the corner radius omit setting the 'RadiusX' and 'RadiusY' properties.<BR/>
    ///    Only uniform CornerRadius values on the Border are supported.
    /// </remarks>
    public class ClipToBounds : Behavior<FrameworkElement>
    {
        #region Head
        private readonly RectangleGeometry clippingPath = new RectangleGeometry();
        #endregion

        #region Event Handlers
        void Handle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateClippingPath();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the x-radius of the ellipse that is used to round the corners of the rectangle.</summary>
        public double RadiusX { get; set; }

        /// <summary>Gets or sets the y-radius of the ellipse that is used to round the corners of the rectangle.</summary>
        public double RadiusY { get; set; }
        #endregion

        #region Methods
        protected override void OnAttached()
        {
            // Setup initial conditions.
            base.OnAttached();
            AssociatedObject.SizeChanged += Handle_SizeChanged;

            // Set corner radius automatically if the object is a 'Border' and explicit Radius X:Y values have not been set.
            if (RadiusX == 0 && RadiusX == 0) AutoSetCornerRadius(AssociatedObject as Border);

            // Add the clipping region.
            AssociatedObject.Clip = clippingPath;

            // Finish up.
            UpdateClippingPath();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SizeChanged -= Handle_SizeChanged;
        }

        /// <summary>Forces an update of the clipping path.</summary>
        public void UpdateClippingPath()
        {
            clippingPath.Rect = new Rect(0, 0, AssociatedObject.ActualWidth, AssociatedObject.ActualHeight);
            clippingPath.RadiusX = RadiusX;
            clippingPath.RadiusY = RadiusY;
        }
        #endregion

        #region Internal

        private void AutoSetCornerRadius(Border border)
        {
            if (border == null) return;
            RadiusX = border.CornerRadius.TopLeft;
            RadiusY = RadiusX;
        }
        #endregion
    }
}
