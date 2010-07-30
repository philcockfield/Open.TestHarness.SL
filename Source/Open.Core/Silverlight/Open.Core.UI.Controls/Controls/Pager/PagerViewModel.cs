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
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Open.Core.Common;
using Open.Core.UI.Controls.Assets;
using T = Open.Core.UI.Controls.PagerViewModel;

namespace Open.Core.UI.Controls
{
    [Export(typeof(IPager))]
    public class PagerViewModel : ViewModelBase, IPager
    {
        #region Events
        public event EventHandler CurrentIndexChanged;
        private void FireCurrentIndexChanged() { if (CurrentIndexChanged != null) CurrentIndexChanged(this, new EventArgs()); }
        #endregion

        #region Head
        private const int DefaultPageCount = 15;
        private const int DefaultButtonCount = 2;
        private const int DefaultCurrentPage = 1;

        /// <summary>Constructor.</summary>
        public PagerViewModel()
        {
            UpdateVisualState();
        }
        #endregion

        #region Event Handlers
        internal void OnPageClicked(int pageNumber)
        {
            CurrentPage = pageNumber;
            UpdateVisualState();
        }

        private void OnPreviousClicked()
        {
            CurrentPage--;
            UpdateVisualState();
        }

        private void OnNextClicked()
        {
            CurrentPage++;
            UpdateVisualState();
        }
        #endregion

        #region Properties : IPager
        public bool IsEnabled
        {
            get { return Property.GetValue<T, bool>(m => m.IsEnabled, true); }
            set { Property.SetValue<T, bool>(m => m.IsEnabled, value, true); }
        }

        public int TotalPages
        {
            get { return Property.GetValue<T, int>(m => m.TotalPages, DefaultPageCount); }
            set
            {
                if (Property.SetValue<T, int>(m => m.TotalPages, value.WithinBounds(0, int.MaxValue), DefaultPageCount))
                {
                    // Ensure the CurrentPage is not greater than the total pages.
                    if (CurrentPage > TotalPages) CurrentPage = TotalPages;
                    if (TotalPages > 0 && CurrentPage == 0) CurrentPage = 1;
                    UpdateVisualState();
                }
            }
        }

        public int TotalPageButtons
        {
            get { return Property.GetValue<T, int>(m => m.TotalPageButtons, DefaultButtonCount); }
            set
            {
                Property.SetValue<T, int>(m => m.TotalPageButtons, value.WithinBounds(0, int.MaxValue), DefaultButtonCount);
                UpdateVisualState();
            }
        }

        public int CurrentPage
        {
            get { return Property.GetValue<T, int>(m => m.CurrentPage, DefaultCurrentPage); }
            set
            {
                if (Property.SetValue<T, int>(
                                                m => m.CurrentPage,
                                                value.WithinBounds(1, TotalPages), 
                                                DefaultCurrentPage))
                {
                    FireCurrentIndexChanged();
                    UpdateVisualState();
                }
            }
        }

        public int CurrentPageIndex
        {
            get { return CurrentPage - 1; }
            set { CurrentPage = value.WithinBounds(0, int.MaxValue) + 1; }
        }

        public bool IsLoading
        {
            get { return Property.GetValue<T, bool>(m => m.IsLoading); }
            set { Property.SetValue<T, bool>(m => m.IsLoading, value); }
        }
        #endregion

        #region Properties : ViewModel
        public bool IsNextEnabled
        {
            get { return Property.GetValue<T, bool>(m => m.IsNextEnabled); }
            private set { Property.SetValue<T, bool>(m => m.IsNextEnabled, value); }
        }

        public bool IsPreviousEnabled
        {
            get { return Property.GetValue<T, bool>(m => m.IsPreviousEnabled); }
            set { Property.SetValue<T, bool>(m => m.IsPreviousEnabled, value); }
        }

        public ICommand NextCommand { get { return GetCommand<T>(m => m.NextCommand, m => m.IsNextEnabled, OnNextClicked); } }
        public ICommand PreviousCommand { get { return GetCommand<T>(m => m.PreviousCommand, m => m.IsPreviousEnabled, OnPreviousClicked); } }

        public string LabelPrevious { get { return StringLibrary.Prompt_Previous; } }
        public string LabelNext { get { return StringLibrary.Prompt_Next; } }
        #endregion

        #region Methods
        public FrameworkElement CreateView()
        {
            return new Pager { ViewModel = this };
        }
        #endregion

        #region Internal
        private void UpdateVisualState()
        {
            IsPreviousEnabled = CurrentPage > 1;
            IsNextEnabled = CurrentPage < TotalPages;
        }
        #endregion
    }
}
