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
using System.Windows.Media;

namespace Open.Core.Common
{
    // Silverlight specific.
    public static partial class FrameworkElementExtensions
    {
        #region Methods - General
        /// <summary>Gets the absolute pixel position of the element relative to the Application's RootVisual.</summary>
        /// <param name="self">The child element to retrieve the positon of.</param>
        /// <returns>The position of the element, or null if the element is not within the visual tree.</returns>
        public static Point? GetAbsolutePosition(this UIElement self)
        {
            // Setup initial conditions.
            if (self == null) return null;

            // Attempt to retrieve the position.
            Point position;
            try
            {
                position = Application.Current.RootVisual.TransformToVisual(self).Transform(new Point(0, 0));
            }
            catch (ArgumentException) // The element is not within the visual tree.
            {
                return null;
            }
            catch (Exception) { throw; }

            // Invert values.
            position = new Point(Math.Abs(position.X), Math.Abs(position.Y));

            // Finish up.
            return position;
        }
        #endregion

        #region Methods - Focused Element
        /// <summary>Determines whether the object is the child of the given type of focused element.</summary>
        /// <typeparam name="TParent">The type of the parent to look for.</typeparam>
        /// <param name="element">The object making the call.</param>
        /// <returns>True if the element is the child of a focused parent, otherwise False.</returns>
        public static bool IsFocusedChildOf<TParent>(this DependencyObject element) where TParent : DependencyObject
        {
            return GetIsFocusedChildOf<TParent>(element, FocusManager.GetFocusedElement() as DependencyObject);
        }

        /// <summary>Gets whether the element contains the focused element.</summary>
        /// <param name="self">The element to look within.</param>
        /// <returns>True if the element, or any of it's children, is currently focused.</returns>
        public static bool ContainsFocus(this DependencyObject self)
        {
            // Setup initial conditions.
            var focusedElement = FocusManager.GetFocusedElement();
            if (self == focusedElement) return true;

            // Enumerate children looking for focus.
            var count = VisualTreeHelper.GetChildrenCount(self);
            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(self, i);
                if (child == focusedElement) return true;

                // Make an exception for 'ComboBox' - if it's dropdown is open, then it can be considered focused.
                if (child is ComboBox && ((ComboBox)child).IsDropDownOpen) return true;
            }

            // Not found in direct child, recurseively walk each child.
            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(self, i);
                if (ContainsFocus(child)) return true;
            }

            // Finish up.
            return false;
        }

        /// <summary>Searches the decendents looking for the first element that is adorned with the 'Focus.IsDefault' attached property.</summary>
        /// <param name="self">The parent to look within.</param>
        /// <returns>The first element found, otherwise null.</returns>
        public static Control GetDefaultFocusChild(this DependencyObject self)
        {
            // Setup initial conditions.
            if (self == null) return null;

            // Walk direct children.
            var count = VisualTreeHelper.GetChildrenCount(self);
            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(self, i) as Control;
                if (child != null && Common.Focus.GetIsDefault(child)) return child;
            }

            // Recursion.
            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(self, i);
                child = child.GetDefaultFocusChild();
                if (child != null) return child as Control;
            }

            // Finish up.
            return null;
        }

        /// <summary>Assigns focus to the first child element that is adorned with the 'Focus.IsDefault' attached property.</summary>
        /// <param name="self">The parent to look within.</param>
        /// <returns>True if focus was asigned to a child control, otherwise False.</returns>
        public static bool FocusDefaultChild(this DependencyObject self)
        {
            var defaultChild = self.GetDefaultFocusChild();
            return defaultChild == null ? false : defaultChild.Focus();
        }
        #endregion
    }
}
