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
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Specialized;
using System.Windows.Interactivity;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Automatically generates row definitions in a grid based on the number of items within the ItemsSource collection.</summary>
    public class AutoRowDefinitions : Behavior<Grid>
    {
        #region Head
        public const string PropItemsSource = "ItemsSource";

        public AutoRowDefinitions()
        {
            // Set default values.
            RowHeight = default(GridLength);
            MinRowHeight = double.NaN;
            MaxRowHeight = double.NaN;
        }
        #endregion

        #region Event Handlers
        private void HandleItemsSourceChanged()
        {
            // Unwire existing events.
            if (ObservableCollection != null) ObservableCollection.CollectionChanged -= HandleItemsCollectionChanged;

            // Store source collection, and load it.
            ObservableCollection = ItemsSource as INotifyCollectionChanged;
            LoadCollection(ItemsSource);

            // Wire up events.
            if (ObservableCollection != null) ObservableCollection.CollectionChanged += HandleItemsCollectionChanged;
        }

        private void HandleItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (RowDefinitions == null) return;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddRow(e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    RemoveRow(e.OldStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    // Ignore - no change.
                    break;

                case NotifyCollectionChangedAction.Reset:
                    RowDefinitions.Clear();
                    break;

                default: throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the collection of items this behavior is monitoring.</summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable) (GetValue(ItemsSourceProperty)); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        /// <summary>Gets or sets the collection of items this behavior is monitoring.</summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                PropItemsSource,
                typeof(IEnumerable),
                typeof (AutoRowDefinitions),
                new PropertyMetadata(null, (sender, e) => ((AutoRowDefinitions)sender).HandleItemsSourceChanged()));

        /// <summary>Gets the observable version of 'ItemsSource'.</summary>
        public INotifyCollectionChanged ObservableCollection { get; private set; }

        /// <summary>Gets the row definition collection.</summary>
        public RowDefinitionCollection RowDefinitions
        {
            get { return AssociatedObject == null ? null : AssociatedObject.RowDefinitions; }
        }

        /// <summary>Gets or sets the optional action to invoke when a row is added (the row index is passed to the Action).</summary>
        public Action<int> OnRowAdded { get; set; }

        /// <summary>Gets or sets the optional action to invoke when a row is removed (the row index is passed to the Action).</summary>
        public Action<int> OnRowRemoved { get; set; }
        #endregion

        #region Properties - Row Properties
        /// <summary>Gets or sets the height to apply to use when creating the row.</summary>
        public GridLength RowHeight { get; set; }

        /// <summary>Gets or sets the maximum height to use when creating the row.</summary>
        public double MaxRowHeight { get; set; }

        /// <summary>Gets or sets the minimum height to use when creating the row.</summary>
        public double MinRowHeight { get; set; }
        #endregion

        #region Methods
        protected override void OnAttached()
        {
            base.OnAttached();
            LoadCollection(ItemsSource);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (ObservableCollection != null) ObservableCollection.CollectionChanged -= HandleItemsCollectionChanged;
        }
        #endregion

        #region Internal
        private void AddRow() { AddRow(-1); }
        private void AddRow(int index)
        {
            // Setup initial conditions.
            if (RowDefinitions == null) return;
            if (index < 0) index = RowDefinitions.Count;

            // Insert the <RowDefinition>.
            RowDefinitions.Insert(index, CreateRowDefinition());

            // Finish up.
            if (OnRowAdded != null) OnRowAdded(index);
        }

        private void RemoveRow(int index)
        {
            // Setup initial conditions.
            if (RowDefinitions == null) return;

            // Remove the <RowDefinition>.
            RowDefinitions.Remove(RowDefinitions[index]);

            // Finish up.
            if (OnRowRemoved != null) OnRowRemoved(index);
        }

        private RowDefinition CreateRowDefinition()
        {
            var row = new RowDefinition();

            if (RowHeight != default(GridLength)) row.Height = RowHeight;
            if (!Equals(MinRowHeight, double.NaN)) row.MinHeight = MinRowHeight;
            if (!Equals(MaxRowHeight, double.NaN)) row.MaxHeight = MaxRowHeight;

            return row;
        }

        private void LoadCollection(IEnumerable items)
        {
            if (RowDefinitions != null) RowDefinitions.Clear();
            if (items != null)
            {
                foreach (var item in ItemsSource)
                {
                    AddRow();
                }
            }
        }
        #endregion
    }
}
