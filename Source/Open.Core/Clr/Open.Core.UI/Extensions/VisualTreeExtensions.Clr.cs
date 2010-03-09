using System.Windows;
using System.Windows.Controls;

namespace Open.Core.Common
{
    public static partial class VisualTreeExtensions
    {
        private static bool RemoveChild(DependencyObject parent, UIElement child)
        {
            if (parent.GetType().IsA(typeof(Decorator)))
            {
                ((Decorator)parent).Child = null;
                return true;
            }
            return false;
        }
    }
}
