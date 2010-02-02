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
using Open.Core.Common;
using Open.Core.UI.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>Flags representing the various colors of a StatusIcon.</summary>
    public enum StatusIconColor
    {
        Green,
        Orange,
        Red,
        Grey
    }

    /// <summary>A red/green/orange/grey icon that can be used to indicate the health status of something.</summary>
    public partial class StatusIcon : UserControl
    {
        #region Head
        public const string PropColor = "Color";
        public const string PropIsFlashing = "IsFlashing";
        public const string PropFlashDuration = "FlashDuration";

        private const string UrlStatusIconGrey = "/Images/Icon.Status.16x17.Grey.png";
        private const string UrlStatusIconGreen = "/Images/Icon.Status.16x17.Green.png";
        private const string UrlStatusIconOrange = "/Images/Icon.Status.16x17.Orange.png";
        private const string UrlStatusIconRed = "/Images/Icon.Status.16x17.Red.png";

        public StatusIcon()
        {
            // Setup initial conditions.
            InitializeComponent();

            // Wire up events.
            IsEnabledChanged += delegate { UpdateVisualState(); };

            // Finish up.
            UpdateVisualState();
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the color of the icon.</summary>
        public StatusIconColor Color
        {
            get { return (StatusIconColor) (GetValue(ColorProperty)); }
            set { SetValue(ColorProperty, value); }
        }
        /// <summary>Gets or sets the color of the icon.</summary>
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                PropColor,
                typeof (StatusIconColor),
                typeof (StatusIcon),
                new PropertyMetadata(StatusIconColor.Green, (s, e) => ((StatusIcon)s).UpdateIconColor()));

        
        /// <summary>Gets or sets whether the icon is pulsing in and out.</summary>
        public bool IsFlashing
        {
            get { return (bool) (GetValue(IsFlashingProperty)); }
            set { SetValue(IsFlashingProperty, value); }
        }
        /// <summary>Gets or sets whether the icon is pulsing in and out.</summary>
        public static readonly DependencyProperty IsFlashingProperty =
            DependencyProperty.Register(
                PropIsFlashing,
                typeof (bool),
                typeof (StatusIcon),
                new PropertyMetadata(false, (s, e) => ((StatusIcon)s).OnIsFlashingChanged()));
        private void OnIsFlashingChanged()
        {
            if (IsFlashing) Pulse();
        }


        /// <summary>Gets or sets the duration (in seconds) that a single flash takes (pulse in and out).</summary>
        public double FlashDuration
        {
            get { return (double) (GetValue(FlashDurationProperty)); }
            set { SetValue(FlashDurationProperty, value); }
        }
        /// <summary>Gets or sets the duration (in seconds) that a single flash takes (pulse in and out).</summary>
        public static readonly DependencyProperty FlashDurationProperty =
            DependencyProperty.Register(
                PropFlashDuration,
                typeof (double),
                typeof (StatusIcon),
                new PropertyMetadata(1.5));
        
        #endregion

        #region Properties - Internal
        private double PulseDuration { get { return FlashDuration*0.5; } }
        #endregion

        #region Methods
        /// <summary>Updates the visual state of the control.</summary>
        public void UpdateVisualState()
        {
            root.Opacity = IsEnabled ? 1 : 0.4;
            UpdateIconColor();
            UpdateBackgroundIconVisibility();
        }
        #endregion

        #region Internal
        private void Pulse()
        {
            if (!IsFlashing && icon.Opacity == 1) return;

            if (icon.Opacity == 1) AnimationUtil.FadeOut(icon, PulseDuration, null, Pulse);
            if (icon.Opacity == 0) AnimationUtil.FadeIn(icon, PulseDuration, null, Pulse);

            UpdateBackgroundIconVisibility();
        }

        private void UpdateBackgroundIconVisibility()
        {
            backgroundIcon.Visibility = IsFlashing ? Visibility.Visible : Visibility.Collapsed;
        }

        private void UpdateIconColor()
        {
            icon.Source = IconUrl(Color).ToImageSource();
        }

        private static string IconUrl(StatusIconColor color)
        {
            switch (color)
            {
                case StatusIconColor.Green: return UrlStatusIconGreen;
                case StatusIconColor.Orange: return UrlStatusIconOrange;
                case StatusIconColor.Red: return UrlStatusIconRed;
                case StatusIconColor.Grey: return UrlStatusIconGrey;

                default: throw new NotSupportedException(color.ToString());
            }
        }
        #endregion
    }
}
