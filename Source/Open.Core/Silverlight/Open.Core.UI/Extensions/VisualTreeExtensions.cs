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

namespace Open.Core.Common
{
    public static partial class VisualTreeExtensions
    {
        #region Methods - Find/Retrieve Elements from Tree
        /// <summary>Gets the elements parent in the VisualTree.</summary>
        /// <param name="element">The source element.</param>
        /// <returns>The parent element, or null if the element does not exist.</returns>
        public static DependencyObject GetParentVisual(this DependencyObject element)
        {
            return VisualTreeHelper.GetParent(element);
        }

        /// <summary>Removes the specified element from the visual tree (if it's currently contained within the tree).</summary>
        /// <param name="element">The element to remove.</param>
        public static void RemoveFromVisualTree(this UIElement element)
        {
            // Setup initial conditions.
            if (element == null) return;
            var parent = element.GetParentVisual();
            if (parent == null) return;

            // Common remove types.
            if (parent.GetType().IsA(typeof(Panel)))
            {
                ((Panel)parent).Children.Remove(element);
                return;
            }
            if (parent.GetType().IsA(typeof(ContentPresenter)))
            {
                ((ContentPresenter)parent).Content = null;
                return;
            }

            // Platform specific remove types (differs between WPF and Silverlight).
            if (RemoveChild(parent, element)) return;

            // Failed to remove.  Container not supported.
            throw new NotSupportedException(
                            string.Format("Cannot remove '{0}' from a parent of type '{1}'.",
                                    element.GetType().Name,
                                    parent.GetType().Name));
        }


        /// <summary>Retreives the first ancestor from the VisualTree that is the same type as the calling element.</summary>
        /// <typeparam name="TParent">The type of the parent to look for.</typeparam>
        /// <param name="self">The object making the call.</param>
        /// <returns>The first visual ancestor, or Null if no matching ancestor was found.</returns>
        public static TParent FindFirstVisualAncestor<TParent>(this DependencyObject self) where TParent : DependencyObject
        {
            return FindFirstVisualAncestor(self, typeof(TParent)) as TParent;
        }

        /// <summary>Retreives the first ancestor from the VisualTree that is of the given type (or derived from it).</summary>
        /// <param name="self">The object making the call.</param>
        /// <param name="type">The type of the object to retrieve.</param>
        /// <returns>The first visual ancestor, or Null if no matching ancestor was found.</returns>
        public static DependencyObject FindFirstVisualAncestor(this DependencyObject self, Type type)
        {
            var parent = VisualTreeHelper.GetParent(self);
            while (parent != null)
            {
                if (parent.GetType().IsA(type))
                    return parent;
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }

        /// <summary>Walks down the visual tree looking for the first child element that matches the given type.</summary>
        /// <param name="element">The source element.</param>
        /// <typeparam name="T">The type of the child to retrieve.</typeparam>
        /// <returns>The specified type of element, otherwise Null.</returns>
        public static T FindFirstChildOfType<T>(this DependencyObject element) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(element);
            var type = typeof(T);
            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);
                if (type.IsAssignableFrom(child.GetType())) return child as T;
            }

            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);
                child = FindFirstChildOfType<T>(child);
                if (child != null) return child as T;
            }

            return null;
        }

        /// <summary>Walks up the tree looking for the first occurance of the named element.</summary>
        /// <param name="self">The starting element.</param>
        /// <param name="name">The name of the element.</param>
        /// <returns>The specified type of element, otherwise Null.</returns>
        public static DependencyObject FindByName(this DependencyObject self, string name)
        {
            // Setup initial conditions.
            if (self == null) return null;
            name = name.AsNullWhenEmpty();
            if (name == null) return null;
            var item = self;

            // Walk up the tree searching for the named element.
            do
            {
                var element = item as FrameworkElement;
                if (element != null)
                {
                    var namedElement = element.FindName(name) as DependencyObject;
                    if (namedElement != null) return namedElement;
                }

                // Step up to the parent.
                item = item.GetParentVisual();
            } while (item != null);

            // Finish up - not found.
            return null;
        }
        #endregion
    }
}
