using System;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core.Helpers
{
    /// <summary>Utility methods for working with Icons.</summary>
    public class IconHelper
    {
        #region Methods
        /// <summary>Converts the named icon into a path.</summary>
        /// <param name="iconName">The name of the icon (see the IconImage enum on the server).</param>
        [AlternateSignature]
        public extern string Path(string iconName);

        /// <summary>Converts the named icon into a path.</summary>
        /// <param name="icon">The flag representing the icon.</param>
        [AlternateSignature]
        public extern string Path(Icons icon);

        /// <summary>Converts the named icon into a path.</summary>
        /// <param name="icon">The flag representing the icon.</param>
        /// <param name="greyscale">Flag indicating if the greyscale version of the icon should be used.</param>
        [AlternateSignature]
        public extern string Path(Icons icon, bool greyscale);

        /// <summary>Converts the named icon into a path.</summary>
        /// <param name="iconName">The name of the icon (see the IconImage enum on the server).</param>
        /// <param name="greyscale">Flag indicating if the greyscale version of the icon should be used.</param>
        public string Path(string iconName, bool greyscale)
        {
            // Attempt to cast to enum if the 'iconName' is not a string.
            // NB: May be passed in from overload.
            if (!(iconName is string))
            {
                iconName = TryParseAsEnum(iconName);
            }

            // Match the path.
            iconName = Helper.String.RemoveEnd(iconName, ".png");
            if (iconName.StartsWith("Silk"))
            {
                if (Script.IsNullOrUndefined(greyscale)) greyscale = false;
                string greyscalePath = greyscale ? "/Greyscale" : null;
                return string.Format("/Open.Assets/Icons/Silk{0}/{1}.png", greyscalePath, iconName);
            }

            // Finish up.
            throw new Exception(string.Format("Icon named '{0}' not found.", iconName));
        }

        /// <summary>Retrieves an IMG tag with the path to the specified icon.</summary>
        /// <param name="iconName">The name of the icon (see the IconImage enum on the server).</param>
        [AlternateSignature]
        public extern jQueryObject Image(string iconName);

        /// <summary>Retrieves an IMG tag with the path to the specified icon.</summary>
        /// <param name="icon">The flag representing the icon.</param>
        [AlternateSignature]
        public extern jQueryObject Image(Icons icon);

        /// <summary>Retrieves an IMG tag with the path to the specified icon.</summary>
        /// <param name="icon">The flag representing the icon.</param>
        /// <param name="greyscale">Flag indicating if the greyscale version of the icon should be used.</param>
        [AlternateSignature]
        public extern jQueryObject Image(Icons icon, bool greyscale);

        /// <summary>Retrieves an IMG tag with the path to the specified icon.</summary>
        /// <param name="iconName">The name of the icon (see the IconImage enum on the server).</param>
        /// <param name="greyscale">Flag indicating if the greyscale version of the icon should be used.</param>
        public jQueryObject Image(string iconName, bool greyscale)
        {
            return Html.CreateImage(Path(iconName, greyscale), null);
        }
        #endregion

        #region Internal
        private static string TryParseAsEnum(string iconName)
        {
            try
            {
                int integer = (int.Parse(iconName));
                Icons icon = (Icons)(integer);
                return Helper.String.ToSentenceCase(icon.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
