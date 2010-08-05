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
using System.Windows.Controls;
using Open.Core.Common;

using T = Open.Core.UI.Controls.ViewFactoryContent;

namespace Open.Core.UI.Controls
{
    /// <summary>Provides binding support for a view-model that implements IViewFactory.</summary>
    [Export(typeof(IViewFactoryContent))]
    public partial class ViewFactoryContent : UserControl, IViewFactoryContent
    {
        #region Head
        /// <summary>Fires when the 'IsViewLoaded' property changes.</summary>
        public event EventHandler IsViewLoadedChanged;
        private void FireIsViewLoadedChanged(){if (IsViewLoadedChanged != null) IsViewLoadedChanged(this, new EventArgs());}

        private readonly ContentContainerViewModel viewModel;
        private IViewFactory viewFactoryValue;
        private bool isViewLoaded;

        /// <summary>Constructor.</summary>
        public ViewFactoryContent()
        {
            InitializeComponent();
            viewModel = new ContentContainerViewModel();
            container.ViewModel = viewModel;
        }
        #endregion

        #region Event Handlers
        private void OnViewFactoryChanged()
        {
            // Determine if the value has actually changed.
            if (ViewFactory == viewFactoryValue) return;
            IsViewLoaded = false;

            // Update visual state.
            viewFactoryValue = ViewFactory;
            var view = ViewFactory == null ? null : viewFactoryValue.CreateView();

            // Wire up events.
            if (view != null) view.Loaded += OnViewLoaded;

            // Insert into visual tree.
            viewModel.Content = view;
        }

        private void OnViewLoaded(object sender, RoutedEventArgs e)
        {
            var view = sender as FrameworkElement;
            if (view != null) view.Loaded -= OnViewLoaded;
            IsViewLoaded = true;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the view has loaded.</summary>
        public bool IsViewLoaded
        {
            get { return isViewLoaded; }
            private set
            {
                if (value == IsViewLoaded) return;
                isViewLoaded = value;
                FireIsViewLoadedChanged();
            }
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the ViewFactory to bind to (typically a View-Model).</summary>
        public IViewFactory ViewFactory
        {
            get { return (IViewFactory)(GetValue(ViewFactoryProperty)); }
            set { SetValue(ViewFactoryProperty, value); }
        }
        /// <summary>Gets or sets the ViewFactory to bind to (typically a View-Model).</summary>
        public static readonly DependencyProperty ViewFactoryProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.ViewFactory),
                typeof(IViewFactory),
                typeof(T),
                new PropertyMetadata(null, (s, e) => ((T)s).OnViewFactoryChanged()));
        #endregion
    }
}