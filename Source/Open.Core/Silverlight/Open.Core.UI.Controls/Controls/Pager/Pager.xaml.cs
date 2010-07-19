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
using System.Windows.Media;
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
        private const string Previous = "Previous";
        private const string Next = "Next";
        private const string Elipses = "...";

        private DataContextObserver dataContextObserver;
        private PropertyObserver<PagerViewModel> viewModelObserver;

        /// <summary>Constructor.</summary>
        public Pager()
        {
            // Setup initial conditions.
            InitializeComponent();

            // Wire up events.
            dataContextObserver = new DataContextObserver(this, OnDataContextChanged);
            Loaded += delegate { BuildPager(); };
        }
        #endregion

        #region Event Handlers
        private void OnDataContextChanged()
        {
            if (viewModelObserver != null) viewModelObserver.Dispose();
            if (ViewModel == null) return;
            viewModelObserver = new PropertyObserver<PagerViewModel>(ViewModel)
                .RegisterHandler(m => m.TotalPageButtons, BuildPager)
                .RegisterHandler(m => m.TotalPages, BuildPager)
                .RegisterHandler(m => m.CurrentPage, BuildPager);
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            // Setup initial conditions.
            if (ViewModel == null) return;
            var button = (Button)e.OriginalSource;

            // Determine the type of button that was clicked.
            var clickedIndex = -1;
            PageButtonType buttonType;
            if (button.Tag.ToString() == Previous)
            {
                buttonType = PageButtonType.Previous;
            }
            else if (button.Tag.ToString() == Next)
            {
                buttonType = PageButtonType.Next;
            }
            else
            {
                buttonType = PageButtonType.Page;
                int.TryParse(button.Tag.ToString(), out clickedIndex);
            }

            // Finish up.
            ViewModel.OnButtonClicked(buttonType, clickedIndex < 0 ? (int?)null : clickedIndex);
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
        /// <summary>Renders a Button control for the Pager.</summary>
        /// <param name="text">Text to display</param>
        /// <param name="isInner">Flag to denote if this refers to the inner buttons or the Previous/Next buttons</param>
        /// <param name="isEnabled">Flag to denote if this button is enabled</param>
        private Button BuildButton(string text, bool isInner, bool isEnabled)
        {
            var button = new Button
                             {
                                 Content = text,
                                 Tag = text,
                                 Style = isInner ? Resources["PagerButtonInnerStyle"] as Style : Resources["PagerButtonOuterStyle"] as Style,
                                 Width = 45
                             };
            if (isInner == false) button.Width = 75;
            button.IsEnabled = isEnabled;
            button.Click += OnButtonClick;
            return button;
        }

        /// <summary>Renders a selected Button or the ellipsis (...) TextBlock</summary>
        /// <param name="text">Text to display</param>
        /// <param name="hasBorder">Flag to denote if this button is to have a border</param>
        /// <returns>UIElement (either a TextBlock or a Border with a TextBlock within)</returns>
        private static UIElement BuildSpan(string text, bool hasBorder)
        {
            if (hasBorder)
            {
                var textBlock = new TextBlock
                                    {
                                        Text = text,
                                        TextAlignment = TextAlignment.Center,
                                        VerticalAlignment = VerticalAlignment.Center,
                                        Width = 30
                                    };
                var border = new Border
                                 {
                                     Margin = new Thickness(3, 3, 3, 3),
                                     BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5),
                                     BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255)),
                                     Width = 30,
                                     Height = 22,
                                     HorizontalAlignment = HorizontalAlignment.Center,
                                     VerticalAlignment = VerticalAlignment.Center,
                                     Child = textBlock
                                 };
                return border;
            }
            else
            {
                return new TextBlock
                           {
                               Text = text,
                               Width = 30,
                               Height = 22,
                               TextAlignment = TextAlignment.Center,
                               VerticalAlignment = VerticalAlignment.Bottom
                           };
            }
        }

        /// <summary>Build the Pager Control.</summary>
        private void BuildPager()
        {
            // Setup initial conditions.
            pagerStackPanel.Children.Clear();
            if (TotalPages <= 1) return;

            // Determine min/max.
            var min = CurrentPage - TotalPageButtons;
            var max = CurrentPage + TotalPageButtons;
            if (max > TotalPages)
            {
                min -= max - TotalPages;
            }
            else if (min < 1)
            {
                max += 1 - min;
            }

            // Previous Button
            pagerStackPanel.Children.Add(BuildButton(Previous, isInner: false, isEnabled: (CurrentPage > 1)));

            // Middle Buttons
            var showDivider = false;
            for (var i = 1; i <= TotalPages; i++)
            {
                if (i <= 2 || i > TotalPages - 2 || (min <= i && i <= max))
                {
                    var text = i.ToString(NumberFormatInfo.InvariantInfo);
                    pagerStackPanel.Children.Add(i == CurrentPage
                                                     ? BuildSpan(text, hasBorder:false)
                                                     : BuildButton(text, isInner:true, isEnabled:true));
                    showDivider = true;
                }
                else if (showDivider)
                {
                    // This will add the ellipsis (...) TextBlock
                    pagerStackPanel.Children.Add(BuildSpan(Elipses, false));
                    showDivider = false;
                }
            }

            // Next Button
            pagerStackPanel.Children.Add(BuildButton(Next, isInner: false, isEnabled: (CurrentPage < TotalPages)));
        }
        #endregion
    }
}