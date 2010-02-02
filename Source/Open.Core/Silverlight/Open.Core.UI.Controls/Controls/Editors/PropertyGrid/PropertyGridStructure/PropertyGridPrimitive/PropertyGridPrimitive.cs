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
using System.Windows.Media;
using Open.Core.Common.AttachedBehavior;
using Open.Core.UI.Controls;

namespace Open.Core.Common.Controls.Editors.PropertyGridStructure
{
    /// <summary>
    ///    The fundamental property-grid that renders a grid of editable 
    ///    property values, without categorization grouping.
    /// </summary>
    public class PropertyGridPrimitive : ContentControl
    {
        #region Head
        public const string PropCellBackground = "CellBackground";

        public PropertyGridPrimitive()
        {
            Templates.Instance.ApplyTemplate(this);
            Behaviors.SetEnabledOpacity(this, new EnabledOpacity());
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public PropertyGridPrimitiveViewModel ViewModel
        {
            get { return DataContext as PropertyGridPrimitiveViewModel; }
            set { DataContext = value; }
        }

        /// <summary>Gets or sets the background color of the cells within the grid.</summary>
        public Brush CellBackground
        {
            get { return (Brush) (GetValue(CellBackgroundProperty)); }
            set { SetValue(CellBackgroundProperty, value); }
        }
        /// <summary>Gets or sets the background color of the cells within the grid.</summary>
        public static readonly DependencyProperty CellBackgroundProperty =
            DependencyProperty.Register(
                PropCellBackground,
                typeof (Brush),
                typeof (PropertyGridPrimitive),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));
        #endregion
    }
}
