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

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Open.Core.Common.AttachedBehavior;

namespace Open.Core.UI.Controls
{
    /// <summary>Automatically generates rows and columns based on a data-source.</summary>
    /// <remarks>
    ///    Any 'RowDefinition' and 'ColumnDefinition' items that are declared within the Grid are overridden by the control.
    ///    Declare columns using the extended 'Columns' propertying with the 'AutoColumnDefinition' object.
    /// </remarks>
    public class AutoGrid : Grid
    {
        #region Head
        public const string PropItemsSource = "ItemsSource";
        public const string PropRowHeight = "RowHeight";
        public const string PropMaxRowHeight = "MaxRowHeight";
        public const string PropMinRowHeight = "MinRowHeight";

        private static readonly RowDefinition defaultRow = new RowDefinition();
        private readonly AutoRowDefinitions autoRowDefinitions = new AutoRowDefinitions();
        private ObservableCollection<AutoColumnDefinition> columns;
        private readonly List<Row> rows = new List<Row>();

        public AutoGrid()
        {
            // Apply the auto-row definition.
            Behaviors.SetAutoRowDefinitions(this, autoRowDefinitions);

            // Wire up events.
            Loaded += HandleLoaded;
        }
        #endregion

        #region Event Handlers
        void HandleLoaded(object sender, RoutedEventArgs e)
        {
            // Setup initial conditions.
            Loaded -= HandleLoaded;

            // Initialize.
            InitializeColumns();
            LoadRows();

            // Wire up events.
            autoRowDefinitions.OnRowAdded = OnRowAdded;
            autoRowDefinitions.OnRowRemoved = OnRowRemoved;
        }

        private void OnItemsSourceChanged()
        {
            // Store source collection.
            autoRowDefinitions.ItemsSource = ItemsSource;
        }

        private void OnRowAdded(int index)
        {
            if (ItemsSource == null || ItemsSource.Count - 1 < index) return;
            InsertRow(index, ItemsSource[index]);
        }

        private void OnRowRemoved(int index)
        {
            RemoveRow(index);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the collection of items being generated within the grid.</summary>
        /// <remarks>Ensure that this is an 'ObservableCollection'.</remarks>
        public IList ItemsSource
        {
            get { return (IList)(GetValue(ItemsSourceProperty)); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        /// <summary>Gets or sets the collection of items being generated within the grid.</summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                PropItemsSource,
                typeof (IEnumerable),
                typeof (AutoGrid),
                new PropertyMetadata(null, (sender, e) => ((AutoGrid)sender).OnItemsSourceChanged()));


        /// <summary>Gets the set of column definitions.</summary>
        public ObservableCollection<AutoColumnDefinition> Columns
        {
            get
            {
                if (columns == null) columns = new ObservableCollection<AutoColumnDefinition>();
                return columns;
            }
        }
        #endregion

        #region Properties - Row Height
        /// <summary>Gets or sets the height to apply to use when creating the row.</summary>
        public GridLength RowHeight
        {
            get { return (GridLength) (GetValue(RowHeightProperty)); }
            set { SetValue(RowHeightProperty, value); }
        }
        /// <summary>Gets or sets the height to apply to use when creating the row.</summary>
        public static readonly DependencyProperty RowHeightProperty =
            DependencyProperty.Register(
                PropRowHeight,
                typeof (GridLength),
                typeof (AutoGrid),
                new PropertyMetadata(defaultRow.Height, (sender, e) => ((AutoGrid)sender).autoRowDefinitions.RowHeight = (GridLength)e.NewValue));
        

        /// <summary>Gets or sets the maximum height to use when creating the row.</summary>
        public double MaxRowHeight
        {
            get { return (double) (GetValue(MaxRowHeightProperty)); }
            set { SetValue(MaxRowHeightProperty, value); }
        }
        /// <summary>Gets or sets the maximum height to use when creating the row.</summary>
        public static readonly DependencyProperty MaxRowHeightProperty =
            DependencyProperty.Register(
                PropMaxRowHeight,
                typeof (double),
                typeof (AutoGrid),
                new PropertyMetadata(defaultRow.MaxHeight, (sender, e) => ((AutoGrid)sender).autoRowDefinitions.MaxRowHeight = (double)e.NewValue));
        

        /// <summary>Gets or sets the minimum height to use when creating the row.</summary>
        public double MinRowHeight
        {
            get { return (double) (GetValue(MinRowHeightProperty)); }
            set { SetValue(MinRowHeightProperty, value); }
        }
        /// <summary>Gets or sets the minimum height to use when creating the row.</summary>
        public static readonly DependencyProperty MinRowHeightProperty =
            DependencyProperty.Register(
                PropMinRowHeight,
                typeof (double),
                typeof (AutoGrid),
                new PropertyMetadata(defaultRow.MinHeight, (sender, e) => ((AutoGrid)sender).autoRowDefinitions.MinRowHeight = (double)e.NewValue));
        #endregion

        #region Internal
        private void InitializeColumns()
        {
            // Setup initial conditions.
            if (columns == null || columns.Count == 0) return;
            ColumnDefinitions.Clear();

            // Add columns.
            var i = 0;
            foreach (var item in Columns)
            {
                var columnDef = new ColumnDefinition
                                    {
                                        Width = item.Width,
                                        MinWidth = item.MinWidth,
                                        MaxWidth = item.MaxWidth
                                    };
                ColumnDefinitions.Add(columnDef);
                item.Index = i;
                i++;
            }
        }

        private void LoadRows()
        {
            if (ItemsSource == null) return;
            Children.Clear();
            var i = 0;
            foreach (var item in ItemsSource)
            {
                InsertRow(i, item);
                i++;
            }
        }

        private void InsertRow(int rowIndex, object model)
        {
            var row = new Row(this, rowIndex, model);
            rows.Add(row);
            if (rowIndex < rows.Count - 1)
            {
                ReIndexRows();
            }
        }

        private void RemoveRow(int rowIndex)
        {
            // Setup initial conditions.
            var row = rows.FirstOrDefault(m => m.Index == rowIndex);
            if (row == null) return;

            // Remove the row.
            row.Remove();
            rows.Remove(row);

            // Finish up.
           ReIndexRows();
        }

        private void ReIndexRows()
        {
            var i = 0;
            foreach (var model in ItemsSource)
            {
                var row = rows.FirstOrDefault(m => m.Model == model);
                if (row != null) row.Index = i;
                i++;
            }
        }
        #endregion

        /// <summary>Represents a single row.</summary>
        private class Row 
        {
            #region Head
            private int index = -1;
            private readonly AutoGrid parent;
            private readonly List<ContentControl> cells = new List<ContentControl>();

            public Row(AutoGrid parent, int index, object model)
            {
                // Store values.
                this.parent = parent;
                Index = index;
                Model = model;

                // Initialize the cells.
                CreateCells();
            }
            #endregion

            #region Properties
            public object Model { get; private set; }
            public int Index
            {
                get { return index; }
                set
                {
                    if (value == Index) return;
                    index = value;
                    UpdateRowIndexes();
                }
            }
            #endregion

            #region Methods
            public void Remove()
            {
                foreach (var control in cells)
                {
                    parent.Children.Remove(control);
                }
            }
            #endregion

            #region Internal
            private void CreateCells()
            {
                foreach (var item in parent.Columns)
                {
                    var cell = CreateCell(item);
                    if (cell != null) cells.Add(cell);
                }
                UpdateRowIndexes();
            }

            private ContentControl CreateCell(AutoColumnDefinition definition)
            {
                // Setup initial conditions.
                if (definition.Template == null) return null;
                var control = new ContentControl
                                                {
                                                    DataContext = Model,
                                                    Template = definition.Template,
                                                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                                                    VerticalContentAlignment = VerticalAlignment.Stretch
                                                };

                // Position within <Grid>
                SetColumn(control, definition.Index);

                // Finish up.
                parent.Children.Add(control);
                return control;
            }

            private void UpdateRowIndexes()
            {
                foreach (var cell in cells)
                {
                    SetRow(cell, Index);
                }
            }
            #endregion
        }
    }
}
