using System;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with delegates.</summary>
    public class DelegateHelper
    {
        /// <summary>Formats a callback function to a JavaScript function name.</summary>
        /// <param name="callback">The callback delegate.</param>
        public string ToCallbackString(Delegate callback)
        {
            return "ss.Delegate." + Delegate.CreateExport(callback, true);
        }


        /// <summary>Formats a callback function with the specified event identifier.</summary>
        /// <param name="callback">The callback delegate.</param>
        /// <param name="eventIdentifier">The event identifier.</param>
        public string ToEventCallbackString(EventCallback callback, string eventIdentifier)
        {
            string func = String.Format("{0}('{1}');",
                                        ToCallbackString(callback),
                                        eventIdentifier);
            return "function(e,ui){ " + func + " }";
        }
    }
}
