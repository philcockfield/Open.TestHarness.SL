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

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>An attached behavior that invokes an animation when a named property changes on the element's view-model.</summary>
    public abstract class NamedPropertyAnimation : PropertyAnimation
    {
        #region Head
        public const string PropPropertyName = "PropertyName";
        #endregion

        #region Properties
        /// <summary>Gets the current value of the opacity property (see 'PropertyName') on the ViewModel.</summary>
        public double PropertyValue
        {
            get { return (double)GetPropertyValue(PropertyName); }
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the name of the double property that specifies the 'Opacity' on the ViewModel.</summary>
        public string PropertyName
        {
            get { return (string)(GetValue(PropertyNameProperty)); }
            set { SetValue(PropertyNameProperty, value); }
        }
        /// <summary>Gets or sets the name of the double property that specifies the 'Opacity' on the ViewModel.</summary>
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.Register(
                PropPropertyName,
                typeof(string),
                typeof(NamedPropertyAnimation),
                new PropertyMetadata(null));
        #endregion

    }
}
