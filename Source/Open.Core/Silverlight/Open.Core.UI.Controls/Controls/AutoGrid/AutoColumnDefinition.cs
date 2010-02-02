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

namespace Open.Core.UI.Controls
{
    /// <summary>Defines a column that is auto-generated within an 'AutoGrid'.</summary>
    public class AutoColumnDefinition : DependencyObject
    {
        #region Head
        public const string PropWidth = "Width";
        public const string PropMinWidth = "MinWidth";
        public const string PropMaxWidth = "MaxWidth";
        public const string PropTemplate = "Template";

        private static readonly ColumnDefinition DefaultColumn = new ColumnDefinition();
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the index of the column.</summary>
        /// </summary>
        internal int Index { get; set; }

        /// <summary>Gets or sets the width of the column.</summary>
        public GridLength Width
        {
            get { return (GridLength) (GetValue(WidthProperty)); }
            set { SetValue(WidthProperty, value); }
        }
        /// <summary>Gets or sets the width of the column.</summary>
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register(
                PropWidth,
                typeof (GridLength),
                typeof (AutoColumnDefinition),
                new PropertyMetadata(DefaultColumn.Width));
        

        /// <summary>Gets or sets the minimum width of the column.</summary>
        public double MinWidth
        {
            get { return (double) (GetValue(MinWidthProperty)); }
            set { SetValue(MinWidthProperty, value); }
        }
        /// <summary>Gets or sets the minimum width of the column.</summary>
        public static readonly DependencyProperty MinWidthProperty =
            DependencyProperty.Register(
                PropMinWidth,
                typeof (double),
                typeof (AutoColumnDefinition),
                new PropertyMetadata(DefaultColumn.MinWidth));

        
        /// <summary>Gets or sets the maximum width of the column.</summary>
        public double MaxWidth
        {
            get { return (double) (GetValue(MaxWidthProperty)); }
            set { SetValue(MaxWidthProperty, value); }
        }
        /// <summary>Gets or sets the maximum width of the column.</summary>
        public static readonly DependencyProperty MaxWidthProperty =
            DependencyProperty.Register(
                PropMaxWidth,
                typeof (double),
                typeof (AutoColumnDefinition),
                new PropertyMetadata(DefaultColumn.MaxWidth));


        /// <summary>Gets or sets the template to use to render the column.</summary>
        public ControlTemplate Template
        {
            get { return (ControlTemplate)(GetValue(TemplateProperty)); }
            set { SetValue(TemplateProperty, value); }
        }
        /// <summary>Gets or sets the template to use to render the column.</summary>
        public static readonly DependencyProperty TemplateProperty =
            DependencyProperty.Register(
                PropTemplate,
                typeof(ControlTemplate),
                typeof(AutoColumnDefinition),
                new PropertyMetadata(null));
        
        #endregion
    }
}
