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
using Open.Core.Common;

using T = Open.Core.UI.Controls.ArrowPointerButtonBase;

namespace Open.Core.UI.Controls
{
    /// <summary>Flags representing the direction the triangle point is facing.</summary>
    public enum ArrowDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    /// <summary>The base class for buttons that represent a pointing arrow.</summary>
    public abstract class ArrowPointerButtonBase : ShapeButton
    {
        #region Head
        /// <summary>Constructor.</summary>
        protected ArrowPointerButtonBase()
        {
            UpdateShape();
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the direction the triangle point is facing.</summary>
        public ArrowDirection PointerDirection
        {
            get { return (ArrowDirection) (GetValue(PointerDirectionProperty)); }
            set { SetValue(PointerDirectionProperty, value); }
        }
        /// <summary>Gets or sets the direction the triangle point is facing.</summary>
        public static readonly DependencyProperty PointerDirectionProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.PointerDirection),
                typeof (ArrowDirection),
                typeof (T),
                new PropertyMetadata(ArrowDirection.Right, (s, e) => ((T) s).UpdateShape()));
        #endregion

        #region Methods
        /// <summary>Updates the 'ShapePathData' based on the current 'PointerDirection'.</summary>
        /// <param name="pointerDirection">The direction of the pointer.</param>
        protected abstract void UpdateShape(ArrowDirection pointerDirection);
        #endregion

        #region Internal
        private void UpdateShape()
        {
            UpdateShape(PointerDirection);
        }
        #endregion
    }
}
