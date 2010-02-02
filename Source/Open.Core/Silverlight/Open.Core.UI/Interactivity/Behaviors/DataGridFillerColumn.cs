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
using System.Diagnostics;
using System.Windows.Interactivity;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Sets a column within a DataGrid to fill the available space.</summary>
    public class DataGridFillerColumn : Behavior<DataGrid>
    {
        #region Head
        private const int ScrollBarOffset = 17;

        private int columnIndex;
        private DelayedAction updateDelay;
        #endregion

        #region Event Handlers
        private void HandleLoaded(object sender, RoutedEventArgs e)
        {
            // Setup initial conditions.
            AssociatedObject.Loaded -= HandleLoaded;
            if (!UpdateColumn()) return;

            // Wire up events.
            AssociatedObject.SizeChanged += HandleSizeChanged;
            AssociatedObject.LoadingRow += HandleLoadingRow;

            // Finish up.
            UpdateWidthInternal();
        }

        private void HandleSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateWidth(true);
        }

        void HandleLoadingRow(object sender, DataGridRowEventArgs e)
        {
            UpdateWidth(true);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the index of the column (0-based).</summary>
        public int ColumnIndex
        {
            get { return columnIndex; }
            set
            {
                if (value < 0) value = 0;
                columnIndex = value;
                UpdateColumn();
                UpdateWidthInternal();
            }
        }

        /// <summary>Gets the column that is the expanding 'filler' column.</summary>
        /// <remarks>
        ///    This column corresponds to the named column identified by the 'ColumnName' property.
        ///    Make sure you have named the column you want as the filler and set the 'ColumnName' property.
        /// </remarks>
        public DataGridColumn Column { get; private set; }
        #endregion

        #region Properties - Internal
        private double BorderOffset
        {
            get { return AssociatedObject.BorderThickness.Left + AssociatedObject.BorderThickness.Right; }
        }
        #endregion

        #region Methods
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += HandleLoaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            Column = null;
            AssociatedObject.SizeChanged -= HandleSizeChanged;
            AssociatedObject.LoadingRow -= HandleLoadingRow;
        }

        /// <summary>Updates the width of the column to make it fill the available space (with no delay).</summary>
        public void UpdateWidth()
        {
            UpdateWidth(false);
        }

        /// <summary>Updates the width of the column to make it fill the available space.</summary>
        /// <param name="delayedAction">Flag indicating if the update should only occur after a brief delay (to prevent needless changes on redraws).</param>
        public void UpdateWidth(bool delayedAction)
        {
            if (Column == null) return;
            if (updateDelay == null) updateDelay = new DelayedAction(0.02, UpdateWidthInternal);
            if (delayedAction) updateDelay.Start(); else UpdateWidthInternal();
        }
        #endregion

        #region Internal
        private void UpdateWidthInternal()
        {
            // Setup initial conditions.
            if (Column == null) return;

            // Calculate the filler-column width.
            var totalWidth = 0d;
            foreach (var column in AssociatedObject.Columns)
            {
                if (column != Column) totalWidth += column.ActualWidth;
            }
            var width = AssociatedObject.GetActualSize().Width - totalWidth - BorderOffset;
            if (AssociatedObject.IsScrolling()) width -= ScrollBarOffset;
            if (width < 0) width = 0;

            // Update the width.
            Column.Width = new DataGridLength(width);
        }

        private bool UpdateColumn()
        {
            // Setup initial conditions.
            if (AssociatedObject == null) return false;
            if (ColumnIndex > AssociatedObject.Columns.Count - 1)
            {
                var name = AssociatedObject.Name.AsNullWhenEmpty() == null ? "<unnamed>" : AssociatedObject.Name;
                Debug.WriteLine(string.Format("Could not set column-filler behavior on the DataGrid named '{0}'. The ColumnIndex '{1}' is out of bounds.", name, ColumnIndex));
                return false;
            }

            // Undo current column.
            if (Column != null) Column.Width = default(DataGridLength);

            // Find the corresponding column.
            Column = AssociatedObject.Columns[ColumnIndex];

            // Finish up.
            return true;
        }
        #endregion
    }
}
