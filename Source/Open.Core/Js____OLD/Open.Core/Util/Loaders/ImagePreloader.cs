using System.Collections;
using System.Html;
using jQueryApi;

namespace Open.Core.Helpers
{
    public static class ImagePreloader
    {
        #region Internal
        private static readonly ArrayList list = new ArrayList();
        #endregion

        #region Methods : Static
        /// <summary>Preloads the given image.</summary>
        /// <param name="url">The url of the image.</param>
        /// <returns>True if the image was added for preloading, or False if it was already loaded.</returns>
        public static bool Preload(string url)
        {
            // Setup initial conditions.
            url = url.ToLowerCase();
            if (Exists(url)) return false;

            // Create the image.
            Element img = Document.CreateElement(Html.Img);
            img.SetAttribute(Html.Src, url);

            // TODO - Not preloading images.

            // Finish up.
            list.Add(img);
            return true;
        }

        /// <summary>Gets whether the given image has been added for downloading.</summary>
        /// <param name="url">The URL of the image.</param>
        public static bool Exists(string url)
        {
            foreach (Element item in list)
            {
                if ((string)item.GetAttribute(Html.Src) == url) return true;
            }
            return false;
        }
       #endregion
    }
}
