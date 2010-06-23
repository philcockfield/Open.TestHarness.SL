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
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;
using T = Open.Core.UI.Controls.NamedControlListViewModel;

namespace Open.Core.UI.Controls
{
    [Export(typeof(INamedControlList))]
    public class NamedControlListViewModel : ViewModelBase, INamedControlList
    {
        #region Head
        public NamedControlListViewModel()
        {
            // Setup initial conditions.
            Items = new ObservableCollection<INamedControlListItem>();
            TitleFont = new FontSettings
                            {
                                FontSize = 18,
                                TextTrimming = TextTrimming.WordEllipsis,
                            };

            // Wire up events.
            Items.CollectionChanged += delegate { UpdateItemState(); };
        }
        #endregion

        #region Properties : INamedControlList
        public ObservableCollection<INamedControlListItem> Items { get; private set; }
        public IFontSettings TitleFont { get; private set; }

        public double ItemSpacing
        {
            get { return GetPropertyValue<T, double>(m => m.ItemSpacing); }
            set
            {
                if (SetPropertyValue<T, double>(m => m.ItemSpacing, value.WithinBounds(0, double.MaxValue)))
                {
                    UpdateItemState();
                }
            }
        }

        public Thickness ControlMargin
        {
            get { return GetPropertyValue<T, Thickness>(m => m.ControlMargin); }
            set { SetPropertyValue<T, Thickness>(m => m.ControlMargin, value); }
        }

        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get { return GetPropertyValue<T, ScrollBarVisibility>(m => m.HorizontalScrollBarVisibility, ScrollBarVisibility.Hidden); }
            set { SetPropertyValue<T, ScrollBarVisibility>(m => m.HorizontalScrollBarVisibility, value, ScrollBarVisibility.Hidden); }
        }

        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return GetPropertyValue<T, ScrollBarVisibility>(m => m.VerticalScrollBarVisibility, ScrollBarVisibility.Auto); }
            set { SetPropertyValue<T, ScrollBarVisibility>(m => m.VerticalScrollBarVisibility, value, ScrollBarVisibility.Auto); }
        }
        #endregion

        #region Methods
        public INamedControlListItem Add(string title, IViewFactory control)
        {
            return Insert(Items.Count, title, control);
        }

        public INamedControlListItem Insert(int index, string title, IViewFactory control)
        {
            if (control == null) throw new ArgumentNullException("control");
            var item = new NamedControlListItemViewModel(this , control) { Title = title };
            Items.Insert(index, item);
            return item;
        }

        public void Remove(INamedControlListItem item)
        {
            if (item == null) return;
            item.Dispose();
            Items.Remove(item);
        }

        public void Remove(IViewFactory control)
        {
            Remove(GetItem(control));
        }

        public INamedControlListItem GetItem(IViewFactory control)
        {
            return control == null ? null : Items.FirstOrDefault(m => m.Control == control);
        }

        public FrameworkElement CreateView()
        {
            return new NamedControlList { DataContext = this };
        }
        #endregion

        #region Internal
        private void UpdateItemState()
        {
            if (Items.IsEmpty()) return;
            var last = Items.LastOrDefault();
            foreach (NamedControlListItemViewModel item in Items)
            {
                item.UpdateState(item.Control == last.Control);
            }
        }
        #endregion
    }
}
