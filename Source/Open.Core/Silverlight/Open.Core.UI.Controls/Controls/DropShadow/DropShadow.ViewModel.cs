using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Open.Core.Common;

using T = Open.Core.UI.Controls.DropShadowViewModel;

namespace Open.Core.UI.Controls
{
    /// <summary>A simple horizontal drop-shadow.</summary>
    [Export(typeof(IDropShadow))]
    public class DropShadowViewModel : ViewModelBase, IDropShadow
    {
        #region Head
        /// <summary>Constructor.</summary>
        public DropShadowViewModel()
        {
            // Set default values.
            Opacity = 0.15;
            Direction = Direction.Down;
            Size = 15;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the opacity (0-1).</summary>
        public double Opacity
        {
            get { return GetPropertyValue<T, double>(m => m.Opacity); }
            set
            {
                value = value.WithinBounds(0, 1);
                SetPropertyValue<T, double>(m => m.Opacity, value, m => m.Visibility, m => m.Fill);
            }
        }

        /// <summary>Gets or sets the color of the drop-shadow (Black by default).</summary>
        public Color Color
        {
            get { return GetPropertyValue<T, Color>(m => m.Color, Colors.Black); }
            set { SetPropertyValue<T, Color>(m => m.Color, value, Colors.Black, m => m.Visibility, m => m.Fill); }
        }

        /// <summary>Gets or sets the direction of the shadow.</summary>
        public Direction Direction
        {
            get { return GetPropertyValue<T, Direction>(m => m.Direction); }
            set { SetPropertyValue<T, Direction>(m => m.Direction, value, m => m.Fill, m => m.Width, m => m.Height); }
        }

        /// <summary>Gets or sets the pixel width or height of the shadow (depending on the 'Direction').</summary>
        public double Size
        {
            get { return GetPropertyValue<T, double>(m => m.Size); }
            set
            {
                if (value <= 0) value = double.NaN;
                SetPropertyValue<T, double>(m => m.Size, value, m => m.Visibility, m => m.Width, m => m.Height);
            }
        }

        /// <summary>Gets the translated Size as a Height (if on a vertical plane direction).</summary>
        public double Width
        {
            get { return Direction.IsHorizontal() ? Size : double.NaN; }
        }

        /// <summary>Gets the translated Size as a Width (if on a horizontal plane direction).</summary>
        public double Height
        {
            get { return Direction.IsVertical() ? Size : double.NaN; }
        }

        /// <summary>Gets the visibility of the dropshadow.</summary>
        public Visibility Visibility
        {
            get
            {
                var isVisible = !double.IsNaN(Size) && Opacity > 0 && Color != Colors.Transparent;
                return isVisible.ToVisibility();
            }
        }

        /// <summary>Gets the fill brush.</summary>
        public LinearGradientBrush Fill
        {
            get
            {
                var brush = Direction.ToGradient(Color.ToAlpha(1), Color.ToAlpha(0));
                brush.Opacity = Opacity;
                return brush;
            }
        }
        #endregion
    }
}
