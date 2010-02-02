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
using System.Windows.Controls.Primitives;

namespace Open.Core.Common
{
    public static partial class FrameworkElementExtensions
    {
        #region General
        /// <summary>Retrieves the DataContext of the element if it is a FrameworkElement.</summary>
        /// <param name="self">The source element.</param>
        /// <returns>The element's datacontext if it is a FrameworkElement, otherwise null.</returns>
        public static object GetDataContext(this UIElement self)
        {
            var frameworkElement = self as FrameworkElement;
            return frameworkElement == null ? null : frameworkElement.DataContext;
        }

        /// <summary>Loads the content of a DataTemplate and places it within the given parent.</summary>
        /// <param name="self">The DataTemplate to load.</param>
        /// <param name="parent">The parent to place the content within.</param>
        /// <param name="dataContext">The DataContext to apply to the element.</param>
        /// <returns></returns>
        public static bool LoadContent(this DataTemplate self, Panel parent, object dataContext)
        {
            // Setup initial conditions.
            if (self == null) throw new ArgumentNullException("self");
            if (parent == null) throw new ArgumentNullException("parent");
            var obj = self.LoadContent();
            if (obj == null) return false;

            // Assign the data-context.
            var frameworkElement = obj as FrameworkElement;
            if (frameworkElement != null) frameworkElement.DataContext = dataContext;

            // Add the element.
            if (!(obj is UIElement)) return false;
            parent.Children.Add(obj as UIElement);

            // Finish up.
            return true;
        }
        #endregion

        #region Size
        /// <summary>Sets the size of the element.</summary>
        /// <param name="self">The element to set the dimensions on.</param>
        /// <param name="width">The width of the element.</param>
        /// <param name="height">The height of the element.</param>
        public static void SetSize(this FrameworkElement self, double width, double height)
        {
            if (self == null) return;
            self.Width = width;
            self.Height = height;
        }

        /// <summary>Gets the actual size of the element, forcing a re-measure of the element if the size is reported as 0:0.</summary>
        /// <param name="self">The element to examine.</param>
        /// <returns>The actual size of the element.</returns>
        public static Size GetActualSize(this FrameworkElement self)
        {
            return GetActualSize(self, false);
        }

        /// <summary>Gets the actual size of the element, forcing a re-measure of the element if the size is reported as 0:0.</summary>
        /// <param name="self">The element to examine.</param>
        /// <param name="forceMeasure">Flag indicating if a re-measure of the element should be forced.</param>
        /// <returns>The actual size of the element.</returns>
        public static Size GetActualSize(this FrameworkElement self, bool forceMeasure)
        {
            var size = new Size(self.ActualWidth, self.ActualHeight);
            if (forceMeasure || (size.Width == 0 && size.Height == 0))
            {
                // If the size is zero, force a measure.
                self.Measure(new Size(9999999, 9999999));
                size = self.DesiredSize;
            }
            return size;
        }        
        #endregion

        #region Position
        /// <summary>Gets the X:Y position of the element within it's canvas.</summary>
        /// <param name="self">The containing canvas.</param>
        /// <param name="element">The element to position.</param>
        public static Point GetChildPosition(this Canvas self, UIElement element)
        {
            return new Point(Canvas.GetLeft(element), Canvas.GetTop(element));
        }

        /// <summary>Sets the X:Y position of the element within it's canvas.</summary>
        /// <param name="self">The containing canvas.</param>
        /// <param name="element">The element to position.</param>
        /// <param name="x">The X pixel position.</param>
        /// <param name="y">The Y pixel position.</param>
        public static void SetPosition(this Canvas self, UIElement element, double x, double y)
        {
            Canvas.SetLeft(element, x);
            Canvas.SetTop(element, y);
        }

        /// <summary>Sets the X:Y position of the element within it's canvas.</summary>
        /// <param name="self">The containing canvas.</param>
        /// <param name="element">The element to position.</param>
        /// <param name="position">The X:Y pixel positions.</param>
        public static void SetPosition(this Canvas self, UIElement element, Point position)
        {
            SetPosition(self, element, position.X, position.Y);
        }


        /// <summary>Gets the position of the given child element relative to it's parent ItemsControl.</summary>
        /// <param name="self">The parent ItemsControl.</param>
        /// <param name="childElement">The child element to retrieve the position of.</param>
        public static Point GetChildPosition(this ItemsControl self, UIElement childElement)
        {
            // Setup initial conditions.
            if (self == null) return default(Point);

            // Retrieve the child StackPanel.
            var stackPanel = self.FindFirstChildOfType<StackPanel>();
            if (stackPanel == null) return default(Point);

            // Get the child-element's position, and add the stack panel offset.
            return stackPanel.GetChildPosition(childElement);
        }

        /// <summary>Gets the position of the given child element relative to it's parent StackPanel.</summary>
        /// <param name="self">The parent StackPanel.</param>
        /// <param name="childElement">The child element to retrieve the position of.</param>
        public static Point GetChildPosition(this StackPanel self, UIElement childElement)
        {
            return self == null
                       ? default(Point)
                       : childElement.TransformToVisual(self).Transform(new Point(0, 0));
        }
        #endregion

        #region ItemsControl
        /// <summary>Retrieves a visual element that has been created by databinding in an ItemsControl.</summary>
        /// <param name="self">The ItemsControl to look in.</param>
        /// <param name="viewModel">The data-bound item to retreive the element of.</param>
        /// <typeparam name="TElement">The type of the element (defined within the DataTemplate in XAML).</typeparam>
        /// <returns>The corresponding element, or Null if not found.</returns>
        public static TElement GetElementFromViewModel<TElement>(this ItemsControl self, object viewModel) where TElement : DependencyObject
        {
            var contentPresenter = (UIElement)self.ItemContainerGenerator.ContainerFromItem(viewModel);
            return contentPresenter.FindFirstChildOfType<TElement>();
        }

        /// <summary>Scrolls the element with the specified view-model to the top.</summary>
        /// <param name="self">The scroll viewer to scroll.</param>
        /// <param name="childViewModel">The model that is bound to the element to scroll.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown if the ScrollViewer does not contain an ItemsControl.</exception>
        /// <typeparam name="TElement">The type of the child element (defined within the DataTemplate in XAML).</typeparam>
        public static void ScrollToTop<TElement>(this ScrollViewer self, object childViewModel) where TElement : DependencyObject
        {
            self.ScrollToTop<TElement>(childViewModel, 0);
        }

        /// <summary>Scrolls the element with the specified view-model to the top.</summary>
        /// <param name="self">The scroll viewer to scroll.</param>
        /// <param name="childViewModel">The model that is bound to the element to scroll.</param>
        /// <param name="yOffset">A vertical offset to apply to the top position.</param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown if the ScrollViewer does not contain an ItemsControl.</exception>
        /// <typeparam name="TElement">The type of the child element (defined within the DataTemplate in XAML).</typeparam>
        public static void ScrollToTop<TElement>(this ScrollViewer self, object childViewModel, double yOffset) where TElement : DependencyObject
        {
            // Setup initial conditions.
            if (self == null) return;
            var itemsControl = self.FindFirstChildOfType<ItemsControl>();
            if (itemsControl == null) throw new ArgumentOutOfRangeException(string.Format("The ScrollViewer must contain an ItemsControl."));

            // Retrieve the corresponding child element to scroll.
            var element = itemsControl.GetElementFromViewModel<TElement>(childViewModel) as UIElement;
            if (element == null) return;

            // Scroll to the element to the top.
            var position = itemsControl.GetChildPosition(element);
            self.ScrollToVerticalOffset(position.Y + yOffset);
        }
        #endregion

        #region Focus
        /// <summary>Assigns focus to the first child of the given element that can recieve focus.</summary>
        /// <param name="self">The element to try to focus.</param>
        /// <returns>True if focus was applied, otherwise False.</returns>
        public static bool Focus(this UIElement self)
        {
            // Setup initial conditions.
            if (self == null) return false;

            // Check if the element is a focusable control.
            var control = self as Control;
            if (control != null) return control.Focus();

            // Look for the first focusable child within the element.
            control = self.FindFirstChildOfType<Control>();
            if (control != null) return control.Focus();

            // Finish up.
            return false;
        }

        private static bool GetIsFocusedChildOf<TParent>(DependencyObject element, DependencyObject focusedElement) where TParent : DependencyObject
        {
            if (focusedElement == null) return false;
            var parentElement = element;
            do
            {
                parentElement = parentElement.FindFirstVisualAncestor<TParent>();
                if (parentElement == focusedElement) return true;
            } while (parentElement != null);
            return false;
        }

        /// <summary>Sets focus on the selected item (if there is one).</summary>
        /// <param name="self">The ItemsControl to apply focus to.</param>
        /// <typeparam name="TItemType">The type of control that is rendered for each item.</typeparam>
        /// <returns>
        ///    True if focus was set to the control, or focus was already on the control. 
        ///    False if the control is not focusable, or there was no selected item.
        /// </returns>
        public static bool FocusSelectedItem<TItemType>(this Selector self) where TItemType : DependencyObject
        {
            // Setup initial conditions.
            if (self == null) return false;
            if (self.SelectedItem == null) return false;

            // Get the selected item element.
            var item = self.GetElementFromViewModel<TItemType>(self.SelectedItem) as Control;
            if (item == null) return false;

            // Run up the tree to get the containing [ListBoxItem].
            var listBoxItem = item.FindFirstVisualAncestor<ListBoxItem>() as Control;
            if (listBoxItem == null) return false;

            // Finish up.
            return listBoxItem.Focus();
        }

        /// <summary>Sets focus on the selected item (if there is one).</summary>
        /// <param name="self">The ItemsControl to apply focus to.</param>
        /// <param name="focusSelectorOnFail">
        ///    Flag indicating whether focus should be applied to the Selector if there is not selected item, 
        ///    or if the selected item cannot recieve focus.
        /// </param>
        /// <typeparam name="TItem">The type of control that is rendered for each item.</typeparam>
        /// <returns>
        ///    True if focus was set to the control, or focus was already on the control. 
        ///    False if the control is not focusable.
        /// </returns>
        public static bool FocusSelectedItem<TItem>(this Selector self, bool focusSelectorOnFail) where TItem : DependencyObject
        {
            if (self == null) return false;
            var wasFocused = self.FocusSelectedItem<TItem>();
            if (!wasFocused && focusSelectorOnFail)
            {
                return self.Focus();
            }
            else
            {
                return wasFocused;
            }
        }
        #endregion
    }
}

