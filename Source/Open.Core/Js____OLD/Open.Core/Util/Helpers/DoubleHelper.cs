namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with doubles.</summary>
    public class DoubleHelper
    {
        /// <summary>Ensures a value is within the given bounds.</summary>
        /// <param name="value">The number to examine.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The given value unchanged (if within bounds) otherwise the min or max value.</returns>
        public double WithinBounds(double value, double min, double max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
    }
}
