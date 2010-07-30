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
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.Common.Collection;
using Open.Core.UI.Controls.Assets;

using T = Open.Core.UI.Controls.SelectionListViewModel;

namespace Open.Core.UI.Controls
{
    /// <summary>
    ///     An extended ListBox for displaying selections with the ability to easily control individual item templates
    ///     and display options for when the list is empty, and paging etc.
    /// </summary>
    [Export(typeof(ISelectionList))]
    public class SelectionListViewModel : ViewModelBase, ISelectionList
    {
        #region Events
        /// <summary>Fires when the SelectedItem changes.</summary>
        public event EventHandler SelectionChanged;
        private void OnSelectionChanged() { if (SelectionChanged != null) SelectionChanged(this, new EventArgs()); }
        #endregion

        #region Head
        private static readonly Thickness defaultItemPadding = new Thickness(5);
        private static readonly Brush defaultDividerColor = Colors.Black.ToBrush(0.15);
        
        private readonly ResourceDictionary templates;
        private ItemViewModel selectedItem;


        /// <summary>Constructor.</summary>
        public SelectionListViewModel()
        {
            // Setup initial conditions.
            Items = new ObservableCollection<object>();
            ItemsWrapper = new ObservableCollectionWrapper<object, ItemViewModel>(Items, o => new ItemViewModel(this, o));
            templates = Templates.Instance.Dictionary;

            // Wire up events.
            Items.CollectionChanged += delegate { OnPropertyChanged<T>(m => m.IsItemsVisible); };
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            EmptyMessage = null;
            ItemsWrapper.Dispose();
        }
        #endregion

        #region Properties - ISelectionList
        /// <summary>Gets the collection of items.</summary>
        public ObservableCollection<object> Items { get; private set; }

        /// <summary>Gets or sets the currently selected item.</summary>
        public object SelectedItem
        {
            get { return SelectedItemWrapper == null ? null : SelectedItemWrapper.Value; }
            set
            {
                // Setup initial conditions.
                if (value == SelectedItem) return;

                // Pass execution to the wrapper method.
                SelectedItemWrapper = ItemsWrapper.GetWrapper(value);
            }
        }

        /// <summary>Gets or sets the Message shown when the list is empty (null to use default).  Pass a UIElement to perform custom rendering.</summary>
        public object EmptyMessage
        {
            get { return GetPropertyValue<T, object>(m => m.EmptyMessage); }
            set { SetPropertyValue<T, object>(m => m.EmptyMessage, value, m => m.EmptyMessageTemplate, m => m.EmptyMessageValue); }
        }

        /// <summary>Gets or sets the color of the item divider.</summary>
        public Brush ItemDividerColor
        {
            get { return GetPropertyValue<T, Brush>(m => m.ItemDividerColor, defaultDividerColor); }
            set { SetPropertyValue<T, Brush>(m => m.ItemDividerColor, value, defaultDividerColor); }
        }

        /// <summary>Gets or sets the padding to apply to the item.</summary>
        public Thickness ItemPadding
        {
            get { return GetPropertyValue<T, Thickness>(m => m.ItemPadding, defaultItemPadding); }
            set { SetPropertyValue<T, Thickness>(m => m.ItemPadding, value, defaultItemPadding); }
        }

        /// <summary>Gets or sets the selector for the DataTemplate which renders each item (passes the item value to the Func).</summary>
        public Func<object, DataTemplate> ItemTemplateSelector
        {
            get { return GetPropertyValue<T, Func<object, DataTemplate>>(m => m.ItemTemplateSelector); }
            set { SetPropertyValue<T, Func<object, DataTemplate>>(m => m.ItemTemplateSelector, value); }
        }
        #endregion

        #region Properties - View Model Internals
        public ItemViewModel SelectedItemWrapper
        {
            get { return selectedItem; }
            set
            {
                // Setup initial conditions.
                if (value == SelectedItem) return;

                // Store value.
                selectedItem = value;

                // Alert listeners.
                OnPropertyChanged<T>(m => m.SelectedItem, m => m.SelectedItemWrapper);
                OnSelectionChanged();
            }
        }

        public ObservableCollectionWrapper<object, ItemViewModel> ItemsWrapper { get; private set; }
        public bool IsItemsVisible { get { return Items.Count != 0; } }
        public object EmptyMessageValue
        {
            get { return EmptyMessage ?? StringLibrary.SelectionList_EmptyMessage; }
        }

        public DataTemplate EmptyMessageTemplate
        {
            get
            {
                var template = EmptyMessageValue is string
                                   ? templates["SelectionList.DefaultEmptyMessage"]
                                   : templates["SelectionList.CustomEmptyMessage"];
                return template as DataTemplate;
            }
        }
        #endregion

        #region Methods
        public FrameworkElement CreateView()
        {
            return new SelectionList { DataContext = this };
        }
        #endregion

        public class ItemViewModel : ViewModelBase
        {
            #region Head

            private DataTemplate defaultTemplate;

            public ItemViewModel(SelectionListViewModel parent, object value)
            {
                Parent = parent;
                Value = value;
                defaultTemplate = Parent.templates["SelectionList.DefaultItemTemplate"] as DataTemplate;
            }
            #endregion

            #region Properties
            public SelectionListViewModel Parent { get; private set; }
            public object Value { get; private set; }
            public DataTemplate Template
            {
                get
                {
                    var template = Parent.ItemTemplateSelector == null
                               ? defaultTemplate
                               : Parent.ItemTemplateSelector(Value);
                    return template ?? defaultTemplate;
                }
            }
            #endregion
        }
    }
}