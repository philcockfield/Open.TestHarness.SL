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
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace Open.Core.Common
{
    /// <summary>Defines a method that is invoked to arrange an element within a panel.</summary>
    /// <param name="element">The element to arrange.</param>
    /// <param name="bounds">The dimensions and positon of the element to place.</param>
    /// <remarks>
    ///    If called from within a Panel's "ArrangeOverride" method, use the 'element.Arrange(...)' method.
    ///    However this delegate also gives you the freedom to do custom layup within other Panel's, for instance
    ///    when using a Canvas you can use the 'Canvas.SetTop' and 'Canvas.SetLeft' methods.
    /// </remarks>
    public delegate void ArrangeChild(UIElement element, Rect bounds);


    /// <summary>Represents the bounds of an visual element.</summary>
    public struct ElementBounds
    {
        public ElementBounds(UIElement element, Rect bounds) : this()
        {
            Element = element;
            Bounds = bounds;
        }

        /// <summary>Gets the Element the bounds relate to.</summary>
        public UIElement Element { get; private set; }

        /// <summary>Gets the bounds of the element.</summary>
        public Rect Bounds { get; private set; }
    }


    /// <summary>Arranges child elements within a panel into a stack.</summary>
    public class StackArranger
    {
        #region Head
        public StackArranger(IEnumerable<UIElement> children, Orientation orientation) : this(children, orientation, null, null){}
        public StackArranger(IEnumerable<UIElement> children, Orientation orientation, Func<UIElement, bool> exclude, ArrangeChild arrangeChild)
        {
            // Setup initial conditions.
            Children = children;
            Orientation = orientation;
            Exclude = exclude;
            ArrangeChild = arrangeChild;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the elments that are being arranged.</summary>
        /// <remarks>This will likely be the 'Children' property of a Panel.</remarks>
        public IEnumerable<UIElement> Children { get; set; }

        /// <summary>Gets or sets the orientation of the panel.</summary>
        public Orientation Orientation { get; set; }

        /// <summary>Gets or sets a delegate that is called to determine if the given child should be excluded.</summary>
        /// <remarks>
        ///     You may want to exlude certin child elements if they under another functions control, 
        ///    for instance temporarily while it is being dragged.
        /// </remarks>
        public Func<UIElement, bool> Exclude { get; set; }

        /// <summary>Gets or sets the arrange child method to use when positioning each item in the Panel's Children collection.</summary>
        public ArrangeChild ArrangeChild { get; set; }
        #endregion

        #region Properties - Private
        private ArrangeChild ArrangeChildMethod
        {
            get { return ArrangeChild ?? delegate(UIElement element, Rect bounds) { element.Arrange(bounds); }; }
        }
        #endregion

        #region Methods
        /// <summary>Provides the behavior for the "measure" pass of the layout.</summary>
        /// <param name="availableSize">
        ///    The available size that this object can give to child objects. Infinity can be specified as a 
        ///    value to indicate that the object will size to whatever content is available.
        /// </param>
        /// <returns>The available size.</returns>
        public Size Measure(Size availableSize)
        {
            foreach (var child in Children)
            {
                child.Measure(availableSize);
            }
            return availableSize;
        }

        /// <summary>Provides the behavior for the "arrange" pass of the layout.</summary>
        /// <param name="finalSize">The final area within the parent that this object should use to arrange itself and its children.</param>
        /// <returns>The final area within the parent.</returns>
        /// <remarks>Call the 'Measure' method before invoking 'Arrange'.</remarks>
        public Size Arrange(Size finalSize)
        {
            List<ElementBounds> boundsList;
            finalSize = ArrangeCalculate(finalSize, out boundsList);
            Arrange(boundsList);
            return finalSize;
        }

        /// <summary>Arranges the specified set of child/bounds item.</summary>
        /// <param name="childBounds">The collection of child, with bounds data, to arrange.</param>
        public void Arrange(List<ElementBounds> childBounds)
        {
            foreach (var item in childBounds) Arrange(item);
        }

        /// <summary>Arranges the specified child/bounds item.</summary>
        /// <param name="childBounds">The child, with bounds data, to arrange.</param>
        public void Arrange(ElementBounds childBounds)
        {
            ArrangeChildMethod(childBounds.Element, childBounds.Bounds);
        }

        /// <summary>Calculates the position of each item within the Panel's Children collection.</summary>
        /// <param name="finalSize">The final area within the parent that this object should use to arrange itself and its children.</param>
        /// <param name="returnBounds">Returns a list of element bounds corresponding, in order, to each child within the Panel.</param>
        /// <returns>The final area within the parent.</returns>
        /// <remarks>This method is used by the 'Arrange' method.</remarks>
        /// <remarks>Call the 'Measure' method before invoking 'ArrangeCalculate'.</remarks>
        public Size ArrangeCalculate(Size finalSize, out List<ElementBounds> returnBounds)
        {
            // Retrieve the positioning information.
            finalSize = Orientation == Orientation.Vertical 
                            ? ArrangeVerticalCalculate(finalSize, out returnBounds) 
                            : ArrangeHorizontalCalculate(finalSize, out returnBounds);

            // Finish up.
            return finalSize;
        }
        #endregion

        #region Methods - Static
        /// <summary>Arranges an element within a canvas.</summary>
        /// <param name="element">The element to arrange.</param>
        /// <param name="bounds">
        ///    The dimensions and positon of the element to place.
        ///    It is assumed that the margin values of the child element is included within the bounds values.
        /// </param>
        public static void ArrangeChildOnCanvas(UIElement element, Rect bounds)
        {
            // Setup initial conditions.
            var margin = GetMargin(element);

            // Set X:Y position on the Canvas.
            Canvas.SetLeft(element, bounds.Left + margin.Left);
            Canvas.SetTop(element, bounds.Top + margin.Top);

            // Set the size (if the element has Width/Height properties).
            UpdateSize(element, bounds, margin);
        }

        /// <summary>Updates the size of the element based on the bounds accounting for margins.</summary>
        /// <param name="element">The element to size.</param>
        /// <param name="bounds">
        ///    The dimensions and positon of the element.
        ///    It is assumed that the margin values of the child element is included within the bounds values.
        /// </param>
        public static void UpdateSize(UIElement element, Rect bounds)
        {
            UpdateSize(element, bounds, GetMargin(element));
        }
        private static void UpdateSize(UIElement element, Rect bounds, Thickness margin)
        {
            // Set the size;
            var frameworkElement = element as FrameworkElement;
            if (frameworkElement != null)
            {
                frameworkElement.Width = bounds.Width - (margin.Left + margin.Right);
                frameworkElement.Height = bounds.Height - (margin.Top + margin.Bottom);
            }
        }
        #endregion

        #region Internal - Arrange Children
        private Size ArrangeVerticalCalculate(Size finalSize, out List<ElementBounds> returnBounds)
        {
            // Setup initial conditions.
            var topOffset = 0d;
            var widestChild = 0d;
            returnBounds = new List<ElementBounds>();

            // Arrange children.
            foreach (var child in Children)
            {
                // Calculate values.
                var margin = GetMargin(child);
                var size = GetSize(child, finalSize, margin);

                // Place the child (if it has not been excluded).
                if (!IsExcluded(child))
                {
                    var bounds = new Rect(0, topOffset, size.Width, size.Height);
                    returnBounds.Add(new ElementBounds(child, bounds));
                }

                // Increment values.
                topOffset += size.Height;
                if (size.Width > widestChild) widestChild = size.Width;
            }

            // Finish up.
            return new Size(widestChild, topOffset); 
        }

        private Size ArrangeHorizontalCalculate(Size finalSize, out List<ElementBounds> returnBounds)
        {
            // Setup initial conditions.
            var leftOffset = 0d;
            var tallestChild = 0d;
            returnBounds = new List<ElementBounds>();

            // Arrange children.
            foreach (var child in Children)
            {
                // Calculate values.
                var margin = GetMargin(child);
                var size = GetSize(child, finalSize, margin);

                // Place the child (if it has not been excluded).
                if (!IsExcluded(child))
                {
                    var bounds = new Rect(leftOffset, 0, size.Width, size.Height);
                    returnBounds.Add(new ElementBounds(child, bounds));
                }

                // Increment values.
                leftOffset += size.Width;
                if (size.Height > tallestChild) tallestChild = size.Height;
            }

            // Finish up.
            return new Size(leftOffset, tallestChild); 
        }
        #endregion

        #region Internal
        private static Thickness GetMargin(UIElement element)
        {
            var frameworkElement = element as FrameworkElement;
            return frameworkElement == null ? default(Thickness) : frameworkElement.Margin;
        }

        private bool IsExcluded(UIElement element)
        {
            return Exclude == null ? false : Exclude(element);
        }

        private Size GetSize(UIElement child, Size finalSize, Thickness margin)
        {
            var element = child as FrameworkElement;
            if (element == null) return finalSize;

            var width = 0.0;
            var height = 0.0;

            if (Orientation == Orientation.Vertical)
            {
                width = Equals(element.Width, double.NaN) ? finalSize.Width : element.Width + (margin.Left + margin.Right);
            }
            else width = element.DesiredSize.Width;

            if (Orientation == Orientation.Horizontal)
            {
                height = Equals(element.Height, double.NaN) ? finalSize.Height : element.Height + (margin.Top + margin.Bottom);
            }
            else height = element.DesiredSize.Height;

            return new Size(width, height);
        }
        #endregion
    }
}
