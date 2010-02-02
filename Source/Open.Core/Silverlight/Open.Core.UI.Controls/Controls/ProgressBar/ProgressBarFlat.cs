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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;
using T = Open.Core.UI.Controls.Controls.ProgressBar.ProgressBarFlat;

namespace Open.Core.UI.Controls.Controls.ProgressBar
{
    /// <summary>Represents a simple flat progress bar.</summary>
    public class ProgressBarFlat : ContentControl
    {
        #region Head
        private Border progressIndicator;
        private bool isInitialized;

        public ProgressBarFlat()
        {
            // Setup default values.
            Height = 10;
            Background = new SolidColorBrush(Colors.Transparent);
            BorderThickness = new Thickness(2);

            // Apply the template.
            Templates.Instance.ApplyTemplate(this);

            // Wire up events.
            SizeChanged += delegate { UpdateIndicatorWidth(); };
        }

        public override void OnApplyTemplate()
        {
            // Setup initial conditions.
            base.OnApplyTemplate();

            // Retrieve elements.
            progressIndicator = GetTemplateChild("progressIndicator") as Border;
            if (progressIndicator == null) throw new TemplateNotSetException();

            // Finish up.
            isInitialized = true;
            UpdateIndicatorWidth();
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the color of the progress bar.</summary>
        public Brush Color
        {
            get { return (Brush) (GetValue(ColorProperty)); }
            set { SetValue(ColorProperty, value); }
        }
        /// <summary>Gets or sets the color of the progress bar.</summary>
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.Color),
                typeof (Brush),
                typeof (T),
                new PropertyMetadata(new SolidColorBrush(Colors.Black.ToAlpha(0.3))));


        /// <summary>Gets or sets the progress percentage.</summary>
        public double PercentComplete
        {
            get { return (double) (GetValue(PercentCompleteProperty)); }
            set { SetValue(PercentCompleteProperty, value); }
        }
        /// <summary>Gets or sets the progress percentage.</summary>
        public static readonly DependencyProperty PercentCompleteProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.PercentComplete),
                typeof (double),
                typeof (T),
                new PropertyMetadata(0.5d, (s, e) => ((T) s).UpdateIndicatorWidth()));
        #endregion

        #region Internal
        private void UpdateIndicatorWidth()
        {
            if (!isInitialized) return;
            var controlWidth = ActualWidth - (Padding.Left + Padding.Right);
            progressIndicator.Width = controlWidth*PercentComplete.WithinBounds(0, 1);
        }
        #endregion
    }
}
