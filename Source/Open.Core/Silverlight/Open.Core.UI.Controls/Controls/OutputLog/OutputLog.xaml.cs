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

using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;

using T = Open.Core.UI.Controls.OutputLog;

namespace Open.Core.UI.Controls
{
    /// <summary>An output display log.</summary>
    public partial class OutputLog : UserControl, INotifyPropertyChanged
    {
        #region Events
        /// <summary>Fires when a property on the object has changed value.</summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void FirePropertyChangedEvent(PropertyChangedEventArgs e) { if (PropertyChanged != null) PropertyChanged(this, e); }
        #endregion

        #region Head
        private readonly NotifyPropertyChangedInvoker invoker;
        private OutputLogViewModel viewModel;
        private ScrollViewer scroller;
        internal readonly SynchronizationContext SyncContext;
        private bool previousIsEmpty = true;

        /// <summary>Constructor.</summary>
        public OutputLog()
        {
            // Setup initial conditions.
            InitializeComponent();
            SyncContext = SynchronizationContext.Current;
            invoker = new NotifyPropertyChangedInvoker(SyncContext, FirePropertyChangedEvent);

            // Create a default writer.
            Writer = new OutputWriter();
        }
        #endregion

        #region Event Handlers
        private void OnLinesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Fire events.
            if (previousIsEmpty != IsEmpty) invoker.OnPropertyChanged(LinqExtensions.GetPropertyName<T>(m => m.IsEmpty));
            invoker.OnPropertyChanged(LinqExtensions.GetPropertyName<T>(m => m.Count));

            // Finish up.
            previousIsEmpty = IsEmpty;
        }
        #endregion

        #region Properties
        /// <summary>Gets the number of items in the log.</summary>
        public int Count { get { return viewModel == null ? 0 : viewModel.Lines.Count; } }

        /// <summary>Gets whether the log is empty.</summary>
        public bool IsEmpty { get { return Count == 0; } }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the writer that is feeding this log.</summary>
        public IOutput Writer
        {
            get { return (IOutput) (GetValue(WriterProperty)); }
            set { SetValue(WriterProperty, value); }
        }
        /// <summary>Gets or sets the writer that is feeding this log.</summary>
        public static readonly DependencyProperty WriterProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.Writer),
                typeof (IOutput),
                typeof (T),
                new PropertyMetadata(null, (s, e) => ((T)s).OnWriterChanged()));
        private void OnWriterChanged()
        {
            if (viewModel != null) Dispose(viewModel);
            if (Writer != null) viewModel = CreateViewModel();
            root.DataContext = viewModel;
            root.Visibility = viewModel == null ? Visibility.Collapsed : Visibility.Visible;
        }


        /// <summary>Gets or sets whether the toolbar is visible.</summary>
        public bool IsToolbarVisible
        {
            get { return (bool) (GetValue(IsToolbarVisibleProperty)); }
            set { SetValue(IsToolbarVisibleProperty, value); }
        }
        /// <summary>Gets or sets whether the toolbar is visible.</summary>
        public static readonly DependencyProperty IsToolbarVisibleProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.IsToolbarVisible),
                typeof (bool),
                typeof (T),
                new PropertyMetadata(true));


        /// <summary>Gets or sets whether the control is writing items to the log.</summary>
        public bool IsActive
        {
            get { return (bool) (GetValue(IsActiveProperty)); }
            set { SetValue(IsActiveProperty, value); }
        }
        /// <summary>Gets or sets whether the control is writing items to the log.</summary>
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.IsActive),
                typeof (bool),
                typeof (T),
                new PropertyMetadata(true));


        /// <summary>Gets or sets whether each line's time stamp is shown.</summary>
        public bool ShowTimeStamp
        {
            get { return (bool) (GetValue(ShowTimeStampProperty)); }
            set { SetValue(ShowTimeStampProperty, value); }
        }
        /// <summary>Gets or sets whether each line's time stamp is shown.</summary>
        public static readonly DependencyProperty ShowTimeStampProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.ShowTimeStamp),
                typeof (bool),
                typeof (T),
                new PropertyMetadata(false, (s, e) => ((T) s).OnShowTimeStampChanged()));
        private void OnShowTimeStampChanged()
        {
            if (viewModel != null) viewModel.OnShowTimeStampChanged();
        }
        #endregion

        #region Properties - Internal
        internal ScrollViewer Scroller
        {
            get
            {
                // Setup initial conditions.
                if (scroller != null) return scroller;

                // Retrieve an initialize scroller on first call.
                scroller = listBox.FindFirstChildOfType<ScrollViewer>();
                if (scroller != null)
                {
                    scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                }

                // Finish up.
                return scroller;
            }
        }
        #endregion

        #region Methods
        /// <summary>Resets the total lines count to zero.</summary>
        public void ResetLineCount()
        {
            if (viewModel != null) viewModel.Count = 0;
        }
        #endregion

        #region Internal
        private OutputLogViewModel CreateViewModel()
        {
            previousIsEmpty = true;
            viewModel = new OutputLogViewModel(Writer, this);
            viewModel.Lines.CollectionChanged += OnLinesCollectionChanged;
            return viewModel;
        }

        private void Dispose(OutputLogViewModel model)
        {
            model.Dispose();
            model.Lines.CollectionChanged -= OnLinesCollectionChanged;
        }
        #endregion
    }
}
