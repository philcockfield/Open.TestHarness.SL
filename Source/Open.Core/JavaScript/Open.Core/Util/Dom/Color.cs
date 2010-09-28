using System;

namespace Open.Core
{
    /// <summary>Class for working with colors.</summary>
    public static class Color
    {
        #region Properties
        public const string HotPink = "#ff00f0";
        public const string Orange = "orange";
        #endregion

        #region Methods
        /// <summary>Gets an RGBA value of black at the given opacity.</summary>
        /// <param name="opacity">The opacity percentage (0..1).</param>
        public static string Black(double opacity)
        {
            return string.Format("rgba(0,0,0,{0})", Helper.NumberDouble.WithinBounds(opacity, 0, 1));
        }

        /// <summary>Gets an RGBA value of white at the given opacity.</summary>
        /// <param name="opacity">The opacity percentage (0..1).</param>
        public static string White(double opacity)
        {
            return string.Format("rgba(255,255,255,{0})", Helper.NumberDouble.WithinBounds(opacity, 0, 1));
        }
        #endregion
    }
}
