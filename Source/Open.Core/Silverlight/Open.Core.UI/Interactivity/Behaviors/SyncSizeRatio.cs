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
using System.Windows.Interactivity;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Flags representing the various size propertys on a framework element.</summary>
    public enum SizeProperty
    {
        Width,
        Height,
        MinWidth,
        MinHeight,
        MaxWidth,
        MaxHeight
    }

    /// <summary>Syncs an elements size to a percentage of another element's size.</summary>
    public class SyncSizeRatio : Behavior<FrameworkElement>
    {
        #region Head
        private bool isLoaded;
        private FrameworkElement source;
        private DelayedAction eventDelay;

        public SyncSizeRatio()
        {
            Percentage = 1; // Default percentage.
        }
        #endregion

        #region Event Handlers
        private void Handle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (eventDelay == null ) eventDelay = new DelayedAction(0.1, SyncSize);
            eventDelay.Start();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the name of the source element to read size data from.</summary>
        public string SourceElement { get; set; }

        /// <summary>Gets or sets the property on the 'SourceElement' to read size data from.</summary>
        public SizeProperty SourceProperty { get; set; }

        /// <summary>Gets or sets the property on the attached element to write size data to.</summary>
        public SizeProperty TargetProperty { get; set; }

        /// <summary>Gets or sets the percentage to alter the source size by when applying it to the target (1 = 100%).</summary>
        public double Percentage { get; set; }
        #endregion

        #region Properties - Private
        private FrameworkElement Source
        {
            get
            {
                if (source == null) source = AssociatedObject.FindName(SourceElement) as FrameworkElement;
                return source;
            }
        }
        #endregion

        #region Methods - Override
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += delegate
                                           {
                                               if (!isLoaded) Source.SizeChanged += Handle_SizeChanged;
                                               isLoaded = true;
                                               SyncSize();
                                           };
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SizeChanged -= Handle_SizeChanged;
            if (eventDelay != null) eventDelay.Stop();
            base.OnDetaching();
        }
        #endregion

        #region Internal
        private void SyncSize()
        {
            // Setup initial conditions.
            if (! isLoaded || SourceElement == null) return;
            var percentage = Percentage < 0 ? 0 : Percentage;
            
            // Calculate the value.
            var value = GetValue(Source, SourceProperty);
            value = value * percentage;

            // Assign it to the target.
            SetValue(AssociatedObject, TargetProperty, value);
        }

        private static double GetValue(FrameworkElement element, SizeProperty property)
        {
            if (element == null) return default(double);
            switch (property)
            {
                case SizeProperty.Width: return element.ActualWidth;
                case SizeProperty.Height: return element.ActualHeight;
                case SizeProperty.MinWidth: return element.MinWidth;
                case SizeProperty.MinHeight: return element.MinHeight;
                case SizeProperty.MaxWidth: return element.MaxWidth;
                case SizeProperty.MaxHeight: return element.MaxHeight;

                default: throw new ArgumentOutOfRangeException(property.ToString());
            }
        }

        private static void SetValue(FrameworkElement element, SizeProperty property, double value)
        {
            if (element == null) return;
            switch (property)
            {
                case SizeProperty.Width: element.Width = value; break;
                case SizeProperty.Height: element.Height = value; break;
                case SizeProperty.MinWidth: element.MinWidth = value; break;
                case SizeProperty.MinHeight: element.MinHeight = value; break;
                case SizeProperty.MaxWidth: element.MaxWidth = value; break;
                case SizeProperty.MaxHeight: element.MaxHeight = value; break;

                default: throw new ArgumentOutOfRangeException(property.ToString());
            }
        }
        #endregion
    }
}
