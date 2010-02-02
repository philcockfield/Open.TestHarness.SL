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
using System.Windows.Input;

namespace Open.Core.Common
{
    public static partial class FrameworkElementExtensions
    {
        #region Focused Element
        /// <summary>Determines whether the object is the child of the given type of focused element (using a focus-scope of the 'MainWindow').</summary>
        /// <typeparam name="TParent">The type of the parent to look for.</typeparam>
        /// <param name="element">The object making the call.</param>
        /// <returns>True if the element is the child of a focused parent, otherwise False.</returns>
        public static bool IsFocusedChildOf<TParent>(this DependencyObject element) where TParent : DependencyObject
        {
            return IsFocusedChildOf<TParent>(element, Application.Current.MainWindow);
        }

        /// <summary>Determines whether the object is the child of the given type of focused element.</summary>
        /// <typeparam name="TParent">The type of the parent to look for.</typeparam>
        /// <param name="element">The object making the call.</param>
        /// <param name="focusScope">The focus scope to look within.</param>
        /// <returns>True if the element is the child of a focused parent, otherwise False.</returns>
        public static bool IsFocusedChildOf<TParent>(this DependencyObject element, DependencyObject focusScope) where TParent : DependencyObject
        {
            var focusedElement = FocusManager.GetFocusedElement(focusScope) as DependencyObject;
            return GetIsFocusedChildOf<TParent>(element, focusedElement);
        }
        #endregion
    }
}
