using System;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with Icons.</summary>
    public class IconHelper
    {
        /// <summary>Converts the named icon into a path.</summary>
        /// <param name="iconName">The name of the icon (see the IconImage enum on the server).</param>
        public string Path(string iconName)
        {
            iconName = Helper.String.RemoveEnd(iconName, ".png");
            if (iconName.StartsWith("Silk"))
            {
                return string.Format("/Open.Assets/Icons/Silk/{0}.png", iconName);
            }
            throw new Exception(string.Format("Icon named '{0}' not found.", iconName));
        }

        /// <summary>Retrieves an IMG tag with the path to the specified icon.</summary>
        /// <param name="iconName">The name of the icon (see the IconImage enum on the server).</param>
        [AlternateSignature]
        public extern jQueryObject Image(string iconName);

        /// <summary>Retrieves an IMG tag with the path to the specified icon.</summary>
        /// <param name="iconName">The name of the icon (see the IconImage enum on the server).</param>
        /// <param name="alt">The alternative text for the image.</param>
        public jQueryObject Image(string iconName, string alt)
        {
            return Html.CreateImage(Path(iconName), alt);
        }
    }
}
