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
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using Open.Core.Common;
using T = Open.Core.UI.Controls.HtmlDocument;

namespace Open.Core.UI.Controls
{
    /// <summary>Embeds an HTML page.</summary>
    public partial class HtmlDocument : UserControl, INotifyDisposed
    {
        #region Head
        private const string HtmlIframe = @"<iframe src='{0}' width='100%' height='100%' scrolling='auto' frameborder='0' />";

        public HtmlDocument()
        {
            InitializeComponent();
        }
        #endregion

        #region Dispose | Finalize
        ~HtmlDocument()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            // Setup initial conditions.
            if (IsDisposed) return;

            // Perform disposal or managed resources.
            if (isDisposing)
            {
                // Dispose of managed resources.
                htmlBlock.Dispose();

                // Alert listeners.
                OnDisposed();
            }

            // Finish up.
            IsDisposed = true;
        }

        /// <summary>Gets whether the object has been disposed.</summary>
        public bool IsDisposed { get; private set; }

        /// <summary>Fires when the object has been disposed of (via the 'Dispose' method.  See 'IDisposable' interface).</summary>
        public event EventHandler Disposed;
        private void OnDisposed() { if (Disposed != null) Disposed(this, new EventArgs()); }
        #endregion

        #region Properties
        /// <summary>Gets the IFrame HTML element.</summary>
        // ReSharper disable InconsistentNaming
        public HtmlElement IFrame { get { return htmlBlock.HtmlElement; } }
        // ReSharper restore InconsistentNaming
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the URL of the page.</summary>
        public Uri SourceUri
        {
            get { return (Uri)(GetValue(SourceUriProperty)); }
            set { SetValue(SourceUriProperty, value); }
        }
        /// <summary>Gets or sets the URL of the page.</summary>
        public static readonly DependencyProperty SourceUriProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.SourceUri),
                typeof(Uri),
                typeof(T),
                new PropertyMetadata(null, (s, e) => ((T)s).SetSource()));

        /// <summary>
        ///     Gets or sets the pixel offset to apply when calculating the position of the 
        ///     HTML (used when the SL app is not filling the entire window).
        /// </summary>
        public Point Offset
        {
            get { return (Point)(GetValue(OffsetProperty)); }
            set { SetValue(OffsetProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the pixel offset to apply when calculating the position of the 
        ///     HTML (used when the SL app is not filling the entire window).
        /// </summary>
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.Offset),
                typeof(Point),
                typeof(T),
                new PropertyMetadata(default(Point), (s, e) => ((T)s).SetOffset()));
        #endregion

        #region Internal
        private void SetSource()
        {
            var html = SourceUri == null ? null : string.Format(HtmlIframe, SourceUri);
            htmlBlock.InnerHtml = html;
        }

        private void SetOffset()
        {
            htmlBlock.Offset = Offset;
        }
        #endregion
    }
}
