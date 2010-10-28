using System;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with Exceptions.</summary>
    public class ExceptionHelper
    {
        /// <summary>Produces an exception for a value that is not supported.</summary>
        /// <param name="value">The value that is not supported.</param>
        public Exception NotSupported(object value)
        {
            return new Exception(string.Format("Not supported [{0}].", Helper.String.FormatToString(value)));
        }
    }
}
