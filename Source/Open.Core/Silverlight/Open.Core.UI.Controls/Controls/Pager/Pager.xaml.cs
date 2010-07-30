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
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>
    /// Renders a Pager control to be used in any fashion
    /// Useful for any control(s) that require paging functionality
    /// </summary>
    public partial class Pager : UserControl
    {
        #region Head
        private DataContextObserver dataContextObserver;
        private PropertyObserver<PagerViewModel> viewModelObserver;

        /// <summary>Constructor.</summary>
        public Pager()
        {
            // Setup initial conditions.
            InitializeComponent();

            // Wire up events.
            dataContextObserver = new DataContextObserver(this, OnDataContextChanged);
            Loaded += delegate { InsertPages(); };
        }
        #endregion

        #region Event Handlers
        private void OnDataContextChanged()
        {
            if (viewModelObserver != null) viewModelObserver.Dispose();
            if (ViewModel == null) return;
            viewModelObserver = new PropertyObserver<PagerViewModel>(ViewModel)
                            .RegisterHandler(m => m.TotalPageButtons, InsertPages)
                            .RegisterHandler(m => m.TotalPages, InsertPages)
                            .RegisterHandler(m => m.CurrentPage, InsertPages);
        }

        private void OnPageClick(object sender, EventArgs e)
        {
            // Setup initial conditions.
            if (ViewModel == null) return;
            var button = (IButton)sender;

            // Determine which page button was clicked.
            var pageNumber = -1;
            int.TryParse(button.Tag.ToString(), out pageNumber);

            // Finish up.
            ViewModel.OnPageClicked(pageNumber);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public PagerViewModel ViewModel
        {
            get { return DataContext as PagerViewModel; }
            set { DataContext = value; }
        }

        public int CurrentPage { get { return ViewModel == null ? 0 : ViewModel.CurrentPage; } }
        public int TotalPages { get { return ViewModel == null ? 0 : ViewModel.TotalPages; } }
        public int TotalPageButtons { get { return ViewModel == null ? 0 : ViewModel.TotalPageButtons; } }
        #endregion

        #region Internal
        private void InsertPages()
        {
            // Setup initial conditions.
            pagerStackPanel.Children.Clear();
            if (TotalPages <= 1) return;

            // Determine min/max.
            int min;
            int max;
            CalculateMinMax(out min, out max);

            // Insert buttons.
            var showEllipsis = false;
            for (var i = 1; i <= TotalPages; i++)
            {
                if (i <= 2 || i > TotalPages - 2 || (min <= i && i <= max))
                {
                    var text = i.ToString(NumberFormatInfo.InvariantInfo);
                    var isCurrent = i == CurrentPage;
                    if (isCurrent)
                    {
                        InsertControl("currentPageTemplate", new PageViewModel(i, ViewModel));
                    }
                    else
                    {
                        InsertPageButton(text);
                    }

                    showEllipsis = true;
                }
                else if (showEllipsis)
                {
                    InsertControl("ellipsisTemplate", null);
                    showEllipsis = false;
                }
            }
        }

        private void CalculateMinMax(out int min, out int max)
        {
            min = CurrentPage - TotalPageButtons;
            max = CurrentPage + TotalPageButtons;
            if (max > TotalPages)
            {
                min -= max - TotalPages;
            }
            else if (min < 1)
            {
                max += 1 - min;
            }
        }

        private void InsertControl(string templateName, object viewModel)
        {
            var control = new ContentControl
                              {
                                  ContentTemplate = Resources[templateName] as DataTemplate,
                                  DataContext = viewModel,
                                  VerticalContentAlignment = VerticalAlignment.Stretch,
                                  HorizontalContentAlignment = HorizontalAlignment.Stretch,
                                  Content = new ContentPresenter(),
                              };
            pagerStackPanel.Children.Add(control);
        }

        private void InsertPageButton(string text)
        {
            // Create the model.
            var model = new ButtonTool
                                    {
                                        Text = text,
                                        Icon = null,
                                        IsDefaultBackgroundVisible = true,
                                        CanToggle = false,
                                        Tag = text,
                                        Margin = new Thickness(1, 0, 1, 0),
                                    };
            model.Click += OnPageClick;

            // Create the view.
            var view = model.CreateView();
            view.Width = 45;

            // Insert into visual tree.
            pagerStackPanel.Children.Add(view);
        }
        #endregion

        public class PageViewModel 
        {
            public PageViewModel(int pageNumber, PagerViewModel pager)
            {
                PageNumber = pageNumber;
                Pager = pager;
            }
            public int PageNumber { get; private set; }
            public PagerViewModel Pager { get; private set; }
        }
    }
}