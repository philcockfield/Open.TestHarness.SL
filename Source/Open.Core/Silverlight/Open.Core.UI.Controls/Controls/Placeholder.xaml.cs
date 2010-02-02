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
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>Generic placeholder control to use when constructing.</summary>
    public partial class Placeholder : UserControl, INotifyDisposed
    {
        #region Head
        public const string PropCornerRadius = "CornerRadius";
        public const string PropText = "Text";
        public const string PropColor = "Color";
        public const string PropShowInstanceCount = "ShowInstanceCount";

        private static int instanceCount;

        public Placeholder()
        {
            // Setup initial conditions.
            InitializeComponent();
            UpdateColors();

            instanceCount++;
            txtInstanceCount.Text = instanceCount.ToString();
            UpdateInstanceCountVisibility();

            root.DataContext = this;
        }
        #endregion

        #region Dispose | Finalize
        ~Placeholder()
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

        #region Dependency Properties
        /// <summary>Gets or sets the corner radius of the control.</summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius) (GetValue(CornerRadiusProperty)); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        /// <summary>Gets or sets the corner radius of the control.</summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(
                PropCornerRadius,
                typeof (CornerRadius),
                typeof (Placeholder),
                new PropertyMetadata(new CornerRadius(3)));


        /// <summary>Gets or sets the text that is displayed within the placeholder.</summary>
        public string Text
        {
            get { return (string) (GetValue(TextProperty)); }
            set { SetValue(TextProperty, value); }
        }
        /// <summary>Gets or sets the text that is displayed within the placeholder.</summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                PropText,
                typeof (string),
                typeof (Placeholder),
                new PropertyMetadata(null));
        

        /// <summary>Gets or sets the color of the placeholder.</summary>
        public Color Color
        {
            get { return (Color)(GetValue(ColorProperty)); }
            set { SetValue(ColorProperty, value); }
        }
        /// <summary>Gets or sets the color of the placeholder.</summary>
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                PropColor,
                typeof(Color),
                typeof (Placeholder),
                new PropertyMetadata(Colors.Red, (sender, e) => ((Placeholder)sender).UpdateColors()));


        /// <summary>Gets or sets whether a count of the instance is displayed (helpful for identifiying when items are recreated).</summary>
        public bool ShowInstanceCount
        {
            get { return (bool) (GetValue(ShowInstanceCountProperty)); }
            set { SetValue(ShowInstanceCountProperty, value); }
        }
        /// <summary>Gets or sets whether a count of the instance is displayed (helpful for identifiying when items are recreated).</summary>
        public static readonly DependencyProperty ShowInstanceCountProperty =
            DependencyProperty.Register(
                PropShowInstanceCount,
                typeof (bool),
                typeof (Placeholder),
                new PropertyMetadata(false, (sender, e) => ((Placeholder)sender).UpdateInstanceCountVisibility()));
        #endregion

        #region Methods
        public override string ToString()
        {
            var text = "";
            if (Text.AsNullWhenEmpty() != null) text = ": " + Text;
            return string.Format("{0}{1}", GetType().Name, text);
        }
        #endregion

        #region Internal
        private void UpdateColors()
        {
            var brush = new SolidColorBrush(Color);
            background.Background = brush;
            border.BorderBrush = brush;
        }

        private void UpdateInstanceCountVisibility()
        {
            txtInstanceCount.Visibility = ShowInstanceCount ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }
}
