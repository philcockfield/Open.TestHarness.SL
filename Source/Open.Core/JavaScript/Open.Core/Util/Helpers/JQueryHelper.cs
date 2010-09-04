using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with JQuery.</summary>
    public class JQueryHelper
    {
        /// <summary>Determines whether the specified code matches the given event.</summary>
        /// <param name="e">The jQuery event.</param>
        /// <param name="keyCode">The code to match.</param>
        public bool IsKey(jQueryEvent e, Key keyCode)
        {
            return (bool)Script.Literal("e.which == {0}", (int)keyCode);
        }
    }
}
