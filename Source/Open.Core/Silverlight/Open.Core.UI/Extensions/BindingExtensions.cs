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
using System.Windows.Data;
using Open.Core.Common;

namespace Open.Core.UI
{
    public static class BindingExtensions
    {
        #region Head
        // Property name constants.
        private static readonly string propOpacity = LinqExtensions.GetPropertyName<IOpacity>(m => m.Opacity);
        private static readonly string propVisibility = "Visibility";
        private static readonly string propColor = LinqExtensions.GetPropertyName<IBorder>(m => m.Color);
        private static readonly string propThickness = LinqExtensions.GetPropertyName<IBorder>(m => m.Thickness);
        private static readonly string propCornerRadius = LinqExtensions.GetPropertyName<IBorder>(m => m.CornerRadius);
        #endregion

        #region Method : SetBindings
        public static FrameworkElement SetBindings<TModel>(this FrameworkElement element)
        {
            if (element == null) return element;
            var type = typeof (TModel);

            if (type == typeof(IOpacity)) element.SetOpacityBindings();
            if (type == typeof(IVisibility)) element.SetVisibilityBindings();
            if (type == typeof(IBorder)) element.SetBorderBindings();

            return element;
        }

        private static void SetOpacityBindings(this FrameworkElement element)
        {
            element.SetBinding(UIElement.OpacityProperty, new Binding(propOpacity));
        }

        private static void SetVisibilityBindings(this FrameworkElement element)
        {
            element.SetBinding(UIElement.VisibilityProperty, new Binding(propVisibility));
        }

        private static void SetBorderBindings(this FrameworkElement element)
        {
            element.SetBinding(Border.BorderBrushProperty, new Binding(propColor));
            element.SetBinding(Border.BorderThicknessProperty, new Binding(propThickness));
            element.SetBinding(Border.CornerRadiusProperty, new Binding(propCornerRadius));
            element.SetVisibilityBindings();
            element.SetOpacityBindings();
        }
        #endregion
    }
}
