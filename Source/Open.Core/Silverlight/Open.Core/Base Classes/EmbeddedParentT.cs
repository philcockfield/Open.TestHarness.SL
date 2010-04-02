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
using System.Linq.Expressions;
using System.Windows.Controls;
using System.Windows.Data;

namespace Open.Core.Common
{
    /// <summary>
    ///    A simple 'embedded view-model' that can used internally with a control
    ///    to provide data-binding support back to the control's properties in situations
    ///    where using 'TemplateBinding' is not working.
    /// </summary>
    /// <typeparam name="T">The type of the control.</typeparam>
    /// <remarks>
    ///    The strategy for using this view-model:
    ///    1. Create an instance of this view-model within the control.
    ///    2. Set it as the DataContext on some element within the control that
    ///        you want it's children to be able to bind to the Control's properties.
    ///            NB: This should not be the root of the control (ie. not the UserControl)
    ///            as this strategy will break when the control has it's DataContext set explicitly   
    ///            when being consumed in a wider XAML setting.
    ///    3. Bind elements to the control properties using a path like this: {Binding Parent.PropertyName}
    ///        Make sure the property(s) you bind to are declared as a DependencyProperty.
    /// </remarks>
    public class EmbeddedParent<T> : ModelBase where T : Control
    {
        #region Head
        /// <summary>Constructor.</summary>
        /// <param name="parent">Gets parent control.</param>
        public EmbeddedParent(T parent)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            Parent = parent;
        }
        #endregion

        #region Properties
        /// <summary>Gets the parent control.</summary>
        public T Parent { get; private set; }
        #endregion

        #region Methods
        /// <summary>Creates a Binding object for the specified property.</summary>
        /// <param name="property">The property on the control.</param>
        /// <returns>A binding in the form of {Binding PropertyName}.</returns>
        public static Binding GetBinding<TObject>(Expression<Func<TObject, object>> property)
        {
            return new Binding(property.GetPropertyName());
        }

        /// <summary>Creates a Binding object for the specified Parent property.</summary>
        /// <param name="parentProperty">The property on the control.</param>
        /// <returns>A binding in the form of {Binding Parent.PropertyName}.</returns>
        public static Binding GetParentBinding(Expression<Func<T, object>> parentProperty)
        {
            var path = string.Format("Parent.{0}", parentProperty.GetPropertyName());
            return new Binding(path);
        }
        #endregion
    }
}
