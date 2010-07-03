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
using System.Windows.Media;
using Open.Core.Common;
using T = Open.Core.UI.Controls.BackgroundModel;

namespace Open.Core.UI.Controls
{
    [Export(typeof(IBackground))]
    public class BackgroundModel : ModelBase, IBackground
    {
        #region Head
        private static readonly Brush transparent = new SolidColorBrush(Colors.Transparent);
        private IBorder defaultBorder;
        private IBorder border;
        private bool ignoreCornerRadiusChanged;

        private PropertyObserver<IBorder> defaultBorderObserver;
        private PropertyObserver<IBorder> customBorderObserver;
        #endregion

        #region Event Handlers
        private void OnBorderCornerRadiusChanged()
        {
            if (ignoreCornerRadiusChanged) return;
            FireCornerRadiusChanged();
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            if (defaultBorderObserver != null) defaultBorderObserver.Dispose();
            if (customBorderObserver != null) customBorderObserver.Dispose();
        }
        #endregion

        #region Properties : IBackground
        public double Opacity
        {
            get { return GetPropertyValue<T, double>(m => m.Opacity, 1); }
            set { SetPropertyValue<T, double>(m => m.Opacity, value, 1); }
        }

        public bool IsVisible
        {
            get { return GetPropertyValue<BorderModel, bool>(m => m.IsVisible, true); }
            set { SetPropertyValue<BorderModel, bool>(m => m.IsVisible, value, true, m => m.Visibility); }
        }

        public Brush Color
        {
            get { return GetPropertyValue<T, Brush>(m => m.Color, transparent); }
            set { SetPropertyValue<T, Brush>(m => m.Color, value, transparent); }
        }

        public IBorder Border
        {
            get { return border ?? GetDefaultBorder(); }
            set
            {
                // Setup initial conditions.
                if (value == Border) return;

                // Wire up events.
                if (value != null && value != GetDefaultBorder())
                {
                    if (customBorderObserver != null) customBorderObserver.Dispose();
                    customBorderObserver = new PropertyObserver<IBorder>(value)
                        .RegisterHandler(m => m.CornerRadius, m => OnBorderCornerRadiusChanged());
                }

                // Store value.
                border = value;

                // Finish up.
                OnPropertyChanged<T>(m => m.Border);
            }
        }

        public CornerRadius CornerRadius
        {
            get { return Border.CornerRadius; }
            set 
            {
                // Setup initial conditions.
                if (value == CornerRadius) return;
                ignoreCornerRadiusChanged = true;

                // Store value.
                Border.CornerRadius = value;

                // Finish up.
                FireCornerRadiusChanged();
                ignoreCornerRadiusChanged = false;
            }
        }

        public DataTemplate Template
        {
            get { return GetPropertyValue<T, DataTemplate>(m => m.Template); }
            set { SetPropertyValue<T, DataTemplate>(m => m.Template, value); }
        }
        #endregion

        #region Properties : ViewModel
        public Visibility Visibility { get { return IsVisible ? Visibility.Visible : Visibility.Collapsed; } }
        #endregion

        #region Methods
        public void SetColor(Color color)
        {
            Color = new SolidColorBrush(color);
        }

        public FrameworkElement CreateView()
        {
            return new Background {DataContext = this};
        }
        #endregion

        #region Internal
        private IBorder GetDefaultBorder()
        {
            // Setup initial conditions.
            if (defaultBorder != null) return defaultBorder;

            // Create and wire up events.
            defaultBorder = new BorderModel { Color = transparent };
            defaultBorderObserver = new PropertyObserver<IBorder>(defaultBorder)
                .RegisterHandler(m => m.CornerRadius, m => OnBorderCornerRadiusChanged());

            // Finish up.
            return defaultBorder;
        }

        private void FireCornerRadiusChanged()
        {
            OnPropertyChanged<T>(m => m.CornerRadius);
        }
        #endregion
    }
}
