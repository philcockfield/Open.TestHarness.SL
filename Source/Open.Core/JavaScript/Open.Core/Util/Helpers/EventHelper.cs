using System;
using System.Collections;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with events.</summary>
    public class EventHelper
    {
        /// <summary>Fires the click event from the source object (if it exposes a parameterless 'FireClick' method).</summary>
        /// <param name="source">The source object to fire the event.</param>
        /// <returns>True if the event was fired, or False if the source object did not expose a Public or Internal 'FireClick' method.</returns>
        public bool FireClick(object source)
        {
            Log.Title("FIRE CLICK"); //TEMP 

            // Setup initial conditions.
            Dictionary obj = source as Dictionary;
            if (obj == null) return false;

            // Determine that the object has a 'FireClick' method.
            Function func = Helper.Reflection.GetFunction(source, "FireClick");
            if (func == null) return false;

            // Fire the event by invoking the method.
            func.Call(source);


            //TEMP 
            Log.Debug("F: " + func);
            Log.LineBreak();

            // Finish up.
            return true;
        }
    }
}
