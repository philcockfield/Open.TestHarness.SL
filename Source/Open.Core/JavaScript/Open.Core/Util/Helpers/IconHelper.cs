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
        [AlternateSignature]
        public extern string Path(string iconName);

        /// <summary>Converts the named icon into a path.</summary>
        /// <param name="iconName">The name of the icon (see the IconImage enum on the server).</param>
        /// <param name="greyscale">Flag indicating if the greyscale version of the icon should be used.</param>
        public string Path(string iconName, bool greyscale)
        {
            iconName = Helper.String.RemoveEnd(iconName, ".png");
            if (iconName.StartsWith("Silk"))
            {
                if (Script.IsNullOrUndefined(greyscale)) greyscale = false;
                string greyscalePath = greyscale ? "/Greyscale" : null;
                return string.Format("/Open.Assets/Icons/Silk{0}/{1}.png", greyscalePath, iconName);
            }
            throw new Exception(string.Format("Icon named '{0}' not found.", iconName));
        }

        /// <summary>Retrieves an IMG tag with the path to the specified icon.</summary>
        /// <param name="iconName">The name of the icon (see the IconImage enum on the server).</param>
        [AlternateSignature]
        public extern jQueryObject Image(string iconName);

        /// <summary>Retrieves an IMG tag with the path to the specified icon.</summary>
        /// <param name="iconName">The name of the icon (see the IconImage enum on the server).</param>
        /// <param name="greyscale">Flag indicating if the greyscale version of the icon should be used.</param>
        public jQueryObject Image(string iconName, bool greyscale)
        {
            return Html.CreateImage(Path(iconName, greyscale), null);
        }
    }
}
