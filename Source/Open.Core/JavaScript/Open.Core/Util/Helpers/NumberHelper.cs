namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with numbers.</summary>
    public class NumberHelper
    {
        /// <summary>Converts seconds to milliseconds.</summary>
        /// <param name="secs">The value to convert.</param>
        public int ToMsecs(double secs)
        {
            return (int)(secs * 1000);
        }
    }
}
