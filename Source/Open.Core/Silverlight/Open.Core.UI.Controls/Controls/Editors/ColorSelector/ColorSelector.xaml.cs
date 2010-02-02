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

namespace Open.Core.Common.Controls.Editors
{
    /// <summary>An RGB color selector.</summary>
    public partial class ColorSelector : UserControl
    {
        #region Head
        /// <summary>Fires when the selected color changes.</summary>
        public event EventHandler ColorChanged;
        protected void OnColorChanged(){if (ColorChanged != null) ColorChanged(this, new EventArgs());}

        public const string PropColor = "Color";
        private readonly ColorSelectorViewModel viewModel = new ColorSelectorViewModel();

        private bool updatingColorProperty;

        public ColorSelector()
        {
            InitializeComponent();
            root.DataContext = viewModel;

            // Wire up events.
            viewModel.PropertyChanged += (sender, e) =>
                                             {
                                                 if (e.PropertyName == ColorSelectorViewModel.PropColor) HandleViewModelColorChanged();
                                             };
        }
        #endregion

        #region Event Handlers
        private void HandleViewModelColorChanged()
        {
            updatingColorProperty = true;
            Color = viewModel.Color;
            OnColorChanged();
            updatingColorProperty = false;
        }

        private void HandleColorPropertyChanged()
        {
            if (updatingColorProperty) return;
            viewModel.Color = Color;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the selected color.</summary>
        public Color Color
        {
            get { return (Color) (GetValue(ColorProperty)); }
            set { SetValue(ColorProperty, value); }
        }
        /// <summary>Gets or sets the selected color.</summary>
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                PropColor,
                typeof (Color),
                typeof (ColorSelector),
                new PropertyMetadata(Colors.Red, (sender, e) => ((ColorSelector)sender).HandleColorPropertyChanged()));
        #endregion
    }
}
