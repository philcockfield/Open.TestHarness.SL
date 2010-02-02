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

using T = Open.Core.Common.AttachedBehavior.PersistentColumnSize;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>A behavior that stores the size of a Grid column.</summary>
    public class PersistentColumnSize : PersistentGridSizeBase
    {
        #region Head
        public PersistentColumnSize()
        {
            Dimension = SizeDimension.Width;
        }
        #endregion

        #region Event Handlers
        private void OnColumnDefinitionChanged( )
        {
            SyncSizeWithValue();
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the column to persist.</summary>
        public ColumnDefinition ColumnDefinition
        {
            get { return (ColumnDefinition) (GetValue(ColumnDefinitionProperty)); }
            set { SetValue(ColumnDefinitionProperty, value); }
        }
        /// <summary>Gets or sets the column to persist.</summary>
        public static readonly DependencyProperty ColumnDefinitionProperty =
            DependencyProperty.Register(
                            LinqExtensions.GetPropertyName<T>(m => m.ColumnDefinition),
                            typeof (ColumnDefinition),
                            typeof (T),
                            new PropertyMetadata(null, (s, e) => ((T) s).OnColumnDefinitionChanged()));
        #endregion

        #region Properties - Protected
        protected override bool IsAttached { get { return ColumnDefinition == null ? false : base.IsAttached; } }

        protected override GridLength GridLength
        {
            get { return ColumnDefinition == null ? default(GridLength) : ColumnDefinition.Width; }
            set { if (ColumnDefinition != null) ColumnDefinition.Width = value; }
        }
        #endregion
    }
}
