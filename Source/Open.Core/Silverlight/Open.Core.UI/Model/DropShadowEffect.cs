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
using System.Windows.Media;
using Open.Core.Common;

using T = Open.Core.UI.Model.DropShadowEffect;

namespace Open.Core.UI.Model
{
    [Export(typeof(IDropShadowEffect))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DropShadowEffect : NotifyPropertyChangedBase, IDropShadowEffect
    {
        #region Head
        private const double defaultBlurRadius = 5;
        private static readonly Color defaultColor = Colors.Black;
        private const double defaultDirection = 315;
        private const double defaultOpacity = 0.3;
        private const double defaultShadowDepth = 5;
        #endregion

        #region Properties
        public double BlurRadius
        {
            get { return GetPropertyValue<T, double>(m => m.BlurRadius, defaultBlurRadius); }
            set { SetPropertyValue<T, double>(m => m.BlurRadius, value, defaultBlurRadius); }
        }

        public Color Color
        {
            get { return GetPropertyValue<T, Color>(m => m.Color, defaultColor); }
            set { SetPropertyValue<T, Color>(m => m.Color, value, defaultColor); }
        }

        public double Direction
        {
            get { return GetPropertyValue<T, double>(m => m.Direction, defaultDirection); }
            set { SetPropertyValue<T, double>(m => m.Direction, value, defaultDirection); }
        }

        public double Opacity
        {
            get { return GetPropertyValue<T, double>(m => m.Opacity, defaultOpacity); }
            set { SetPropertyValue<T, double>(m => m.Opacity, value.WithinBounds(0, 1), defaultOpacity); }
        }

        public double ShadowDepth
        {
            get { return GetPropertyValue<T, double>(m => m.ShadowDepth, defaultShadowDepth); }
            set { SetPropertyValue<T, double>(m => m.ShadowDepth, value, defaultShadowDepth); }
        }
        #endregion

        #region Methods
        public void CopyTo(System.Windows.Media.Effects.DropShadowEffect effect)
        {
            if (effect == null) return;
            effect.BlurRadius = BlurRadius;
            effect.Color = Color;
            effect.Direction = Direction;
            effect.Opacity = Opacity;
            effect.ShadowDepth = ShadowDepth;
        }
        #endregion
    }
}
