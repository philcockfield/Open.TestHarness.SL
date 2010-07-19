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
using Open.Core.Common;
using T = Open.Core.UI.Controls.PagerViewModel;

namespace Open.Core.UI.Controls
{
    internal enum PageButtonType
    {
        Previous,
        Next,
        Page
    }

    [Export(typeof(IPager))]
    public class PagerViewModel : ViewModelBase, IPager
    {
        #region Head
        public event EventHandler CurrentIndexChanged;
        private void FireCurrentIndexChanged(){if (CurrentIndexChanged != null) CurrentIndexChanged(this, new EventArgs());}

        private const int DefaultPageCount = 15;
        private const int DefaultButtonCount = 2;
        private const int DefaultCurrentPage = 1;
        #endregion

        #region Event Handlers
        internal void OnButtonClicked(PageButtonType buttonType, int? clickedIndex)
        {
            switch (buttonType)
            {
                case PageButtonType.Previous:
                    CurrentPage--;
                    break;

                case PageButtonType.Next:
                    CurrentPage++;
                    break;

                case PageButtonType.Page:
                    CurrentPage = clickedIndex.HasValue ? clickedIndex.Value : 0;
                    break;

                default: throw new NotSupportedException(buttonType.ToString());
            }
        }
        #endregion

        #region Properties
        public int TotalPages
        {
            get { return Property.GetValue<T, int>(m => m.TotalPages, DefaultPageCount); }
            set { Property.SetValue<T, int>(m => m.TotalPages, value, DefaultPageCount); }
        }

        public int TotalPageButtons
        {
            get { return Property.GetValue<T, int>(m => m.TotalPageButtons, DefaultButtonCount); }
            set { Property.SetValue<T, int>(m => m.TotalPageButtons, value, DefaultButtonCount); }
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
                }
            }
        }

        public int CurrentPageIndex
        {
            get { return CurrentPage - 1; }
            set { CurrentPage = value.WithinBounds(0, int.MaxValue) + 1; }
        }
        #endregion

        #region Methods
        public FrameworkElement CreateView()
        {
            return new Pager { ViewModel = this };
        }
        #endregion
    }
}
