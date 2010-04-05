using System.Windows;
using System.Windows.Input;

namespace Open.Core
{
    public static class MouseExtensions
    {
        /// <summary>Determines whether the mouse is currently within the bounds of the given element.</summary>
        /// <param name="e">The event args returned from a mouse event (eg. MouseLeftButtonUp)</param>
        /// <param name="element">The element to examine.</param>
        public static bool IsMouseWithin(this MouseButtonEventArgs e, FrameworkElement element)
        {
            // Setup initial conditions.
            if (e == null) return false;
            if (element == null) return false;
            var position = e.GetPosition(element);

            // Check bounds.
            if (position.X < 0 || position.Y < 0) return false;
            return position.X <= element.ActualWidth && position.Y <= element.ActualHeight;
        }
    }
}
