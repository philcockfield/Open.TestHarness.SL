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
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Diagnostics;
using Open.Core.Common;

using T = Open.Core.UI.Controls.HtmlBlock;

namespace Open.Core.UI.Controls
{
    /// <summary>Flags representing the various values of the CSS 'Overflow' property.</summary>
    public enum CssOverflow
    {
        /// <summary>The overflow is not clipped. It renders outside the element's box.</summary>
        Visible,

        /// <summary>The overflow is clipped, and the rest of the content will be invisible.</summary>
        Hidden,

        /// <summary>The overflow is clipped, but a scroll-bar is added to see the rest of the content.</summary>
        Scroll,

        /// <summary>If overflow is clipped, a scroll-bar should be added to see the rest of the content.</summary>
        Auto,

        /// <summary>Specifies that the value of the overflow property should be inherited from the parent element.</summary>
        Inherit
    }

    /// <summary>Renders a block of HTML content.</summary>
    /// <remarks>
    ///    This control achieves HTML rendering by positioning a DIV element within the host browser at 
    ///    the position of the control.  
    /// </remarks>
    public partial class HtmlBlock : UserControl, INotifyDisposed
    {
        #region Head
        private HtmlElement htmlElement;
        private readonly DelayedAction updateDelay;

        public HtmlBlock()
        {
            // Setup initial conditions.
            InitializeComponent();
            HtmlElementId = Guid.NewGuid().ToString();

            // Create update delay.
            // NB: This is put on a delay (of zero, which causes it to be invoked asynchronously)
            // because Google Chrome crashes otherwise.  Too many calls, to quickly to the JavaScript engine.
            updateDelay = new DelayedAction(0, UpdateDimensions);

            // Wire up events.
            Loaded += OnLoaded;
        }
        #endregion

        #region Dispose | Finalize
        ~HtmlBlock()
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
                Loaded -= OnLoaded;
                RemoveElement();
            }

            // Finish up.
            IsDisposed = true;
            OnDisposed();
        }

        /// <summary>Gets whether the object has been disposed.</summary>
        public bool IsDisposed { get; private set; }

        /// <summary>Fires when the object has been disposed of (via the 'Dispose' method.  See 'IDisposable' interface).</summary>
        public event EventHandler Disposed;
        private void OnDisposed(){if (Disposed != null) Disposed(this, new EventArgs());}
        #endregion

        #region Event Handlers
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateDimensions();
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            updateDelay.Start();
        }

        private void OnSourceUriChanged()
        {
            Load(SourceUri);
        }
        #endregion

        #region Properties
        /// <summary>Gets the absolute position of the control relative to the visual root (or null if it's not within the tree).</summary>
        public Point? AbsolutePosition
        {
            get
            {
                var position = this.GetAbsolutePosition();
                return position == null 
                                ? (Point?)null 
                                : new Point(position.Value.X + Offset.X, position.Value.Y + Offset.Y);
            }
        }

        /// <summary>Gets the unique identifier of the embedded HTML element.</summary>
        public string HtmlElementId { get; private set; }

        /// <summary>Gets the HTML element.</summary>
        public HtmlElement HtmlElement
        {
            get
            {
                if (htmlElement == null) InsertElement();
                return htmlElement;
            }
        }
        #endregion

        #region Properties - Private
        /// <summary>Gets whether the corresponding HTML element has been inserted into the DOM.</summary>
        private bool IsInserted { get { return htmlElement != null; } }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the HTML content of the DIV element.</summary>
        public string InnerHtml
        {
            get { return (string)(GetValue(InnerHtmlProperty)); }
            set { SetValue(InnerHtmlProperty, value); }
        }
        /// <summary>Gets or sets the HTML content of the DIV element.</summary>
        public static readonly DependencyProperty InnerHtmlProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.InnerHtml),
                typeof(string),
                typeof(HtmlBlock),
                new PropertyMetadata(null, (s, e) => ((T)s).UpdateInnerHtml()));


        /// <summary>Gets or sets the CSS style to apply to as the background for the DIV element.</summary>
        public string BackgroundStyle
        {
            get { return (string) (GetValue(BackgroundStyleProperty)); }
            set { SetValue(BackgroundStyleProperty, value); }
        }
        /// <summary>Gets or sets the CSS style to apply to as the background for the DIV element.</summary>
        public static readonly DependencyProperty BackgroundStyleProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.BackgroundStyle),
                typeof(string),
                typeof (HtmlBlock),
                new PropertyMetadata(null, (s, e) => ((T) s).UpdateStyle()));
        

        /// <summary>Gets or sets the overflow style to apply to the DIV element.</summary>
        public CssOverflow OverflowStyle
        {
            get { return (CssOverflow) (GetValue(OverflowStyleProperty)); }
            set { SetValue(OverflowStyleProperty, value); }
        }
        /// <summary>Gets or sets the overflow style to apply to the DIV element.</summary>
        public static readonly DependencyProperty OverflowStyleProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.OverflowStyle),
                typeof (CssOverflow),
                typeof (HtmlBlock),
                new PropertyMetadata(CssOverflow.Auto, (s, e) => ((T)s).UpdateStyle()));


        /// <summary>Gets or sets the source of a file containing the HTML to load.</summary>
        /// <remarks>Setting this property causes the InnerHTML property to be updated.</remarks>
        public Uri SourceUri
        {
            get { return (Uri) (GetValue(SourceUriProperty)); }
            set { SetValue(SourceUriProperty, value); }
        }
        /// <summary>Gets or sets the source of a file containing the HTML to load.</summary>
        public static readonly DependencyProperty SourceUriProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.SourceUri),
                typeof (Uri),
                typeof (T),
                new PropertyMetadata(null, (s, e) => ((T) s).OnSourceUriChanged()));


        /// <summary>
        ///     Gets or sets the pixel offset to apply when calculating the position of the 
        ///     HTML (used when the SL app is not filling the entire window).
        /// </summary>
        public Point Offset
        {
            get { return (Point) (GetValue(OffsetProperty)); }
            set { SetValue(OffsetProperty, value); }
        }
        /// <summary>
        ///     Gets or sets the pixel offset to apply when calculating the position of the 
        ///     HTML (used when the SL app is not filling the entire window).
        /// </summary>
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.Offset),
                typeof (Point),
                typeof (T),
                new PropertyMetadata(default(Point), (s, e) => ((T) s).UpdateDimensions()));
        #endregion

        #region Methods
        /// <summary>Loads the HTML contained in the file at the given location.</summary>
        /// <param name="source">The source file containing the HTML.</param>
        public void Load(Uri source)
        {
            // Setup initial conditions.
            if (source == null) return;

            // Load the HTML content.
            var webClient = new WebClient();
            webClient.DownloadStringCompleted += (s, e) =>
                        {
                            if (e.Error != null)
                            {
                                Debug.WriteLine(string.Format("HtmlBlock Load Error: Failed to load HTML contained in an XML document at '{0}'.", source));
                                Debug.WriteLine(e.Error.Message);
                                return;
                            }
                            InnerHtml = e.Result;
                        };
            webClient.DownloadStringAsync(source);
        }
        #endregion

        #region Internal
        private void InsertElement()
        {
            // Create the DIV element.
            var doc = HtmlPage.Document;
            htmlElement = HtmlPage.Document.CreateElement("DIV");

            // Apply attributes.
            htmlElement.SetAttribute("id", HtmlElementId);
            htmlElement.SetStyleAttribute("position", "absolute");
            UpdateInnerHtml();
            UpdateStyle();

            // Wire up events.
            LayoutUpdated += OnLayoutUpdated;

            // Insert into page.
            doc.Body.AppendChild(htmlElement);
        }

        private void RemoveElement()
        {
            if (!IsInserted) return;
            HtmlPage.Document.Body.RemoveChild(HtmlElement);
            htmlElement = null;
            LayoutUpdated -= OnLayoutUpdated;
        }
        #endregion

        #region Internal - Updates
        private void UpdateDimensions()
        {
            // Setup initial conditions.
            var position = GetPosition();
            if (position == null)
            {
                RemoveElement();
                return;
            }

            // Update position and size.
            var div = HtmlElement;
            div.SetAbsolutePosition(position.Value.X, position.Value.Y);
            div.SetSize(ActualWidth, ActualHeight);
        }

        private void UpdateStyle()
        {
            if (htmlElement == null) return;
            htmlElement.SetStyleAttribute("background", BackgroundStyle);
            htmlElement.SetStyleAttribute("overflow", OverflowStyle.ToString().ToLower());
        }

        private void UpdateInnerHtml()
        {
            if (htmlElement ==null) return;
            htmlElement.SetInnerHtml(InnerHtml);
        }

        private Point? GetPosition()
        {
            return this.IsVisibleToRoot()
                                ? AbsolutePosition
                                : new Point(-50000, -50000); // Off screen if control is not visible.
        }
        #endregion
    }
}
