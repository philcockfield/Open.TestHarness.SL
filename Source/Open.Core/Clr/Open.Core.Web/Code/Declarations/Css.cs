using System;
using System.Diagnostics;
using System.Web;


namespace Open.Core.Web
{
    public enum CssFile
    {
        JQueryUi,
        Core,
        CoreControls,
        CoreLists,
        LibraryJit,
    }

    /// <summary>Constants and helpers for working with CSS.</summary>
    public class Css
    {
        #region Properties
        /// <summary>Gets the embed tag for the specified script.</summary>
        /// <param name="cssFile">Flag indicating what css file to retrieve the path for.</param>
        public string this[CssFile cssFile]
        {
            get { return ToCssLink(GetPath(cssFile)); }
        }
        #endregion

        #region Methods
        /// <summary>Formats the given URL into a CSS link tag.</summary>
        /// <param name="url">The path to the style sheet.</param>
        /// <param name="ieOnly">Flag indicating if the link should be wrapped in an IE only conditional statement.</param>
        public static string ToCssLink(string url, bool ieOnly = false)
        {
            string link = string.Format("<link href='{0}' rel='stylesheet' type='text/css' />", url);
            if (ieOnly) link = string.Format("<!--[if IE]> \r\n {0} \r\n <![endif]-->", link);
            return link;
        }
        #endregion

        #region Internal
        private static string GetPath(CssFile cssFile)
        {
            string path;
            switch (cssFile)
            {
                case CssFile.Core: path = "/Open.Core/Css/Core.css"; break;
                case CssFile.CoreControls: path = "/Open.Core/Css/Core.Controls.css"; break;
                case CssFile.CoreLists: path = "/Open.Core/Css/Core.Lists.css"; break;
                case CssFile.LibraryJit: path = "/Open.Core/Css/Jit.Hypertree.css"; break;
                case CssFile.JQueryUi: path = "http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"; break;

                default: throw new NotSupportedException(cssFile.ToString());
            }
            return path.PrependDomain();
        }
        #endregion
    }
}
