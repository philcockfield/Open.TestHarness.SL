using System;
using System.Runtime.CompilerServices;

namespace Open.Core
{
    /// <summary>Class for working with colors.</summary>
    public static class Color
    {
        #region Methods
        /// <summary>Gets an RGBA value of black.</summary>
        [AlternateSignature]
        public static extern string Black();
        /// <summary>Gets an RGBA value of black at the given opacity.</summary>
        /// <param name="opacity">The opacity percentage (0..1).</param>
        public static string Black(double opacity) { return ToColor(0, 0, 0, opacity); }


        /// <summary>Gets an RGBA value of white.</summary>
        [AlternateSignature]
        public static extern string White();
        /// <summary>Gets an RGBA value of white at the given opacity.</summary>
        /// <param name="opacity">The opacity percentage (0..1).</param>
        public static string White(double opacity) { return ToColor(255, 255, 255, opacity); }


        /// <summary>Gets an RGBA value of red.</summary>
        [AlternateSignature]
        extern public static string Red();
        /// <summary>Gets an RGBA value of red at the given opacity.</summary>
        /// <param name="opacity">The opacity percentage (0..1).</param>
        public static string Red(double opacity) { return ToColor(255, 0, 0, opacity); }


        /// <summary>Gets an RGBA value of green.</summary>
        [AlternateSignature]
        extern public static string Green();
        /// <summary>Gets an RGBA value of green at the given opacity.</summary>
        /// <param name="opacity">The opacity percentage (0..1).</param>
        public static string Green(double opacity) { return ToColor(106, 215, 0, opacity); }


        /// <summary>Gets an RGBA value of orange.</summary>
        [AlternateSignature]
        extern public static string Orange();
        /// <summary>Gets an RGBA value of orange at the given opacity.</summary>
        /// <param name="opacity">The opacity percentage (0..1).</param>
        public static string Orange(double opacity) { return ToColor(255, 168, 0, opacity); }

        /// <summary>Gets an RGBA value of hot-pink (magenta).</summary>
        [AlternateSignature]
        extern public static string HotPink();
        /// <summary>Gets an RGBA value of hot-pink (magenta) at the given opacity.</summary>
        /// <param name="opacity">The opacity percentage (0..1).</param>
        public static string HotPink(double opacity) { return ToColor(255, 0, 216, opacity); }
        #endregion

        #region Internal
        private static string ToColor(int r, int g, int b, double percent)
        {
            if (Script.IsNullOrUndefined(percent)) percent = 1;
            percent = Helper.NumberDouble.WithinBounds(percent, 0, 1);
            return string.Format("rgba({0},{1},{2},{3})", r, g, b, percent);
        }
        #endregion
    }
}
